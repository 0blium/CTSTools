using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Common.Files;

public class FileDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public string Data { get; set; }
    public string Icon { get; set; }
    public string URL { get; set; }
    public string MIMEType { get; set; }
    public string FileName { get; set; }
    public int? FileDirectory { get; set; }
    #endregion
    #region Extended Properties
    public string TreeViewID { get; set; }
    public int? ParentID { get; set; }
    public string[] DirectoryArray { get; set; }
    public List<FileDTO> FileList { get; set; }
    #endregion

    public FileDTO()
    {
        DirectoryArray = [];
        FileList = [];
    }

}
