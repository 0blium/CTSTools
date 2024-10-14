using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Users.ActiveDirectory
{
    public class ActiveDirectoryDTO
    {
        #region Base Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public string Title { get; set; }
        public string Domain { get; set; }
        public string Fullname { get; set; }
        public byte[] Picture { get; set; }
        public ValidationResultDTO Validation_ResultDTO { get; set; }
        #endregion
        #region Constructor
        public ActiveDirectoryDTO()
        {
            Email = "Empty";
            Fullname = "Empty";
            Title = "Empty";
            Validation_ResultDTO = new ValidationResultDTO();
        }
        #endregion
    }
}
