using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_HeaderDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Model { get; set; }
    //public bool? IsESD { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? SupportGroupID { get; set; }
    public int? Item_SupportGroupID { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }


    #endregion

    #region Extended Properties
    public int? BrandID { get; set; }
    public string BrandName { get; set; }
    public BrandDTO BrandDTO { get; set; }
    public int?[] BrandIDArray { get; set; }
    public bool GetBrandDTO { get; set; }
    public int?[] Item_HeaderIDArray { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public FileDTO FileDTO { get; set; }
    public bool GetItemHeaderPicture { get; set; }
    public string ItemImg { get; set; }
    public string Names { get; set; }
    public string NamesWithModel { get; set; }
    public int?[] UserDefinedIDArray { get; set; }

    #endregion

    #region Constructor
    public Item_HeaderDTO()
    {
        BrandDTO = new BrandDTO();
        BrandIDArray = new int?[] { };
        Item_HeaderIDArray = new int?[] { };
        SupportGroupIDArray = new int?[] { };
        UserDefinedIDArray = new int?[] { };
    }
    #endregion
}
