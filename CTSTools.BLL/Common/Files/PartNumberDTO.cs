using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Files
{
    public class PartNumberDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public PartNumberDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };

        }
        #endregion
    }
}
