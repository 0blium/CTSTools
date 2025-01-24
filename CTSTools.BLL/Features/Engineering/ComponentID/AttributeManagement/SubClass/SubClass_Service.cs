using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
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

    #region Update Excel functions
    public static ValidationResultDTO SubClassFileValidation_Global(FileDTO FileDTO)
    {
        ExcelSubClassDTO _excelSubClassDTO = new ExcelSubClassDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelSubClassRowsValidation = new ExcelSubClassDTO();
            string _extension = "";
            if (FileDTO.FileName.Contains(".xlsx"))
            {
                _extension = ".xlsx";
            }
            else if (FileDTO.FileName.Contains(".xls"))
            {
                _extension = ".xls";
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Input only excel files.";
                _validationResultDTO.Message = "Invalid file";
            }
            //Step 1. Validate if the files contains following headers: Number, Name, Address, City, State, Country, Postal Code.
            // WARNING: The validations allows to contain empty rows above the column headers. If something are above of coliumns, the validation
            // will take it as an error.
            _excelSubClassDTO = ValidateSubClassFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelSubClassDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetSubClassInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelSubClassRowsValidation = SubClassFileRowsValidation(_validationResultDTO.Data);

                    if (_excelSubClassRowsValidation.SubClassGoodLinesList.Count > 0)
                    {
                        foreach (var _SubClassDTO in _excelSubClassRowsValidation.SubClassGoodLinesList)
                        {
                            _SubClassDTO.AddedByID = FileDTO.ID;
                            _validationResultDTO = CreateSubClass_Global(_SubClassDTO);
                        }
                    }
                }
                else
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelSubClassRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelSubClassRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelSubClassRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelSubClassRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ExcelSubClassDTO ValidateSubClassFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelSubClassDTO _excelSubClassFileValidationDTO = new ExcelSubClassDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();

        if (Extension == ".xlsx" || Extension == ".xls")
        {
            _fileheaders = ExcelDataImport_Service.GetHeadersFromExcel(_fileBytes);
        }
        else
        {
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = "Input only excel files.";
            _validationResultDTO.Message = "Invalid file";
        }
        string _missingHeader = string.Empty;
        _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "name,<br>";
        _missingHeader += (_fileheaders.Contains("code") == true) ? string.Empty : "code,<br>";
        _missingHeader += (_fileheaders.Contains("description") == true) ? string.Empty : "description,<br>";
        _missingHeader += (_fileheaders.Contains("class")) ? string.Empty : "class,<br>";

        if (_missingHeader != string.Empty)
        {
            var lastComma = _missingHeader.LastIndexOf(',');
            _missingHeader = _missingHeader.Remove(lastComma, 1).Insert(lastComma, ".");
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = string.Format("The following columns are missing:<br> {0}", _missingHeader);
            _validationResultDTO.Message = "Error";
        }
        else
        {
            _validationResultDTO.Result = true;
            _validationResultDTO.Description = "The file have the correct format ";
            _validationResultDTO.Message = "Success";
        }
        _excelSubClassFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelSubClassFileValidationDTO;
    }
    private static ValidationResultDTO GetSubClassInfoListFromExcelFile(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelSubClassDTO> _excelSubClassDTOList = new List<ExcelSubClassDTO>();
            using (var _fileStream = new MemoryStream(FileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                // Fill the dictionary with headings
                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    string headerName = _firstTable.Rows[0][colIndex].ToString().Trim();
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", " ");
                    if (headerName.Equals("NAME", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("CODE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("CLASS", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelSubClassDTO = new ExcelSubClassDTO();
                    _excelSubClassDTO.SubClassDTO.SubClassValueDTO = new ValueDTO();
                    _excelSubClassDTO.SubClassDTO.PartTypeDTO= new ValueDTO();
                    _excelSubClassDTO.SubClassDTO.ComponentTypeDTO = new ValueDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelSubClassDTO.SubClassDTO.SubClassValueDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("CODE", out int CodeIndex))
                    {
                        string _codeValue = row[CodeIndex].ToString().Trim();
                        _codeValue = System.Text.RegularExpressions.Regex.Replace(_codeValue, @"\s+", " ");
                        _excelSubClassDTO.SubClassDTO.SubClassValueDTO.Code = _codeValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelSubClassDTO.SubClassDTO.SubClassValueDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("CLASS", out int ClassIndex))
                    {
                        string _classValue = row[ClassIndex].ToString().Trim();
                        _classValue = System.Text.RegularExpressions.Regex.Replace(_classValue, @"\s+", " ");
                        _excelSubClassDTO.SubClassDTO.ParentValueName = _classValue;
                        _haveInfo = true;
                    }

                    if (_haveInfo)
                    {
                        _excelSubClassDTO.RowIteration = rowIndex + 1;
                        _excelSubClassDTOList.Add(_excelSubClassDTO);
                    }
                }
            }
            if (_excelSubClassDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelSubClassDTOList;
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
    private static ExcelSubClassDTO SubClassFileRowsValidation(List<ExcelSubClassDTO> ExcelSubClassFileDataList)
    {
        ExcelSubClassDTO _excelSubClassFileValidationDTO = new ExcelSubClassDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelSubClassFileData in ExcelSubClassFileDataList)
            {
                SubClassDTO _subClassDTO = new SubClassDTO();
                _subClassDTO.SubClassValueDTO = new ValueDTO();
                bool isSucces = true;
                if (string.IsNullOrEmpty(_excelSubClassFileData.SubClassDTO.SubClassValueDTO.Name))
                {
                    _excelSubClassFileData.SubClassDTO.SubClassValueDTO.Name = "Error, The Name is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelSubClassFileData.SubClassDTO.SubClassValueDTO.Code))
                {
                    _excelSubClassFileData.SubClassDTO.SubClassValueDTO.Code = "Error, The Code is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelSubClassFileData.SubClassDTO.ParentValueName))
                {
                    _excelSubClassFileData.SubClassDTO.ParentValueName = "Error, The Class is null or empty";
                    isSucces = false;
                }
                else
                {
                    _subClassDTO.SubClassValueDTO.AttributeID = (int)Attribute_Enum.SubClass;
                    var _existParentValueDTO = Value_Service.GetValueList_Global(new ValueDTO { AttributeID = (int)Attribute_Enum.Class, Name = _excelSubClassFileData.SubClassDTO.ParentValueName }).FirstOrDefault();
                    if (_existParentValueDTO != null)
                    {
                        _subClassDTO.ParentAttributeID = (int)Attribute_Enum.Class;
                        _subClassDTO.ParentValueID = _existParentValueDTO.ID;
                        _subClassDTO.ChildAttributeID = (int)Attribute_Enum.SubClass;
                    }
                    else
                    {
                        _excelSubClassFileData.SubClassDTO.ParentValueName = "Error, The Class is not exist";
                        isSucces = false;
                    }
                }

                _subClassDTO.SubClassValueDTO.Name = _excelSubClassFileData.SubClassDTO.SubClassValueDTO.Name;
                _subClassDTO.SubClassValueDTO.Code = _excelSubClassFileData.SubClassDTO.SubClassValueDTO.Code;
                _subClassDTO.SubClassValueDTO.Description = _excelSubClassFileData.SubClassDTO.SubClassValueDTO.Description;
                _subClassDTO.ParentValueName = _excelSubClassFileData.SubClassDTO.ParentValueName;
                _subClassDTO.SubClassValueDTO.IsActive = true;
                _subClassDTO.IsActive = true;

                if (isSucces == true)
                {
                    _excelSubClassFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelSubClassFileValidationDTO.SubClassGoodLinesList.Add(_subClassDTO);
                }
                else
                {
                    _subClassDTO.ID = _excelSubClassFileData.RowIteration;
                    _excelSubClassFileValidationDTO.ValidationResultDTO.Message = _excelSubClassFileValidationDTO.ValidationResultDTO.Message;
                    _excelSubClassFileValidationDTO.SubClassBadLinesList.Add(_subClassDTO);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _excelSubClassFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelSubClassFileValidationDTO;
    }
    #endregion

    #endregion

}
