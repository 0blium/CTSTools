using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Quality.QMS.Document;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevisionDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Revision { get; set; }
    public int? StatusID { get; set; }
    public string StatusName { get; set; }
    public int? DocumentID { get; set; }
    public string DocumentName { get; set; }
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
    public Dictionary<int?, DocumentDTO> DocumentDict { get; set; }
    public bool GetDocumentDTO { get; set; }
    public int?[] DocumentIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public Dictionary<int?, StatusDTO> StatusDict  { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public bool GetFileDTO { get; set; }
    public FileDTO FileDTO { get; set; }
    #endregion

    #region Constructor
    public DocumentRevisionDTO()
    {
        DocumentRevisionIDArray = [];
        DocumentDTO = new DocumentDTO();
        DocumentIDArray = [];
        StatusDTO = new StatusDTO();
        StatusIDArray = [];
        FileDTO = new FileDTO();
        DocumentDict = [];
        StatusDict = [];
    }
    #endregion
}
