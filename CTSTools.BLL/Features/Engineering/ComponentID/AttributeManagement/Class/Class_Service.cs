using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class_Sequence;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
public class Class_Service
{
    public static List<ClassDTO> GetClassList_Global(ClassDTO ClassDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _classGlobalList = new List<ClassDTO>();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeIDArray = [(int)Attribute_Enum.PartType, (int)Attribute_Enum.ComponentType],
                ChildAttributeID = (int)Attribute_Enum.Class,
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true,
            };
            var _valuelinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);


            var _componentTypeList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.ComponentType).ToList();
            var _componentTypeDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.PartType,
                ChildAttributeID = (int)Attribute_Enum.ComponentType,
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true

            };
            var _partTypeList = ValueLink_Service.GetValueLinkList_Global(_componentTypeDTO);


            foreach (var ComponentTypeDTO in _componentTypeList)
            {
                var _classDTO = new ClassDTO
                {
                    ID = ComponentTypeDTO.ID,
                    ComponentTypeDTO = ComponentTypeDTO.ParentValueDTO,
                    ClassValueDTO = ComponentTypeDTO.ChildValueDTO,

                };
                _classDTO.PartTypeDTO = _partTypeList.Where(w => w.ChildAttributeID == _classDTO.ComponentTypeDTO.AttributeID)
                                                     .ToList()
                                                     .FirstOrDefault()
                                                     .ParentValueDTO;
                _classGlobalList.Add(_classDTO);
            }
            var _partTypeValuesList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.PartType)
                                                    .ToList();
            foreach (var PartTypeDTO in _partTypeValuesList)
            {
                var _classDTO = new ClassDTO
                {
                    ID = PartTypeDTO.ID,
                    PartTypeDTO = PartTypeDTO.ParentValueDTO,
                    ClassValueDTO = PartTypeDTO.ChildValueDTO,
                };
                _classGlobalList.Add(_classDTO);
            }

            //if Value is empty, return list

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _classGlobalList;
    }

    public static ValidationResultDTO CreateClass_Global(ClassDTO ClassDTO)
    {
        ClassDTO.ClassValueDTO.AddedByID = ClassDTO.AddedByID;
        ClassDTO.ClassValueDTO.AddedDate = DateTime.Now;
        //Step 1. Validate fields
        var _validationResultDTO = Class_Validator.CreateClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Create the Class value
        _validationResultDTO = Value_Service.CreateValue_Global(ClassDTO.ClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Validate Value Link
        ClassDTO.ChildValueID = _validationResultDTO.Data;
        ClassDTO.ClassValueDTO.ID = _validationResultDTO.Data;
        _validationResultDTO = ValueLink_Validator.CreateValueLink_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            return _validationResultDTO;
        }
        //Step 4. Create Value Link 
        _validationResultDTO = ValueLink_Service.CreateValueLink_Global(ClassDTO);
        ClassDTO.ID = _validationResultDTO.Data;
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            return _validationResultDTO;
        }
        //Step 5. Create Class ID Value
        ClassDTO.ClassValueDTO.ID = ClassDTO.ChildValueID;
        _validationResultDTO = Class_Sequence_Service.CreateClass_Sequence_Global(ClassDTO);
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ClassDTO);
            return _validationResultDTO;
        }


        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateClass_Global(ClassDTO ClassDTO)
    {

        //Step 1. Validate fields
        var _validationResultDTO = Class_Validator.UpdateClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step X. Update Atttribute Value
        ClassDTO.ClassValueDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Value_Service.UpdateValue_Global(ClassDTO.ClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step Y. Update Attribute Value 
        ClassDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = ValueLink_Service.UpdateValueLink_Global(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        return _validationResultDTO;
    }

    public static ValidationResultDTO DeleteClass_Global(ClassDTO ClassDTO)
    {
        //Step 1. Delete Value Link 
        var _validationResultDTO = Class_Validator.DeleteClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Value 
        _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
        if (_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }

    public static ValidationResultDTO CreateMassiveList_Global(List<ClassDTO> ClassDTOList)
    {
        var _classValueList = new List<ValueDTO>();
        var _valueLinkList = new List<ValueLinkDTO>();
        //Step 1. Validate fields
        var _validationResultDTO = Class_Validator.CreateMultiple_Validation(ClassDTOList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Extraemos los ClassValueDTO de ClassDTOList y los agregamos a newClassValues
        foreach (var ClassDTO in ClassDTOList)
        {
            _classValueList.Add(ClassDTO.ClassValueDTO);  // Aquí estamos extrayendo el ClassValueDTO de cada ClassDTO
        }
        //Step 2. Create the Class value
        _validationResultDTO = Value_Service.CreateMultiple_Global(_classValueList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Asignar los Oid a las propiedades correspondientes de ClassDTOList
        for (int i = 0; i < ClassDTOList.Count; i++)
        {
            // Asignamos el Oid del ValueDTO correspondiente a las propiedades
            ClassDTOList[i].ChildValueID = _validationResultDTO.Data[i].Oid;  // Asignar Oid a ChildValueID
            ClassDTOList[i].ClassValueDTO.ID = _validationResultDTO.Data[i].Oid;  // Asignar Oid a ClassValueDTO.ID
            _classValueList[i].ID = _validationResultDTO.Data[i].Oid;
        }

        //Step 3. Validate Value Link
        _valueLinkList.AddRange(ClassDTOList);
        _validationResultDTO = ValueLink_Validator.CreateMultipleValueLink_Validation(_valueLinkList);
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteMultiple_Global(_classValueList);
            return _validationResultDTO;
        }
        ////Step 4. Create Value Link 
        _validationResultDTO = ValueLink_Service.CreateMultiple_Global(_valueLinkList);
        for (int i = 0; i < ClassDTOList.Count; i++)
        {
            ClassDTOList[i].ID = _validationResultDTO.Data[i].Oid;
            ClassDTOList[i].ClassValueDTO.ID = ClassDTOList[i].ChildValueID;
        }
        //ClassDTO.ID = _validationResultDTO.Data;
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteMultiple_Global(_classValueList);
            return _validationResultDTO;
        }
        ////Step 5. Create Class ID Value
        //ClassDTO.ClassValueDTO.ID = ClassDTO.ChildValueID;
        //_validationResultDTO = Class_Sequence_Service.CreateClass_Sequence_Global(ClassDTO);
        //if (!_validationResultDTO.Result)
        //{
        //    _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
        //    _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ClassDTO);
        //    return _validationResultDTO;
        //}


        return _validationResultDTO;
    }
    #region Business Logic

    #region Upload Excel functions

    public static ValidationResultDTO GenerateClassFromExcel(FileDTO FileDTO)
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
            _validationResultDTO = GetClassListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Class_Validator.ExcelClassInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Class
            _validationResultDTO = CreateMassiveList_Global(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetClassListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<ClassDTO>(),
                BadRowLinesList = new List<ClassDTO>()
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
                    var _classDTO = new ClassDTO { ClassValueDTO = new ValueDTO() };
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _classDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _classDTO.ClassValueDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]].ToString());
                    _classDTO.ClassValueDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]].ToString());
                    _classDTO.ClassValueDTO.Code = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["CODE"]].ToString());
                    _classDTO.PartTypeDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["PARTTYPE"]].ToString());
                    _classDTO.ComponentTypeDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["COMPONENTTYPE"]].ToString());
                    _classDTO.AddedByID = FileDTO.ID;
                    _classDTO.ClassValueDTO.AddedByID = FileDTO.ID;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Class_Validator.ExcelClassRows_Validation(_classDTO);

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
