using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common
{
    public class ValidationResultDTO
    {
        public int ID { get; set; }
        public bool Result { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int ErrorCode { get; set; }
        public List<ValidationResultDTO> ValidationResultList { get; set; }
        public ValidationResultDTO()
        {
            Result = true;
            Message = "Success";
            Description = "";
            ValidationResultList = new List<ValidationResultDTO>();
        }

    }
}
