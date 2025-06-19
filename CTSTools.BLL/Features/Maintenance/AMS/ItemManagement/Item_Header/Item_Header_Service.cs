using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_Header_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        var _ValidationResultDTO = Item_Header_Validator.CreateItem_Header_Validation(Item_HeaderDTO);
        if (_ValidationResultDTO.Result && (Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0))
        {
            Item_HeaderDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Item_Header_Repository.CreateItem_Header(Item_HeaderDTO);
            Item_HeaderDTO.ID = _ValidationResultDTO.Data;
        }
        //Save Item Picture
        if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.Data != null)
        {
            Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
            Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory;
            _ValidationResultDTO = File_Service.SaveFile_Global(Item_HeaderDTO.FileDTO);
        }
        //Save Item_SupportGroup
        if (_ValidationResultDTO.Result && Item_HeaderDTO.SupportGroupID != 0 && Item_HeaderDTO.SupportGroupID != null)
        {
            var _item_SupportGroupDTO = new Item_SupportGroupDTO();
            _item_SupportGroupDTO.Item_HeaderDTO.ID = Item_HeaderDTO.ID;
            _item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _item_SupportGroupDTO.AddedByID = Item_HeaderDTO.AddedByID;
            _item_SupportGroupDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = Item_SupportGroup_Service.CreateItem_SupportGroup_Global(_item_SupportGroupDTO);
        }
        //Save Attachments
        //if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.FileList != null)
        //{
        //    Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
        //    Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        //    _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_HeaderDTO.FileDTO);
        //}
        //Save UserDefined
        if (_ValidationResultDTO.Result && Item_HeaderDTO.UserDefinedIDArray.Length > 0)
        {
            var _userDefinedTemplateDTO = new UserDefinedTemplateDTO();
            _userDefinedTemplateDTO.UserDefinedIDArray = Item_HeaderDTO.UserDefinedIDArray;
            _userDefinedTemplateDTO.Item_HeaderID = Item_HeaderDTO.ID;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.ID = _ValidationResultDTO.Data;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _userDefinedTemplateDTO.LastUpdateByID = Item_HeaderDTO.AddedByID;
            _userDefinedTemplateDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = UserDefinedTemplate_Service.UpdateUserDefinedTemplate_Global(_userDefinedTemplateDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<Item_HeaderDTO>(Item_HeaderDTO, (int)Item_HeaderDTO.AddedByID, (int)Item_HeaderDTO.ID);
        }
        _ValidationResultDTO.Data = Item_HeaderDTO.ID;
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        var _ValidationResultDTO = Item_Header_Validator.UpdateItem_Header_Validation(Item_HeaderDTO);
        var _previousItem_HeaderDTO = GetItem_HeaderList_Global(new Item_HeaderDTO { ID = Item_HeaderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            Item_HeaderDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Item_Header_Repository.UpdateItem_Header(Item_HeaderDTO);
        }
        //Save Item Picture
        if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.Data != null)
        {
            Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
            Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory;
            _ValidationResultDTO = File_Service.UpdateFile_Global(Item_HeaderDTO.FileDTO);
        }
        //Save Attachments
        //if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.FileList != null)
        //{
        //    Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
        //    Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        //    _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_HeaderDTO.FileDTO);
        //}
        //Save UserDefined
        if (_ValidationResultDTO.Result && Item_HeaderDTO.UserDefinedIDArray.Length >= 0)
        {
            var _userDefinedTemplateDTO = new UserDefinedTemplateDTO();
            _userDefinedTemplateDTO.UserDefinedIDArray = Item_HeaderDTO.UserDefinedIDArray;
            _userDefinedTemplateDTO.Item_HeaderID = Item_HeaderDTO.ID;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.ID = Item_HeaderDTO.Item_SupportGroupID;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _userDefinedTemplateDTO.LastUpdateByID = Item_HeaderDTO.LastUpdateByID;
            _userDefinedTemplateDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = UserDefinedTemplate_Service.UpdateUserDefinedTemplate_Global(_userDefinedTemplateDTO);
        }
        //Save Change log
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<Item_HeaderDTO>(_previousItem_HeaderDTO, Item_HeaderDTO, (int)Item_HeaderDTO.LastUpdateByID, (int)Item_HeaderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        //item validation
        var _ValidationResultDTO = Item_Header_Validator.DeleteItem_Header_Validation(Item_HeaderDTO);
        var _previousItem_HeaderDTO = GetItem_HeaderList_Global(new Item_HeaderDTO { ID = Item_HeaderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Item_Header_Repository.DeleteItem_Header(Item_HeaderDTO);
        }
        //Save Change log
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<Item_HeaderDTO>(_previousItem_HeaderDTO, (int)Item_HeaderDTO.LastUpdateByID, (int)Item_HeaderDTO.ID);
        }
        return _ValidationResultDTO;
    }

    public static ValidationResultDTO CreateMassiveList_Global(List<Item_HeaderDTO> Item_HeaderList)
    {
        var _item_SupportGroupList = new List<Item_SupportGroupDTO>();
        // Step 1. Create the Item_Header
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO = Item_Header_Repository.CreateMultiple(Item_HeaderList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2. Validate Item_Header
        // Assign the Oids to the corresponding Item_HeaderList properties
        for (int i = 0; i < Item_HeaderList.Count; i++)
        {
            // We assign the Oid of the ValueDTO within the _validationResultDTO corresponding to the properties
            Item_HeaderList[i].ID = _validationResultDTO.Data[i].Oid;
        }
        // Step 3. Save Item_SupportGroup
        foreach (var _item_HeaderDTO in Item_HeaderList)
        {
            var _item_SupportGroupDTO = new Item_SupportGroupDTO
            {
                Item_HeaderDTO = new Item_HeaderDTO
                {
                    ID = _item_HeaderDTO.ID,
                },
                SupportGroupDTO = new SupportGroupDTO
                {
                    ID = _item_HeaderDTO.SupportGroupID,
                },
                AddedByID = _item_HeaderDTO.AddedByID,
                AddedDate = _item_HeaderDTO.AddedDate,
                IsActive = _item_HeaderDTO.IsActive,
            };
            _item_SupportGroupList.Add(_item_SupportGroupDTO);
        }
        _validationResultDTO = Item_SupportGroup_Repository.CreateMultiple(_item_SupportGroupList);
        return _validationResultDTO;
    }

    public static List<Item_HeaderDTO> GetItem_HeaderList_Global(Item_HeaderDTO Item_HeaderDTO, PagedResultDTO<Item_HeaderDTO> PagedResultDTO = null)
    {
        var _item_headerglobalList = new List<Item_HeaderDTO>();
        try
        {
            var _item_headerList = Item_Header_Repository.GetItem_HeaderList(Item_HeaderDTO, PagedResultDTO);
            // if Item_Header is empty, return list
            if (_item_headerList.Count() == 0)
            {
                _item_headerglobalList = _item_headerList;
                return _item_headerglobalList;
            }
            if (/*&& !Item_HeaderDTO.GetSupportGroupDTO*/ !Item_HeaderDTO.GetBrandDTO && !Item_HeaderDTO.GetItemHeaderPicture)
            {
                _item_headerglobalList = _item_headerList;
                return _item_headerglobalList;
            }
            _item_headerglobalList = GetItem_HeaderRelatedData(Item_HeaderDTO, _item_headerList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_headerglobalList;
    }

    public static List<Item_HeaderDTO> GetItem_HeaderRelatedData(Item_HeaderDTO Item_HeaderDTO, List<Item_HeaderDTO> Item_HeaderList)
    {
        var _item_headerglobalList = new List<Item_HeaderDTO>();
        var _brandDict = new Dictionary<int?, BrandDTO>();
        try
        {
            if (Item_HeaderDTO.GetBrandDTO)
            {
                Item_HeaderDTO.BrandDTO.BrandIDArray = Item_HeaderList.GroupBy(g => g.BrandID)
                        .Select(s => s.Key)
                        .ToArray();

                _brandDict = Brand_Service.GetBrandList_Global(Item_HeaderDTO.BrandDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _item_headerDTO in Item_HeaderList)
            {
                if (Item_HeaderDTO.GetBrandDTO && _brandDict.ContainsKey(_item_headerDTO.BrandID))
                {
                    _item_headerDTO.BrandDTO = _brandDict[_item_headerDTO.BrandID];
                }
                if (Item_HeaderDTO.GetItemHeaderPicture)
                {
                    var _fileDTO = new FileDTO
                    {
                        ID = (int)_item_headerDTO.ID,
                        FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory
                    };
                    _item_headerDTO.ItemImg = File_Service.GetFile(_fileDTO).URL;
                }
                _item_headerglobalList.Add(_item_headerDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_headerglobalList;
    }

    public static int GetItem_HeaderTotalCount(PagedResultDTO<Item_HeaderDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Item_Header_Repository.GetItem_HeaderCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic


    #endregion

    #region Upload Excel functions

    public static ValidationResultDTO GenerateItem_HeaderFromExcel(FileDTO FileDTO)
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
            _validationResultDTO = GetItem_HeaderListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Item_Header_Validator.ExcelItem_HeaderInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Item_Header
            _validationResultDTO = Item_Header_Service.CreateMassiveList_Global(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetItem_HeaderListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<Item_HeaderDTO>(),
                BadRowLinesList = new List<Item_HeaderDTO>()
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
                    var _item_HeaderDTO = new Item_HeaderDTO();
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _item_HeaderDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _item_HeaderDTO.Model = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["MODEL"]].ToString());
                    _item_HeaderDTO.BrandName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["BRAND"]].ToString());
                    _item_HeaderDTO.SupportGroupName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["SUPPORTGROUP"]].ToString());
                    _item_HeaderDTO.SubClassName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["SUBCLASS"]].ToString());
                    _item_HeaderDTO.AddedByID = FileDTO.ID;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Item_Header_Validator.ExcelItem_HeaderRows_Validation(_item_HeaderDTO);

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

    #region Files
    public static List<FileDTO> GetItem_HeaderFileList(Item_HeaderDTO Item_HeaderDTO)
    {
        List<FileDTO> _Item_HeaderFileList = new List<FileDTO>();
        try
        {
            if (Item_HeaderDTO.ID != null && Item_HeaderDTO.ID > 0)
            {
                var _fileDTO = new FileDTO { ID = (int)Item_HeaderDTO.ID, FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory };
                _Item_HeaderFileList = File_Service.GetFileList(_fileDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _Item_HeaderFileList;
    }

    public static ValidationResultDTO DeleteItemHeaderFile(FileDTO FileDTO)
    {
        FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        var _validationResultDTO = File_Service.DeleteFile(FileDTO);
        return _validationResultDTO;
    }
    #endregion
}
