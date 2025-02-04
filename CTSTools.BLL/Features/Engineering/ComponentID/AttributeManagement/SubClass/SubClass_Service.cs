using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;
public class SubClass_Service
{
    public static List<SubClassDTO> GetSubClassList_Global(SubClassDTO ClassDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _subClassGlobalList = new List<SubClassDTO>();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeIDArray = [(int)Attribute_Enum.PartType, (int)Attribute_Enum.ComponentType, (int)Attribute_Enum.Class],
                ChildAttributeIDArray = [(int)Attribute_Enum.ComponentType, (int)Attribute_Enum.SubClass, (int)Attribute_Enum.Class],
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true,
                ParentValueDTO = { GetAttributeDTO = true },
                ChildValueDTO = { GetAttributeDTO = true }
            };
            var _valuelinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _classList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.ComponentType ||
                                                       w.ParentAttributeID == (int)Attribute_Enum.PartType &&
                                                       w.ChildAttributeID == (int)Attribute_Enum.Class)
                                           .ToList();

            var _subClassesList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.Class &&
                                                            w.ChildAttributeID == (int)Attribute_Enum.SubClass)
                                                .ToList();
            var _componentTypeFromPartTypeList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.PartType &&
                                                            w.ChildAttributeID == (int)Attribute_Enum.ComponentType)
                                                .ToList();
            if (_subClassesList.Count() > 0)
            {
                foreach (var _subClassesDTO in _subClassesList)
                {
                    var _subClassDTO = new SubClassDTO();
                    var _classValueLinkDTO = new ValueLinkDTO();
                    _subClassDTO.ID = _subClassesDTO.ID;
                    _subClassDTO.SubClassValueDTO = _subClassesDTO.ChildValueDTO;

                    if (_classList.Count() > 0)
                    {
                        _classValueLinkDTO = _classList.Where(w => w.ChildValueID == _subClassesDTO.ParentValueID)
                                                       .FirstOrDefault();
                        _subClassDTO.ClassDTO = _classValueLinkDTO.ChildValueDTO;
                    }


                    _subClassDTO.ComponentTypeDTO = _classValueLinkDTO.ParentAttributeID == (int)Attribute_Enum.ComponentType ?
                                                    _classValueLinkDTO.ParentValueDTO : null;


                    _subClassDTO.PartTypeDTO = _componentTypeFromPartTypeList.Count() > 0 &&
                                               _classValueLinkDTO.ParentAttributeID == (int)Attribute_Enum.ComponentType ?
                                               _componentTypeFromPartTypeList.Where(w => w.ChildValueID == _classValueLinkDTO.ParentValueID)
                                                                             .FirstOrDefault().ParentValueDTO :
                                               _classValueLinkDTO.ParentValueDTO;

                    _subClassGlobalList.Add(_subClassDTO);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _subClassGlobalList;
    }
    public static ValidationResultDTO CreateSubClass_Global(SubClassDTO SubClassDTO)
    {
        SubClassDTO.SubClassValueDTO.AddedByID = SubClassDTO.AddedByID;
        SubClassDTO.SubClassValueDTO.AddedDate = DateTime.Now;
        //Step 1. Validate fields
        var _validationResultDTO = SubClass_Validator.CreateClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Create the attribute value
        _validationResultDTO = Value_Service.CreateValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Validate Value Link
        SubClassDTO.ChildValueID = _validationResultDTO.Data;
        _validationResultDTO = ValueLink_Validator.CreateValueLink_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 4. Create Value Link 
        _validationResultDTO = ValueLink_Service.CreateValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        SubClassDTO.ID = _validationResultDTO.Data;
        //Step 5. Create Decoder
        _validationResultDTO = CreateDecoderFromSubClass(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateSubClass_Global(SubClassDTO SubClassDTO)
    {
        //Step 1.Validate fields
        var _validationResultDTO = SubClass_Validator.UpdateSubClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 2. Update Atttribute Value
        SubClassDTO.SubClassValueDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Value_Service.UpdateValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 3. Update Attribute Value 
        SubClassDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = ValueLink_Service.UpdateValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteSubClass_Global(SubClassDTO SubClassDTO)
    {
        //Step 1.Validate fields
        var _validationResultDTO = SubClass_Validator.DeleteSubClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 1. Delete Value Link 
        _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Value 
        _validationResultDTO = Value_Service.DeleteValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }

    #region Business Logic

    public static ValidationResultDTO CreateDecoderFromSubClass(SubClassDTO SubClassDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _subClassList = GetSubClassList_Global(new SubClassDTO());
            var _subClassDTO = _subClassList.Where(w => w.ID == SubClassDTO.ID).FirstOrDefault();
            if (_subClassDTO != null) 
            {
                var _decorderDTO = new DecoderDTO
                {
                    SubClassID = SubClassDTO.ChildValueID,
                    ClassID = SubClassDTO.ParentValueID,
                    PartTypeID = _subClassDTO.PartTypeDTO.ID,
                    ComponentTypeID = _subClassDTO.ComponentTypeDTO != null ? _subClassDTO.ComponentTypeDTO.ID : null,
                    AddedByID = SubClassDTO.AddedByID,
                    IsActive = true,
                };
                _validationResultDTO = Decoder_Service.CreateDecoder_Global(_decorderDTO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    #region Upload Excel functions

    public static ValidationResultDTO GenerateSubClassFromExcel(FileDTO FileDTO)
    {
        var _excelRowDTO = new ExcelRowDTO();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been create successfully.."
        };
        try
        {
            // step 1. Validate that it is an excel file
            _validationResultDTO = ExcelImport_Validator.ExcelFile_Validation(FileDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 2. Bring the information from excel
            _validationResultDTO = ExcelImport_Validator.ExcelHeaderColumns_Validation(FileDTO);

            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 3. Validate that the list of data comes
            FileDTO.DirectoryArray = _validationResultDTO.Data;
            _validationResultDTO = GetSubClassListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = SubClass_Validator.ExcelSubClassInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create SubClass
            //_validationResultDTO = SubClass_Repository.CreateMultipleSubClass(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetSubClassListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<SubClassDTO>(),
                BadRowLinesList = new List<SubClassDTO>()
            };

            // Converts the Base64 string contained in FileDTO.Data to a byte array,
            // To later process it and read the content of the Excel file.
            var _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            using (var _fileStream = new MemoryStream(_fileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store the excel column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                // Create the list with the name of the columns that were previously validated
                var _columnHeaderNameList = FileDTO.DirectoryArray.Select(ColumnName => ColumnName.ToUpper()).ToList();
                // This is to identify the columns within the Excel, to later bring the information contained in the row
                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    // We take the column name, put it in capital letters to compare it with our list (_columnHeaderNameList) 
                    // and we remove all the spaces from the name before comparing it with the list. (e.g. " Unit  Of Measure " -> "UnitOfMeasure")
                    string headerName = _firstTable.Rows[0][colIndex].ToString().ToUpper();
                    // The regular expression @"\s+", Removes all whitespace from the column name
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", "");
                    if (_columnHeaderNameList.Contains(headerName))
                    {
                        // If the column name exists, it stores it in the dirctory (_columnHeaderMap) and assigns its identifier
                        _columnHeaderMap[headerName] = colIndex;
                    }
                }

                // Process rows starting from the second row (index 1), to take the information to store in each iteration
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    // We create a DTO where we will store the content of the excel to later validate it
                    var _subClassDTO = new SubClassDTO { SubClassValueDTO = new ValueDTO() };
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _subClassDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _subClassDTO.SubClassValueDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]].ToString());
                    _subClassDTO.SubClassValueDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]].ToString());
                    _subClassDTO.SubClassValueDTO.Code = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["CODE"]].ToString());
                    _subClassDTO.ParentValueName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["CLASS"]].ToString());
                    _subClassDTO.SubClassValueDTO.AddedByID = FileDTO.ID;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = SubClass_Validator.ExcelSubClassRows_Validation(_subClassDTO);

                    // We verify if our DTO complied with the validations
                    if (_validationResultDTO.Data.GoodRowLinesList.Count > 0)
                        // If the DTO does not have null properties, it is stored in the GoodRowLines.
                        _excelRowDTO.GoodRowLinesList.AddRange(_validationResultDTO.Data.GoodRowLinesList);
                    else
                        // If any of the DTO properties is null, it is stored on a BadRowLines.
                        _excelRowDTO.BadRowLinesList.AddRange(_validationResultDTO.Data.BadRowLinesList);
                }
            }
            // Verify if there were good or bad lines to return _excelRowDTO
            if (_excelRowDTO.GoodRowLinesList.Count > 0 || _excelRowDTO.BadRowLinesList.Count > 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Column records have no data.";
                _validationResultDTO.Message = "Error";
                return _validationResultDTO;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = ex.Message;
            return _validationResultDTO;
        }
    }
    #endregion

    #endregion

}
