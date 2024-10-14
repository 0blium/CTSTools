using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;
namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class Department_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDepartment_Global(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = Department_Validator.CreateDepartment_Validation(DepartmentDTO);
        if (_ValidationResultDTO.Result)
        {
            DepartmentDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Department_Repository.CreateDepartment(DepartmentDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDepartment_Global(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = Department_Validator.UpdateDepartment_Validation(DepartmentDTO);
        if (_ValidationResultDTO.Result)
        {
            DepartmentDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Department_Repository.UpdateDepartment(DepartmentDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDepartment_Global(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = Department_Validator.DeleteDepartment_Validation(DepartmentDTO);
        if(_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DepartmentResponsible_Service.DeleteDepartment_ResponsiblesByDepartment(DepartmentDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Department_Repository.DeleteDepartment(DepartmentDTO);
        }
        
        return _ValidationResultDTO;
    }
    public static List<DepartmentDTO> GetDepartmentList_Global(DepartmentDTO DepartmentDTO, PagedResultDTO<DepartmentDTO> PagedResultDTO = null)
    {
        var _departmentglobalList = new List<DepartmentDTO>();
        try
        {
            var _departmentList = Department_Repository.GetDepartmentList(DepartmentDTO, PagedResultDTO);
            // if Department is empty, return list
            if (_departmentList.Count() == 0)
            {
                _departmentglobalList = _departmentList;
                return _departmentglobalList;
            }
            if (!DepartmentDTO.GetFacilityDTO && !DepartmentDTO.GetDepartmentResponsibleList)
            {
                _departmentglobalList = _departmentList;
                return _departmentglobalList;
            }
            _departmentglobalList = GetDepartmentRelatedData(DepartmentDTO, _departmentList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentglobalList;
    }
    public static List<DepartmentDTO> GetDepartmentRelatedData(DepartmentDTO DepartmentDTO, List<DepartmentDTO> DepartmentList)
    {
        var _departmentglobalList = new List<DepartmentDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _departmentResponsible = new Dictionary<int, List<DepartmentResponsibleDTO>>();
        try
        {
            if (DepartmentDTO.GetFacilityDTO)
            {
                DepartmentDTO.FacilityDTO.FacilityIDArray = DepartmentList.GroupBy(g => g.FacilityDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(DepartmentDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DepartmentDTO.GetDepartmentResponsibleList)
            {
                var _departmentResponsibleDTO = new DepartmentResponsibleDTO();
                _departmentResponsibleDTO.DepartmentIDArray = DepartmentList.GroupBy(g => g.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _departmentResponsible = DepartmentResponsible_Service.GetDepartmentResponsibleList_Global(_departmentResponsibleDTO).GroupBy(m => m.DepartmentID).ToDictionary(group => group.Key,group => group.ToList());
            }
            foreach (var _departmentDTO in DepartmentList)
            {
                if (DepartmentDTO.GetFacilityDTO && _facilityDict.ContainsKey(_departmentDTO.FacilityDTO.ID))
                {
                    _departmentDTO.FacilityDTO = _facilityDict[_departmentDTO.FacilityDTO.ID];
                }
                if(DepartmentDTO.GetDepartmentResponsibleList && _departmentResponsible.ContainsKey((int)_departmentDTO.ID))
                {
                    _departmentDTO.Department_ResponsibleList = _departmentResponsible[(int)_departmentDTO.ID];
                    foreach(var _responsible in _departmentDTO.Department_ResponsibleList)
                    {
                        _departmentDTO.ResponsibleNames += _responsible.ResponsibleName + ", ";
                    }
                    
                }
                _departmentglobalList.Add(_departmentDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentglobalList;
    }
    public static int GetDepartmentTotalCount(PagedResultDTO<DepartmentDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Department_Repository.GetDepartmentCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion
    #region Business Logic
    public static ValidationResultDTO CreateMultipleDepartmentResponsibleByArray(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            if (DepartmentDTO.ResponsiblesIDArray.Length > 0)
            {
                foreach (var _responsibleID in DepartmentDTO.ResponsiblesIDArray)
                {
                    var _departmentResponsibleDTO = new DepartmentResponsibleDTO
                    {
                        DepartmentDTO =
                        {
                            ID=DepartmentDTO.ID,
                        },
                        ResponsibleDTO =
                        {
                            ID=_responsibleID,
                        },
                        AddedByID = (int)DepartmentDTO.AddedByID,
                        AddedDate = DateTime.Now
                    };
                    _validationResultDTO = DepartmentResponsible_Repository.CreateDepartmentResponsible(_departmentResponsibleDTO);
                }
                DepartmentResponsible_Service.DeleteUnselectDepartmentResponsible(DepartmentDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = string.Format("There was an error trying to save the fields of Department_responsibles. {0}", ex.Message);
        }

        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateDepartmentWithResponsibles(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = CreateDepartment_Global(DepartmentDTO);
        }
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = CreateMultipleDepartmentResponsibleByArray(DepartmentDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDepartmentWithResponsibles(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = UpdateDepartment_Global(DepartmentDTO);
        }
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = CreateMultipleDepartmentResponsibleByArray(DepartmentDTO);
        }
        return _validationResultDTO;
    }
    #endregion
}
