using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Ticket;

public class TicketMap
{
    public static TicketDTO XPOToDTO(TicketXPO TicketXPO)
    {
        var _ticketDTO = new TicketDTO();
        try
        {
            _ticketDTO.ID = TicketXPO.Oid;
            _ticketDTO.Title = TicketXPO.Title;
            _ticketDTO.Description = TicketXPO.Description;
            _ticketDTO.Solution = TicketXPO.Solution;
            _ticketDTO.Resolution = TicketXPO.Resolution;
            _ticketDTO.TicketNumber = TicketXPO.TicketNumber;
            _ticketDTO.CreatedByID = (TicketXPO.CreatedBy != null) ? TicketXPO.CreatedBy.Oid : 0;
            _ticketDTO.CreatedByName = (TicketXPO.CreatedBy != null) ? TicketXPO.CreatedBy.Name : "Unnassigned";
            _ticketDTO.FacilityID = (TicketXPO.Facility != null) ? TicketXPO.Facility.Oid : 0;
            _ticketDTO.FacilityName = (TicketXPO.Facility != null) ? TicketXPO.Facility.Name : "Unnassigned";
            _ticketDTO.DepartmentID = (TicketXPO.Department != null) ? TicketXPO.Department.Oid : 0;
            _ticketDTO.DepartmentName = (TicketXPO.Department != null) ? TicketXPO.Department.Name : "Unnassigned";
            _ticketDTO.AssignedToID = (TicketXPO.AssignedTo != null) ? TicketXPO.AssignedTo.Oid : 0;
            _ticketDTO.AssignedToName = (TicketXPO.AssignedTo != null) ? TicketXPO.AssignedTo.Name : "Unnassigned";
            _ticketDTO.AssignedDate = (TicketXPO.AssignedDate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.AssignedDate : (DateTime?)null;
            _ticketDTO.Note = TicketXPO.Note;
            _ticketDTO.Item_LineDTO.ID = (TicketXPO.Item_Line != null) ? TicketXPO.Item_Line.Oid : 0;
            _ticketDTO.Item_LineID = (TicketXPO.Item_Line != null) ? TicketXPO.Item_Line.Oid : 0;
            _ticketDTO.StatusID = (TicketXPO.Status != null) ? TicketXPO.Status.Oid : 0;
            _ticketDTO.StatusName = (TicketXPO.Status != null) ? TicketXPO.Status.Name : "Unnassigned";
            _ticketDTO.PriorityID = (TicketXPO.Priority != null) ? TicketXPO.Priority.Oid : 0;
            _ticketDTO.PriorityName = (TicketXPO.Priority != null) ? TicketXPO.Priority.Name : "Unnassigned";
            _ticketDTO.CategoryID = (TicketXPO.Category != null) ? TicketXPO.Category.Oid : 0;
            _ticketDTO.CategoryName = (TicketXPO.Category != null) ? TicketXPO.Category.Name : "Unnassigned";
            _ticketDTO.SubCategoryID = (TicketXPO.SubCategory != null) ? TicketXPO.SubCategory.Oid : 0;
            _ticketDTO.SubCategoryName = (TicketXPO.SubCategory != null) ? TicketXPO.SubCategory.Name : "Unnassigned";
            _ticketDTO.ThirdLevelCategoryID = (TicketXPO.ThirdLevelCategory != null) ? TicketXPO.ThirdLevelCategory.Oid : 0;
            _ticketDTO.ThirdLevelCategoryName = (TicketXPO.ThirdLevelCategory != null) ? TicketXPO.ThirdLevelCategory.Name : "Unnassigned";
            _ticketDTO.SupportGroupID = (TicketXPO.SupportGroup != null) ? TicketXPO.SupportGroup.Oid : 0;
            _ticketDTO.SupportGroupName = (TicketXPO.SupportGroup != null) ? TicketXPO.SupportGroup.Name : "Unnassigned";
            _ticketDTO.AddedDate = (TicketXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.AddedDate : (DateTime?)null;
            _ticketDTO.ClosedByID = (TicketXPO.ClosedBy != null) ? TicketXPO.ClosedBy.Oid : 0;
            _ticketDTO.ClosedByName = (TicketXPO.ClosedBy != null) ? TicketXPO.ClosedBy.Name : "Unnassigned";
            _ticketDTO.ClosedDate = (TicketXPO.ClosedDate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.ClosedDate : (DateTime?)null;
            _ticketDTO.LastUpdate = (TicketXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.LastUpdate : (DateTime?)null;
            _ticketDTO.LastUpdateByID = (TicketXPO.LastUpdateBy != null) ? TicketXPO.LastUpdateBy.Oid : 0;
            _ticketDTO.LastUpdateByName = (TicketXPO.LastUpdateBy != null) ? TicketXPO.LastUpdateBy.Name : "Unnassigned";
            _ticketDTO.IsActive = TicketXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _ticketDTO;
    }

    public static TicketXPO DTOtoXPO(TicketDTO TicketDTO, UnitOfWork UnitOfWork)
    {
        TicketXPO _ticketXPO;
        try
        {
            _ticketXPO = TicketDTO.ID == null || TicketDTO.ID == 0 ? new TicketXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<TicketXPO>(TicketDTO.ID);
            _ticketXPO.Title = _ticketXPO.Title == TicketDTO.Title ? _ticketXPO.Title : TicketDTO.Title;
            _ticketXPO.Description = _ticketXPO.Description == TicketDTO.Description ? _ticketXPO.Description : TicketDTO.Description;
            _ticketXPO.Solution = _ticketXPO.Solution == TicketDTO.Solution ? _ticketXPO.Solution : TicketDTO.Solution;
            _ticketXPO.Resolution = _ticketXPO.Resolution == TicketDTO.Resolution ? _ticketXPO.Resolution : TicketDTO.Resolution;
            _ticketXPO.TicketNumber = (_ticketXPO.TicketNumber != null && _ticketXPO.TicketNumber != 0) ? _ticketXPO.TicketNumber : TicketDTO.TicketNumber;
            _ticketXPO.CreatedBy = (_ticketXPO.CreatedBy?.Oid == TicketDTO.CreatedByID) ? _ticketXPO.CreatedBy : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.CreatedByID);
            _ticketXPO.Facility = (_ticketXPO.Facility?.Oid == TicketDTO.FacilityID) ? _ticketXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(TicketDTO.FacilityID);
            _ticketXPO.Department = (_ticketXPO.Department?.Oid == TicketDTO.DepartmentID) ? _ticketXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(TicketDTO.DepartmentID);
            _ticketXPO.AssignedTo = (_ticketXPO.AssignedTo?.Oid == TicketDTO.AssignedToID) ? _ticketXPO.AssignedTo : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.AssignedToID);
            _ticketXPO.AssignedDate = _ticketXPO.AssignedDate == TicketDTO.AssignedDate ? _ticketXPO.AssignedDate : TicketDTO.AssignedDate;
            _ticketXPO.Note = _ticketXPO.Note == TicketDTO.Note ? _ticketXPO.Note : TicketDTO.Note;
            _ticketXPO.Item_Line = (_ticketXPO.Item_Line?.Oid == TicketDTO.Item_LineID) ? _ticketXPO.Item_Line : UnitOfWork.GetObjectByKey<Item_LineXPO>(TicketDTO.Item_LineID);
            _ticketXPO.Status = (_ticketXPO.Status?.Oid == TicketDTO.StatusID) ? _ticketXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(TicketDTO.StatusID);
            _ticketXPO.Priority = (_ticketXPO.Priority?.Oid == TicketDTO.PriorityID) ? _ticketXPO.Priority : UnitOfWork.GetObjectByKey<PriorityXPO>(TicketDTO.PriorityID);
            _ticketXPO.Category = (_ticketXPO.Category?.Oid == TicketDTO.CategoryID) ? _ticketXPO.Category : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.CategoryID);
            _ticketXPO.SubCategory = (_ticketXPO.SubCategory?.Oid == TicketDTO.SubCategoryID) && (TicketDTO.SubCategoryID != null) ? _ticketXPO.SubCategory : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.SubCategoryID);
            _ticketXPO.ThirdLevelCategory = (_ticketXPO.ThirdLevelCategory?.Oid == TicketDTO.ThirdLevelCategoryID) && (TicketDTO.ThirdLevelCategoryID != null) ? _ticketXPO.ThirdLevelCategory : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.ThirdLevelCategoryID);
            _ticketXPO.SupportGroup = (_ticketXPO.SupportGroup?.Oid == TicketDTO.SupportGroupID) ? _ticketXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(TicketDTO.SupportGroupID);
            _ticketXPO.AddedDate = _ticketXPO.AddedDate ?? TicketDTO.AddedDate;
            _ticketXPO.ClosedBy = (_ticketXPO.ClosedBy?.Oid == TicketDTO.ClosedByID) ? _ticketXPO.ClosedBy : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.ClosedByID);
            _ticketXPO.ClosedDate = _ticketXPO.ClosedDate == TicketDTO.ClosedDate ? _ticketXPO.ClosedDate : TicketDTO.ClosedDate;
            _ticketXPO.LastUpdate = _ticketXPO.LastUpdate == TicketDTO.LastUpdate ? _ticketXPO.LastUpdate : TicketDTO.LastUpdate;
            _ticketXPO.LastUpdateBy = (_ticketXPO.LastUpdateBy?.Oid == TicketDTO.LastUpdateByID) ? _ticketXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.LastUpdateByID);
            _ticketXPO.IsActive = _ticketXPO.IsActive == TicketDTO.IsActive ? (bool)_ticketXPO.IsActive : (bool)TicketDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _ticketXPO;
    }
}
