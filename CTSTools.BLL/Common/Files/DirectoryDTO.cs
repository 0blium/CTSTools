using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Files
{
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
}
