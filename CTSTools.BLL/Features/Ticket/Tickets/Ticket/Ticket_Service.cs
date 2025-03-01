using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Ticket.Item.Item_Line;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.BLL.Features.Ticket.Tickets.Category;
using CTSTools.BLL.Features.Ticket.Tickets.Priority;
using CTSTools.DAL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status.Status_Enum;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartUsage;

namespace CTSTools.BLL.Features.Ticket.Tickets.Ticket;

public class Ticket_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateTicket_Global(TicketDTO TicketDTO)
    {
        var _ValidationResultDTO = Ticket_Validator.CreateTicket_Validation(TicketDTO);
        if (_ValidationResultDTO.Result)
        {
            TicketDTO.AddedDate = DateTime.Now;
            TicketDTO.StatusDTO.ID = (int)Statuses_Enum.Active;
            GetTicketNumber(TicketDTO);
            _ValidationResultDTO = Ticket_Repository.CreateTicket(TicketDTO);
        }
        //SaveAttachments
        if (_ValidationResultDTO.Result && TicketDTO.FileDTO?.FileList != null)
        {
            TicketDTO.FileDTO.ID = (int)TicketDTO.ID;
            TicketDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.TicketAttachmentsDirectory;
            _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(TicketDTO.FileDTO);
        }
        //Send Notifications to Created By And SupportGroup
        if (_ValidationResultDTO.Result)
        {
            ////Define Requestor email.
            ////find user by employee tress id
            //var _requestorDTO = User_Service.GetUserList_Global(new UserDTO { EmployeeTressDTO = TicketDTO.RequestorDTO }).FirstOrDefault();
            //if (_requestorDTO == null)
            //{
            //    //if requestor doesn't have account , send email to the creator of ticket.
            //    _requestorDTO = User_Service.GetUserList_Global(new UserDTO { ID = TicketDTO.CreatedByDTO.ID }).FirstOrDefault();
            ////}
            //TicketDTO.RequestorEmail = _requestorDTO.Email;
            _ValidationResultDTO.Data = TicketDTO.TicketNumber;
        }


        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateTicket_Global(TicketDTO TicketDTO)
    {
        var _ValidationResultDTO = Ticket_Validator.UpdateTicket_Validation(TicketDTO);
        var _previousTicketDTO = GetTicketList_Global(new TicketDTO { ID = TicketDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            TicketDTO.LastUpdate = DateTime.Now;
            TicketDTO.AssignedDate = (_previousTicketDTO.AssignedToDTO?.ID != TicketDTO.AssignedToDTO?.ID) &&
                (_previousTicketDTO.AssignedToDTO?.ID != null && TicketDTO.AssignedToDTO?.ID != null) ? DateTime.Now : _previousTicketDTO.AssignedDate;
            //Ticket Status change to closed
            if (TicketDTO.StatusDTO.ID == (int)Statuses_Enum.Closed && _previousTicketDTO.StatusDTO.ID != TicketDTO.StatusDTO.ID)
            {
                //If ticket is closed assigned new values
                TicketDTO.ClosedByDTO = new UserDTO { ID = TicketDTO.LastUpdateByID };
                TicketDTO.ClosedDate = DateTime.Now;
            }
            else
            {
                //If ticket not closed not changes values
                TicketDTO.ClosedByDTO = _previousTicketDTO.ClosedByDTO;
                TicketDTO.ClosedDate = _previousTicketDTO.ClosedDate;
            }
            _ValidationResultDTO = Ticket_Repository.UpdateTicket(TicketDTO);
        }
        //Create Spare Part Inventory Trasanction
        if (_ValidationResultDTO.Result && TicketDTO.Item_LineDTO?.ID != null && TicketDTO.Item_LineDTO?.ID != 0 && TicketDTO.StatusDTO.ID == (int)Statuses_Enum.Closed)
        {
            _ValidationResultDTO = SparePartInventory_Service.SparePartInventoryTransaction(new SparePartUsageDTO { TicketDTO = TicketDTO, LastUpdateByID = TicketDTO.LastUpdateByID });
        }
        //Create log
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate(_previousTicketDTO, TicketDTO, (int)TicketDTO.LastUpdateByID, (int)TicketDTO.ID);
        }
        //Send min & max Notification To Support Group

        //Send email to support group if support group change
        if (_ValidationResultDTO.Result && (_previousTicketDTO.SupportGroupDTO.ID != TicketDTO.SupportGroupDTO.ID))
        {

        }
        //Send email to requestor when change status
        if (_ValidationResultDTO.Result && (_previousTicketDTO.StatusDTO.ID != TicketDTO.StatusDTO.ID))
        {

        }
        //Send email to responsible when the "Assigned to" change
        if (_ValidationResultDTO.Result && (_previousTicketDTO.SupportGroupDTO.ID != TicketDTO.SupportGroupDTO.ID))
        {

        }

        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteTicket_Global(TicketDTO TicketDTO)
    {
        var _ValidationResultDTO = Ticket_Validator.DeleteTicket_Validation(TicketDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Ticket_Repository.DeleteTicket(TicketDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<TicketDTO> GetTicketList_Global(TicketDTO TicketDTO, PagedResultDTO<TicketDTO> PagedResultDTO = null)
    {
        var _ticketglobalList = new List<TicketDTO>();
        try
        {
            var _ticketList = Ticket_Repository.GetTicketList(TicketDTO, PagedResultDTO);
            // if Ticket is empty, return list
            if (_ticketList.Count() == 0)
            {
                _ticketglobalList = _ticketList;
                return _ticketglobalList;
            }
            if (!TicketDTO.GetFacilityDTO && !TicketDTO.GetDepartmentDTO && !TicketDTO.GetItem_LineDTO && !TicketDTO.GetStatusDTO && !TicketDTO.GetPriorityDTO && !TicketDTO.GetCategoryDTO && !TicketDTO.GetSupportGroupDTO)
            {
                _ticketglobalList = _ticketList;
                return _ticketglobalList;
            }
            _ticketglobalList = GetTicketRelatedData(TicketDTO, _ticketList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ticketglobalList;
    }



    public static List<TicketDTO> GetTicketRelatedData(TicketDTO TicketDTO, List<TicketDTO> TicketList)
    {
        var _ticketglobalList = new List<TicketDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _departmentDict = new Dictionary<int?, DepartmentDTO>();
        var _item_lineDict = new Dictionary<int?, Item_LineDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _priorityDict = new Dictionary<int?, PriorityDTO>();
        var _categoryDict = new Dictionary<int?, CategoryDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();

        try
        {
            if (TicketDTO.GetFacilityDTO)
            {
                TicketDTO.FacilityDTO.FacilityIDArray = TicketList.GroupBy(g => g.FacilityDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(TicketDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetDepartmentDTO)
            {
                TicketDTO.DepartmentDTO.DepartmentIDArray = TicketList.GroupBy(g => g.DepartmentDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _departmentDict = Department_Service.GetDepartmentList_Global(TicketDTO.DepartmentDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetItem_LineDTO)
            {
                TicketDTO.Item_LineDTO.Item_LineIDArray = TicketList.GroupBy(g => g.Item_LineDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_lineDict = Item_Line_Service.GetItem_LineList_Global(TicketDTO.Item_LineDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetStatusDTO)
            {
                TicketDTO.StatusDTO.StatusIDArray = TicketList.GroupBy(g => g.StatusDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(TicketDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetPriorityDTO)
            {
                TicketDTO.PriorityDTO.PriorityIDArray = TicketList.GroupBy(g => g.PriorityDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _priorityDict = Priority_Service.GetPriorityList_Global(TicketDTO.PriorityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetCategoryDTO)
            {
                TicketDTO.CategoryDTO.CategoryIDArray = TicketList.GroupBy(g => g.CategoryDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _categoryDict = Category_Service.GetCategoryList_Global(TicketDTO.CategoryDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (TicketDTO.GetSupportGroupDTO)
            {
                TicketDTO.SupportGroupDTO.SupportGroupIDArray = TicketList.GroupBy(g => g.SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(TicketDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _ticketDTO in TicketList)
            {
                if (TicketDTO.GetFacilityDTO && _facilityDict.ContainsKey(_ticketDTO.FacilityDTO.ID))
                {
                    _ticketDTO.FacilityDTO = _facilityDict[_ticketDTO.FacilityDTO.ID];
                }
                if (TicketDTO.GetDepartmentDTO && _departmentDict.ContainsKey(_ticketDTO.DepartmentDTO.ID))
                {
                    _ticketDTO.DepartmentDTO = _departmentDict[_ticketDTO.DepartmentDTO.ID];
                }
                if (TicketDTO.GetItem_LineDTO && _item_lineDict.ContainsKey(_ticketDTO.Item_LineDTO.ID))
                {
                    _ticketDTO.Item_LineDTO = _item_lineDict[_ticketDTO.Item_LineDTO.ID];
                }
                if (TicketDTO.GetStatusDTO && _statusDict.ContainsKey(_ticketDTO.StatusDTO.ID))
                {
                    _ticketDTO.StatusDTO = _statusDict[_ticketDTO.StatusDTO.ID];
                }
                if (TicketDTO.GetPriorityDTO && _priorityDict.ContainsKey(_ticketDTO.PriorityDTO.ID))
                {
                    _ticketDTO.PriorityDTO = _priorityDict[_ticketDTO.PriorityDTO.ID];
                }
                if (TicketDTO.GetCategoryDTO && _categoryDict.ContainsKey(_ticketDTO.CategoryDTO.ID))
                {
                    _ticketDTO.CategoryDTO = _categoryDict[_ticketDTO.CategoryDTO.ID];
                }
                if (TicketDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_ticketDTO.SupportGroupDTO.ID))
                {
                    _ticketDTO.SupportGroupDTO = _supportgroupDict[_ticketDTO.SupportGroupDTO.ID];
                }
                _ticketglobalList.Add(_ticketDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ticketglobalList;
    }




    public static int GetTicketTotalCount(PagedResultDTO<TicketDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Ticket_Repository.GetTicketCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    public static ValidationResultDTO GetTicketNumber(TicketDTO TicketDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _ticketNumber = AssetManagementSQL.GetTicketNumber();
            TicketDTO.TicketNumber = _ticketNumber;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha ocurrido un error. {0}", ex.Message);
        }
        return _validationResultDTO;
    }

    #endregion

    #region Files
    public static List<FileDTO> GetTicketFileList(TicketDTO TicketDTO)
    {
        List<FileDTO> _Item_HeaderFileList = new List<FileDTO>();
        try
        {
            if (TicketDTO.ID != null && TicketDTO.ID > 0)
            {
                var _fileDTO = new FileDTO { ID = (int)TicketDTO.ID, FileDirectory = (int)FileDirectory_Enum.TicketAttachmentsDirectory };
                _Item_HeaderFileList = File_Service.GetFileList(_fileDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _Item_HeaderFileList;
    }

    public static ValidationResultDTO DeleteTicketFile(FileDTO FileDTO)
    {
        FileDTO.FileDirectory = (int)FileDirectory_Enum.TicketAttachmentsDirectory;
        var _validationResultDTO = File_Service.DeleteFile(FileDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO UploadTicketFile(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        FileDTO.FileDirectory = (int)FileDirectory_Enum.TicketAttachmentsDirectory;
        if (FileDTO?.FileList != null && FileDTO?.FileList.Count() > 0 && FileDTO.ID != 0)
        {
            _validationResultDTO = File_Service.SaveMultipleFiles_Global(FileDTO);
        }
        return _validationResultDTO;
    }
    #endregion
}
