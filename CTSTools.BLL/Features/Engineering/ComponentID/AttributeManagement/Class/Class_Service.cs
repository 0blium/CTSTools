using CTSTools.BLL.Common;
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

    #region Business Logic

    #region Update Excel functions
    public static ValidationResultDTO ClassFileValidation_Global(FileDTO FileDTO)
    {
        ExcelClassDTO _excelClassDTO = new ExcelClassDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelClassRowsValidation = new ExcelClassDTO();
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
            _excelClassDTO = ValidateClassFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelClassDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetClassInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelClassRowsValidation = ClassFileRowsValidation(_validationResultDTO.Data);

                    if (_excelClassRowsValidation.ClassGoodLinesList.Count > 0)
                    {
                        foreach (var _classDTO in _excelClassRowsValidation.ClassGoodLinesList)
                        {
                            _classDTO.AddedByID = FileDTO.ID;
                            var _validationResulDTO = CreateClass_Global(_classDTO);
                        }
                    }
                }
                else
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelClassRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelClassRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelClassRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelClassRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ExcelClassDTO ValidateClassFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelClassDTO _excelClassFileValidationDTO = new ExcelClassDTO();
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
        _missingHeader += (_fileheaders.Contains("part type") == true || _fileheaders.Contains("parttype")) ? string.Empty : "part type,<br>";
        _missingHeader += (_fileheaders.Contains("component type") == true || _fileheaders.Contains("componenttype")) ? string.Empty : "component type,<br>";

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
        _excelClassFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelClassFileValidationDTO;
    }
    private static ValidationResultDTO GetClassInfoListFromExcelFile(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelClassDTO> _excelClassDTOList = new List<ExcelClassDTO>();
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
                        headerName.Equals("PART TYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("PARTTYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("COMPONENT TYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("COMPONENTTYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelClassDTO = new ExcelClassDTO();
                    _excelClassDTO.ClassDTO.ClassValueDTO = new ValueDTO();
                    _excelClassDTO.ClassDTO.PartTypeDTO = new ValueDTO();
                    _excelClassDTO.ClassDTO.ComponentTypeDTO = new ValueDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelClassDTO.ClassDTO.ClassValueDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("CODE", out int CodeIndex))
                    {
                        string _codeValue = row[CodeIndex].ToString().Trim();
                        _codeValue = System.Text.RegularExpressions.Regex.Replace(_codeValue, @"\s+", " ");
                        _excelClassDTO.ClassDTO.ClassValueDTO.Code = _codeValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelClassDTO.ClassDTO.ClassValueDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("PART TYPE", out int PartTypeIndex) ||
                        _columnHeaderMap.TryGetValue("PARTTYPE", out PartTypeIndex))
                    {
                        string _partTypeValue = row[PartTypeIndex].ToString().Trim();
                        _partTypeValue = System.Text.RegularExpressions.Regex.Replace(_partTypeValue, @"\s+", " ");
                        _excelClassDTO.ClassDTO.PartTypeDTO.Name = _partTypeValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("COMPONENT TYPE", out int ComponentTypeIndex) ||
                        _columnHeaderMap.TryGetValue("COMPONENTTYPE", out ComponentTypeIndex))
                    {
                        string _componentTypeValue = row[ComponentTypeIndex].ToString().Trim();
                        _componentTypeValue = System.Text.RegularExpressions.Regex.Replace(_componentTypeValue, @"\s+", " ");
                        _excelClassDTO.ClassDTO.ComponentTypeDTO.Name = _componentTypeValue;
                        _haveInfo = true;
                    }
                    if (_haveInfo)
                    {
                        _excelClassDTOList.Add(_excelClassDTO);
                    }
                }
            }
            if (_excelClassDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelClassDTOList;
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
    private static ExcelClassDTO ClassFileRowsValidation(List<ExcelClassDTO> ExcelClassFileDataList)
    {
        ExcelClassDTO _excelClassFileValidationDTO = new ExcelClassDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelClassFileData in ExcelClassFileDataList)
            {
                ClassDTO _classDTO = new ClassDTO();
                _classDTO.ClassValueDTO = new ValueDTO();
                _classDTO.PartTypeDTO = new ValueDTO();
                _classDTO.ComponentTypeDTO = new ValueDTO();
                bool isSucces = true;
                if (string.IsNullOrEmpty(_excelClassFileData.ClassDTO.ClassValueDTO.Name))
                {
                    _excelClassFileData.ClassDTO.ClassValueDTO.Name = "Error, The Name is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelClassFileData.ClassDTO.ClassValueDTO.Code))
                {
                    _excelClassFileData.ClassDTO.ClassValueDTO.Code = "Error, The Code is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelClassFileData.ClassDTO.PartTypeDTO.Name))
                {
                    _excelClassFileData.ClassDTO.PartTypeDTO.Name = "Error, The Part Type is null or empty";
                    isSucces = false;
                }
                else
                {
                    _classDTO.ClassValueDTO.AttributeID = (int)Attribute_Enum.Class;
                    if (_excelClassFileData.ClassDTO.PartTypeDTO.Name == nameof(Value_Enum.PartTypeValue_Enum.Purchased))
                    {
                        _classDTO.ParentAttributeID = (int)Attribute_Enum.PartType;
                        _classDTO.ParentValueID = (int)Value_Enum.PartTypeValue_Enum.Purchased;
                        _classDTO.ChildAttributeID = (int)Attribute_Enum.Class;
                    }
                    else if (_excelClassFileData.ClassDTO.PartTypeDTO.Name == nameof(Value_Enum.PartTypeValue_Enum.Manufactured))
                    {
                        if (string.IsNullOrEmpty(_excelClassFileData.ClassDTO.ComponentTypeDTO.Name))
                        {
                            _excelClassFileData.ClassDTO.ComponentTypeDTO.Name = "Error, The Component Type is null or empty";
                            isSucces = false;
                        }
                        else
                        {
                            var _existComponentTypeDTO = Value_Service.GetValueList_Global(new ValueDTO { AttributeID = (int)Attribute_Enum.ComponentType, Name = _excelClassFileData.ClassDTO.ComponentTypeDTO.Name }).FirstOrDefault();
                            if (_existComponentTypeDTO != null)
                            {
                                _classDTO.ParentAttributeID = (int)Attribute_Enum.ComponentType;
                                _classDTO.ParentValueID = _existComponentTypeDTO.ID;
                                _classDTO.ChildAttributeID = (int)Attribute_Enum.Class;
                            }
                            else
                            {
                                _excelClassFileData.ClassDTO.ComponentTypeDTO.Name = "Error, The Component Type not exist";
                                isSucces = false;
                            }
                        }
                    }
                    else 
                    {
                        _excelClassFileData.ClassDTO.PartTypeDTO.Name = "Error, The Part Type is not exist";
                        isSucces = false;
                    }
                }

                _classDTO.ClassValueDTO.Name = _excelClassFileData.ClassDTO.ClassValueDTO.Name;
                _classDTO.ClassValueDTO.Code = _excelClassFileData.ClassDTO.ClassValueDTO.Code;
                _classDTO.ClassValueDTO.Description = _excelClassFileData.ClassDTO.ClassValueDTO.Description;
                _classDTO.PartTypeDTO.Name = _excelClassFileData.ClassDTO.PartTypeDTO.Name;
                _classDTO.ComponentTypeDTO.Name = _excelClassFileData.ClassDTO.ComponentTypeDTO.Name;
                _classDTO.ClassValueDTO.IsActive = true;
                _classDTO.IsActive = true;

                if (isSucces == true)
                {
                    _excelClassFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelClassFileValidationDTO.ClassGoodLinesList.Add(_classDTO);
                }
                else
                {
                    _excelClassFileValidationDTO.ValidationResultDTO.Message = _excelClassFileValidationDTO.ValidationResultDTO.Message;
                    _excelClassFileValidationDTO.ClassBadLinesList.Add(_classDTO);
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
        _excelClassFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelClassFileValidationDTO;
    }
    #endregion

    #endregion

}
