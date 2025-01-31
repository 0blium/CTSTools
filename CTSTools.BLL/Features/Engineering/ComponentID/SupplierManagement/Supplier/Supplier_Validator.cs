using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class Supplier_Validator
{
    public static ValidationResultDTO CreateSupplier_Validation(SupplierDTO SupplierDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(SupplierDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Supplier)}{nameof(SupplierDTO.Name)}",
                });
            }

            if (SupplierDTO.AddedByID == null || SupplierDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Added By Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (SupplierDTO.IsManufacturer == false && SupplierDTO.IsVendor == false)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Supplier must be vendor or manufacturer ",
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
            var _supplierDTO = new SupplierDTO { Name = SupplierDTO.Name };
            var _sameSupplierDTO = Supplier_Service.GetSupplierList_Global(_supplierDTO).FirstOrDefault();
            if (_sameSupplierDTO != null)
            {
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Duplicated",
                    Description = "There is an attribute with the same name"
                };
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
    public static ValidationResultDTO UpdateSupplier_Validation(SupplierDTO SupplierDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SupplierDTO.ID == null || SupplierDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(SupplierDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field is Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Supplier)}{nameof(SupplierDTO.Name)}",
                });
            }
            if (SupplierDTO.LastUpdateByID == null || SupplierDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Last Update By is Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            var _supplierDTO = new SupplierDTO { Name = SupplierDTO.Name };
            var _sameSupplierDTO = Supplier_Service.GetSupplierList_Global(_supplierDTO)
                                                   .Where(w => w.ID != SupplierDTO.ID)
                                                   .FirstOrDefault();
            if (_sameSupplierDTO != null)
            {
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Duplicated",
                    Description = "There is an attribute with the same name."
                };
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
    public static ValidationResultDTO DeleteSupplier_Validation(SupplierDTO SupplierDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SupplierDTO.ID == null || SupplierDTO.ID == 0)
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
    #region Excel Supplier Validation
    public static ValidationResultDTO ExcelSupplierRows_Validation(SupplierDTO SupplierDTO)
    {
        var _excelRowDTO = new ExcelRowDTO 
        {
            GoodRowLinesList = new List<SupplierDTO>(),
            BadRowLinesList = new List<SupplierDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            SupplierDTO _supplierDTO = new SupplierDTO
            {
                ID = SupplierDTO.ID,
                Name = !string.IsNullOrEmpty(SupplierDTO.Name) ? SupplierDTO.Name : "Error, The name is null or empty",
                IsManufacturer = SupplierDTO.IsManufacturer,
                IsVendor = SupplierDTO.IsVendor,
                IsActive = true
            };

            if (_supplierDTO.Name.StartsWith("Error"))
                isSucces = false;
            if (_supplierDTO.IsManufacturer == false && _supplierDTO.IsVendor == false) 
            {
                _supplierDTO.Description = "Error, Supplier must be vendor or manufacturer";
                isSucces = false;
            }

            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_supplierDTO);
            else
                _excelRowDTO.BadRowLinesList.Add(_supplierDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO ExcelSupplierInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<SupplierDTO>(),
            BadRowLinesList = new List<SupplierDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            var _supplierDTOList = ExcelRowDTO.GoodRowLinesList as List<SupplierDTO> ?? new List<SupplierDTO>();
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            var _newSupplierList = _supplierDTOList.GroupBy(supplier => supplier.Name?.ToLower()).Select(group => group.First()).ToList();

            // We normalize names to lowercase only once
            var _supplierDTO = new SupplierDTO { SupplierNameArray = _newSupplierList.Select(SupplierDTO => SupplierDTO.Name?.ToLower()).ToArray() };

            // Go for the data in the db
            var _supplierList = Supplier_Service.GetSupplierList_Global(_supplierDTO);

            // We create dictionaries with normalized keys (in lowercase)
            var _supplierDict = _supplierList.ToDictionary(SupplierDTO => SupplierDTO.Name.ToLower(), SupplierDTO => (int?)SupplierDTO.ID);

            foreach (var SupplierDTO in _newSupplierList)
            {
                bool isSuccess = true;

                // We search the normalized dictionaries without using `ToLower()` on each iteration
                if (_supplierDict.ContainsKey(SupplierDTO.Name?.ToLower() ?? ""))
                {
                    var _name = SupplierDTO.Name;
                    SupplierDTO.Name = $"Error: The Name: {_name}, already exists"; 
                    isSuccess = false; 
                }

                if (isSuccess)
                {
                    SupplierDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(SupplierDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(SupplierDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw;
        }
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion
}
