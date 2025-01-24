using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Level;
using CTSTools.BLL.Features.Quality.QMS.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevisionDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Revision { get; set; }
    public string ChangeReason { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    #endregion

    #region Extended Properties
    public int?[] DocumentRevisionIDArray { get; set; }
    public DocumentDTO DocumentDTO { get; set; }
    public int? DocumentID { get; set; }
    public string DocumentName { get; set; }
    public bool GetDocumentDTO { get; set; }
    public int?[] DocumentIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public int? StatusID { get; set; }
    public string StatusName { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    #endregion

    #region Constructor
    public DocumentRevisionDTO()
    {
        DocumentRevisionIDArray = new int?[] { };
        DocumentDTO = new DocumentDTO();
        DocumentIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
    }
    #endregion
}
