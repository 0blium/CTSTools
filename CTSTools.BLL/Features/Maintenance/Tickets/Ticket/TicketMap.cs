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
            _ticketDTO.CreatedByDTO.ID = (TicketXPO.CreatedBy != null) ? TicketXPO.CreatedBy.Oid : 0;
            _ticketDTO.CreatedByDTO.Name = (TicketXPO.CreatedBy != null) ? TicketXPO.CreatedBy.Name : "Unnassigned";
            _ticketDTO.FacilityDTO.ID = (TicketXPO.Facility != null) ? TicketXPO.Facility.Oid : 0;
            _ticketDTO.FacilityDTO.Name = (TicketXPO.Facility != null) ? TicketXPO.Facility.Name : "Unnassigned";
            _ticketDTO.DepartmentDTO.ID = (TicketXPO.Department != null) ? TicketXPO.Department.Oid : 0;
            _ticketDTO.DepartmentDTO.Name = (TicketXPO.Department != null) ? TicketXPO.Department.Name : "Unnassigned";
            _ticketDTO.AssignedToDTO.ID = (TicketXPO.AssignedTo != null) ? TicketXPO.AssignedTo.Oid : 0;
            _ticketDTO.AssignedToDTO.Name = (TicketXPO.AssignedTo != null) ? TicketXPO.AssignedTo.Name : "Unnassigned";
            _ticketDTO.AssignedDate = (TicketXPO.AssignedDate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.AssignedDate : (DateTime?)null;
            _ticketDTO.Note = TicketXPO.Note;
            _ticketDTO.Item_LineDTO.ID = (TicketXPO.Item_Line != null) ? TicketXPO.Item_Line.Oid : 0;
            _ticketDTO.StatusDTO.ID = (TicketXPO.Status != null) ? TicketXPO.Status.Oid : 0;
            _ticketDTO.StatusDTO.Name = (TicketXPO.Status != null) ? TicketXPO.Status.Name : "Unnassigned";
            _ticketDTO.PriorityDTO.ID = (TicketXPO.Priority != null) ? TicketXPO.Priority.Oid : 0;
            _ticketDTO.PriorityDTO.Name = (TicketXPO.Priority != null) ? TicketXPO.Priority.Name : "Unnassigned";
            _ticketDTO.CategoryDTO.ID = (TicketXPO.Category != null) ? TicketXPO.Category.Oid : 0;
            _ticketDTO.CategoryDTO.Name = (TicketXPO.Category != null) ? TicketXPO.Category.Name : "Unnassigned";
            _ticketDTO.SubCategoryDTO.ID = (TicketXPO.SubCategory != null) ? TicketXPO.SubCategory.Oid : 0;
            _ticketDTO.SubCategoryDTO.Name = (TicketXPO.SubCategory != null) ? TicketXPO.SubCategory.Name : "Unnassigned";
            _ticketDTO.ThirdLevelCategoryDTO.ID = (TicketXPO.ThirdLevelCategory != null) ? TicketXPO.ThirdLevelCategory.Oid : 0;
            _ticketDTO.ThirdLevelCategoryDTO.Name = (TicketXPO.ThirdLevelCategory != null) ? TicketXPO.ThirdLevelCategory.Name : "Unnassigned";
            _ticketDTO.SupportGroupDTO.ID = (TicketXPO.SupportGroup != null) ? TicketXPO.SupportGroup.Oid : 0;
            _ticketDTO.SupportGroupDTO.EnglishName = (TicketXPO.SupportGroup != null) ? TicketXPO.SupportGroup.EnglishName : "Unnassigned";
            _ticketDTO.AddedDate = (TicketXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? TicketXPO.AddedDate : (DateTime?)null;
            _ticketDTO.ClosedByDTO.ID = (TicketXPO.ClosedBy != null) ? TicketXPO.ClosedBy.Oid : 0;
            _ticketDTO.ClosedByDTO.Name = (TicketXPO.ClosedBy != null) ? TicketXPO.ClosedBy.Name : "Unnassigned";
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
            _ticketXPO.CreatedBy = (_ticketXPO.CreatedBy?.Oid == TicketDTO.CreatedByDTO.ID) ? _ticketXPO.CreatedBy : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.CreatedByDTO.ID);
            _ticketXPO.Facility = (_ticketXPO.Facility?.Oid == TicketDTO.FacilityDTO.ID) ? _ticketXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(TicketDTO.FacilityDTO.ID);
            _ticketXPO.Department = (_ticketXPO.Department?.Oid == TicketDTO.DepartmentDTO.ID) ? _ticketXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(TicketDTO.DepartmentDTO.ID);
            _ticketXPO.AssignedTo = (_ticketXPO.AssignedTo?.Oid == TicketDTO.AssignedToDTO.ID) ? _ticketXPO.AssignedTo : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.AssignedToDTO.ID);
            _ticketXPO.AssignedDate = _ticketXPO.AssignedDate == TicketDTO.AssignedDate ? _ticketXPO.AssignedDate : TicketDTO.AssignedDate;
            _ticketXPO.Note = _ticketXPO.Note == TicketDTO.Note ? _ticketXPO.Note : TicketDTO.Note;
            _ticketXPO.Item_Line = (_ticketXPO.Item_Line?.Oid == TicketDTO.Item_LineDTO.ID) ? _ticketXPO.Item_Line : UnitOfWork.GetObjectByKey<Item_LineXPO>(TicketDTO.Item_LineDTO.ID);
            _ticketXPO.Status = (_ticketXPO.Status?.Oid == TicketDTO.StatusDTO.ID) ? _ticketXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(TicketDTO.StatusDTO.ID);
            _ticketXPO.Priority = (_ticketXPO.Priority?.Oid == TicketDTO.PriorityDTO.ID) ? _ticketXPO.Priority : UnitOfWork.GetObjectByKey<PriorityXPO>(TicketDTO.PriorityDTO.ID);
            _ticketXPO.Category = (_ticketXPO.Category?.Oid == TicketDTO.CategoryDTO.ID) ? _ticketXPO.Category : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.CategoryDTO.ID);
            _ticketXPO.SubCategory = (_ticketXPO.SubCategory?.Oid == TicketDTO.SubCategoryDTO?.ID) && (TicketDTO.SubCategoryDTO?.ID != null) ? _ticketXPO.SubCategory : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.SubCategoryDTO.ID);
            _ticketXPO.ThirdLevelCategory = (_ticketXPO.ThirdLevelCategory?.Oid == TicketDTO.ThirdLevelCategoryDTO?.ID) && (TicketDTO.ThirdLevelCategoryDTO?.ID != null) ? _ticketXPO.ThirdLevelCategory : UnitOfWork.GetObjectByKey<CategoryXPO>(TicketDTO.ThirdLevelCategoryDTO.ID);
            _ticketXPO.SupportGroup = (_ticketXPO.SupportGroup?.Oid == TicketDTO.SupportGroupDTO.ID) ? _ticketXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(TicketDTO.SupportGroupDTO.ID);
            _ticketXPO.AddedDate = _ticketXPO.AddedDate ?? TicketDTO.AddedDate;
            _ticketXPO.ClosedBy = (_ticketXPO.ClosedBy?.Oid == TicketDTO.ClosedByDTO.ID) ? _ticketXPO.ClosedBy : UnitOfWork.GetObjectByKey<UserXPO>(TicketDTO.ClosedByDTO.ID);
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
