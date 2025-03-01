using CTSTools.BLL.Features.Ticket.Item.DataType;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.UserDefined;

public class UserDefinedDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public bool? IsMandatory { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    public string NameWithDataType { get; set; }

    #endregion

    #region Extended Properties

    public int?[] UserDefinedIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public DataTypeDTO DataTypeDTO { get; set; }
    public bool GetDataTypeDTO { get; set; }
    public int?[] DataTypeIDArray { get; set; }

    #endregion
    #region Constructor
    public UserDefinedDTO()
    {
        UserDefinedIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };
        DataTypeDTO = new DataTypeDTO();
        DataTypeIDArray = new int?[] { };

    }
    #endregion
}
