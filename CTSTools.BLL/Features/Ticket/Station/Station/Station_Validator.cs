using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Ticket.Item.Item_Line;
using CTSTools.DAL.Features.Ticket.Item;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.Station;

public class Station_Validator
{
    public static ValidationResultDTO CreateStation_Validation(StationDTO StationDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(StationDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.Name)}",
                });
            }
            //if (string.IsNullOrEmpty(StationDTO.Serial) )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Serial Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(Station)}{nameof(StationDTO.Serial)}", 
            //    });
            //}
            if (StationDTO.FacilityDTO.ID == null || StationDTO.FacilityDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.FacilityDTO)}",
                });
            }
            if (StationDTO.DepartmentDTO.ID == null || StationDTO.DepartmentDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.DepartmentDTO)}",
                });
            }
            if (StationDTO.StationTypeDTO.ID == null || StationDTO.StationTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "StationType Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.StationTypeDTO)}",
                });
            }

            if (StationDTO.AddedByID == null || StationDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO UpdateStation_Validation(StationDTO StationDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (StationDTO.ID == null || StationDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(StationDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.Name)}",
                });
            }
            if (string.IsNullOrEmpty(StationDTO.Serial))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Serial Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.Serial)}",
                });
            }
            if (StationDTO.FacilityDTO.ID == null || StationDTO.FacilityDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.FacilityDTO)}",
                });
            }
            if (StationDTO.DepartmentDTO.ID == null || StationDTO.DepartmentDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.DepartmentDTO)}",
                });
            }
            if (StationDTO.StationTypeDTO.ID == null || StationDTO.StationTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "StationType Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Station)}{nameof(StationDTO.StationTypeDTO)}",
                });
            }

            if (StationDTO.LastUpdateByID == null || StationDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteStation_Validation(StationDTO StationDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (StationDTO.ID == null || StationDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            else
            {
                //validate if stations have items
                var _item_lineList = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { StationDTO = StationDTO });
                if (_item_lineList.Count() > 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "the station has items",
                        Description = " Please, verify information ",
                    });
                }
            }


            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
}
