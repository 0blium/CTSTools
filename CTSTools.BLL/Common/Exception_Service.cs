using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common
{
    public class Exception_Service
    {
        #region SQL relation error format
        public static ValidationResultDTO GetErrorTypeByMessage(string Message)
        {
            var _validation_ResultDTO = new ValidationResultDTO();
            // 1. Check message error contains "table" and ", column"
            if (Message.Contains("table") && Message.Contains(", column"))
            {
                // 2. Extract the text by the indices of both words
                string _result = string.Empty;
                int StartIndex = Message.IndexOf("constraint ", 0) + "constraint ".Length;
                int EndIndex = Message.IndexOf(". The", StartIndex);
                _result = Message.Substring(StartIndex, EndIndex - StartIndex).Trim();
                // 3. check which word contains the _result
                switch (_result)
                {
                    case "\"FK_UserDefinedTemplate_Item_Header\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To delete the record, you must first remove the custom fields from the item.";
                        break;
                    case "\"FK_UserDefinedTemplate_Item_SupportGroup\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To delete the record, you must first remove the custom fields from the item.";
                        break;
                    case "\"FK_UserDefinedTemplate_UserDefined\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To delete the record, the custom field must not be assigned to any item.";
                        break;
                    case "\"FK_UserDefinedValue_UserDefined\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error";
                        _validation_ResultDTO.Description = "You can't delete a custom field that has already been linked to a item.";
                        break;
                    case "\"FK_Item_Line_Item_Header\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To remove the record, the individual items must first be removed from the item.";
                        break;
                    case "\"FK_UserDefinedValue_Item_Line\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To delete the record, you must first map the related custom fields again.";
                        break;
                    //case "\"FK_Category_Parent\"":
                    //    _validation_ResultDTO.Result = false;
                    //    _validation_ResultDTO.Message = "Error!";
                    //    _validation_ResultDTO.Description = "To delete the record, you must first remove the related subcategories.";
                    //    break;
                    case "\"FK_Item_Line_Station\"":
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = "To delete the record, you must first remove the items that are assigned to this station.";
                        break;
                    //case "\"FK_Ticket_Category\"":
                    //    _validation_ResultDTO.Result = false;
                    //    _validation_ResultDTO.Message = "Error!";
                    //    _validation_ResultDTO.Description = "You cannot delete a category that has already been assigned to a ticket.";
                    //    break;
                    //case "\"FK_Station_Station_Type\"":
                    //    _validation_ResultDTO.Result = false;
                    //    _validation_ResultDTO.Message = "Error!";
                    //    _validation_ResultDTO.Description = "To delete the record, the station type must not be assigned to any station.";
                    //    break;
                    default:
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error!";
                        _validation_ResultDTO.Description = string.Format("Relationships exist in the registry, contact an administrator {0}", Message);
                        break;
                }
            }
            return _validation_ResultDTO;
        }
        #endregion
    }
}
