using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.Tickets.Category;
using CTSTools.BLL.Features.Maintenance.AMS.Tickets.Priority;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Ticket;

public class TicketDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Solution { get; set; }
    public string Resolution { get; set; }
    public int TicketNumber { get; set; }
    public DateTime? AssignedDate { get; set; }
    public string Note { get; set; }
    public DateTime? AddedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public string RequestorEmail { get; set; }
    public int?[] TicketIDArray { get; set; }
    public UserDTO CreatedByDTO { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public UserDTO AssignedToDTO { get; set; }
    public Item_LineDTO Item_LineDTO { get; set; }
    public bool GetItem_LineDTO { get; set; }
    public int?[] Item_LineIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public PriorityDTO PriorityDTO { get; set; }
    public bool GetPriorityDTO { get; set; }
    public int?[] PriorityIDArray { get; set; }
    public CategoryDTO CategoryDTO { get; set; }
    public bool GetCategoryDTO { get; set; }
    public int?[] CategoryIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public UserDTO ClosedByDTO { get; set; }

    public CategoryDTO SubCategoryDTO { get; set; }
    public CategoryDTO ThirdLevelCategoryDTO { get; set; }
    public FileDTO FileDTO { get; set; }
    #region Filters
    public DateTime StartAddedDate { get; set; }
    public DateTime EndAddedDate { get; set; }
    #endregion
    #endregion
    #region Constructor
    public TicketDTO()
    {
        TicketIDArray = new int?[] { };
        CreatedByDTO = new UserDTO();
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = new int?[] { };
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = new int?[] { };
        AssignedToDTO = new UserDTO();
        Item_LineDTO = new Item_LineDTO();
        Item_LineIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
        PriorityDTO = new PriorityDTO();
        PriorityIDArray = new int?[] { };
        CategoryDTO = new CategoryDTO();
        CategoryIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };
        ClosedByDTO = new UserDTO();
        SubCategoryDTO = new CategoryDTO();
        ThirdLevelCategoryDTO = new CategoryDTO();

    }
    #endregion
}

