using CTSTools.BLL.Common.Files;
using System.Collections.Generic;

namespace CTSTools.BLL.Common.Directories;

public class DirectoryDTO
{
    #region Base Properties
    public string Name { get; set; }
    public string OriginURL { get; set; }
    public string PreviousURL { get; set; }
    public List<FileDTO> FileList { get; set; }
    public string[] DocumentList { get; set; }
    public int? FileDirectory { get; set; }
    #endregion
    #region Constructor
    public DirectoryDTO()
    {
        this.FileList = new List<FileDTO>();
    }
    #endregion
}
