using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePart;

public class SparePart_Validator
{
    public static ValidationResultDTO CreateSparePart_Validation(SparePartDTO SparePartDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(SparePartDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart)}{nameof(SparePartDTO.Name)}",
                });
            }
            else
            {
                var _sparePartDTO = SparePart_Service.GetSparePartList_Global(
                    new SparePartDTO
                    {
                        Name = SparePartDTO.Name.Trim()
                    }).FirstOrDefault();
                if (_sparePartDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Already exist ",
                        Description = " Please, verify the information ",
                        Data = $"{nameof(SparePart)}{nameof(SparePartDTO.Name)}",
                    });
                }
            }
            if (string.IsNullOrEmpty(SparePartDTO.ManufactureID))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ManufactureID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart)}{nameof(SparePartDTO.ManufactureID)}",
                });
            }

            //if (SparePartDTO.FileDTO?.Data == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Picture Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}

            if (SparePartDTO.AddedByID == null || SparePartDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateSparePart_Validation(SparePartDTO SparePartDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartDTO.ID == null || SparePartDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(SparePartDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart)}{nameof(SparePartDTO.Name)}",
                });
            }
            else
            {
                var _sparePartDTO = SparePart_Service.GetSparePartList_Global(
                   new SparePartDTO
                   {
                       Name = SparePartDTO.Name.Trim()
                   }).FirstOrDefault();
                if (_sparePartDTO != null)
                {
                    if (_sparePartDTO?.ID != SparePartDTO.ID)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Name Already exist",
                            Description = " Please, verify the information ",
                            Data = $"{nameof(SparePart)}{nameof(SparePartDTO.Name)}",
                        });
                    }
                }
            }
            if (string.IsNullOrEmpty(SparePartDTO.ManufactureID))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ManufactureID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart)}{nameof(SparePartDTO.ManufactureID)}",
                });
            }

            if (SparePartDTO.LastUpdateByID == null || SparePartDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteSparePart_Validation(SparePartDTO SparePartDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartDTO.ID == null || SparePartDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
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
}
