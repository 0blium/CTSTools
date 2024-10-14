using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsible_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDepartmentResponsible_Global(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = DepartmentResponsible_Validator.CreateDepartmentResponsible_Validation(DepartmentResponsibleDTO);
        if (_ValidationResultDTO.Result)
        {
            DepartmentResponsibleDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DepartmentResponsible_Repository.CreateDepartmentResponsible(DepartmentResponsibleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDepartmentResponsible_Global(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = DepartmentResponsible_Validator.UpdateDepartmentResponsible_Validation(DepartmentResponsibleDTO);
        if (_ValidationResultDTO.Result)
        {
            DepartmentResponsibleDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DepartmentResponsible_Repository.UpdateDepartmentResponsible(DepartmentResponsibleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDepartmentResponsible_Global(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = DepartmentResponsible_Validator.DeleteDepartmentResponsible_Validation(DepartmentResponsibleDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DepartmentResponsible_Repository.DeleteDepartmentResponsible(DepartmentResponsibleDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DepartmentResponsibleDTO> GetDepartmentResponsibleList_Global(DepartmentResponsibleDTO DepartmentResponsibleDTO, PagedResultDTO<DepartmentResponsibleDTO> PagedResultDTO = null)
    {
        var _departmentResponsibleglobalList = new List<DepartmentResponsibleDTO>();
        try
        {
            var _departmentList = DepartmentResponsible_Repository.GetDepartmentList(DepartmentResponsibleDTO, PagedResultDTO);
            // if Department is empty, return list
            if (_departmentList.Count() == 0)
            {
                _departmentResponsibleglobalList = _departmentList;
                return _departmentResponsibleglobalList;
            }
            if (!DepartmentResponsibleDTO.GetDepartmentDTO && !DepartmentResponsibleDTO.GetResponsibleDTO)
            {
                _departmentResponsibleglobalList = _departmentList;
                return _departmentResponsibleglobalList;
            }
            //_departmentResponsibleglobalList = GetDepartmentRelatedData(DepartmentDTO, _departmentList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentResponsibleglobalList;
    }
    //public static List<DepartmentDTO> GetDepartmentRelatedData(DepartmentDTO DepartmentDTO, List<DepartmentDTO> DepartmentList)
    //{
    //    var _departmentglobalList = new List<DepartmentDTO>();
    //    var _facilityDict = new Dictionary<int?, FacilityDTO>();
    //    var _businessunitDict = new Dictionary<int?, BusinessUnitDTO>();

    //    try
    //    {
    //        if (DepartmentDTO.GetFacilityDTO)
    //        {
    //            DepartmentDTO.FacilityDTO.FacilityIDArray = DepartmentList.GroupBy(g => g.FacilityDTO.ID)
    //                    .Select(s => s.Key)
    //                    .ToArray();

    //            _facilityDict = Facility_Service.GetFacilityList_Global(DepartmentDTO.FacilityDTO)
    //                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
    //        }
    //        if (DepartmentDTO.GetBusinessUnitDTO)
    //        {
    //            DepartmentDTO.BusinessUnitDTO.BusinessUnitIDArray = DepartmentList.GroupBy(g => g.BusinessUnitDTO.ID)
    //                    .Select(s => s.Key)
    //                    .ToArray();

    //            _businessunitDict = BusinessUnit_Service.GetBusinessUnitList_Global(DepartmentDTO.BusinessUnitDTO)
    //                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
    //        }
    //        foreach (var _departmentDTO in DepartmentList)
    //        {
    //            if (DepartmentDTO.GetFacilityDTO && _facilityDict.ContainsKey(_departmentDTO.FacilityDTO.ID))
    //            {
    //                _departmentDTO.FacilityDTO = _facilityDict[_departmentDTO.FacilityDTO.ID];
    //            }
    //            if (DepartmentDTO.GetBusinessUnitDTO && _businessunitDict.ContainsKey(_departmentDTO.BusinessUnitDTO.ID))
    //            {
    //                _departmentDTO.BusinessUnitDTO = _businessunitDict[_departmentDTO.BusinessUnitDTO.ID];
    //            }
    //            _departmentglobalList.Add(_departmentDTO);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return _departmentglobalList;
    //}
    public static int GetDepartmentTotalCount(PagedResultDTO<DepartmentResponsibleDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DepartmentResponsible_Repository.GetDepartmentResponsibleCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion
    #region Business Logic
    public static ValidationResultDTO DeleteUnselectDepartmentResponsible(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _departmentResponsibleList = GetUnSelectDepartment_Responsibles(DepartmentDTO);
            foreach (var _departmentResponsibleObj in _departmentResponsibleList)
            {
                DeleteDepartmentResponsible_Global(new DepartmentResponsibleDTO { ID=_departmentResponsibleObj.ID });
            }
        }
        catch(Exception ex)
        {

        }
        return _validationResultDTO;
    }
    public static List<DepartmentResponsibleDTO> GetUnSelectDepartment_Responsibles(DepartmentDTO DepartmentDTO)
    {
        var _departmentResponsiblesList = new List<DepartmentResponsibleDTO>();
        try
        {
            using (var _session = XPO_Helper.GetNewSession())
            {
                var _departmentResponsibleXPQuery = new XPQuery<DepartmentResponsibleXPO>(_session);
                _departmentResponsiblesList = (from
                                                    f in _departmentResponsibleXPQuery
                                                where DepartmentDTO.ResponsiblesIDArray.Contains(f.Responsible.Oid) != true &&
                                                    f.Department.Oid == DepartmentDTO.ID
                                                select
                                                       new DepartmentResponsibleDTO { ID = f.Oid }).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentResponsiblesList;
    }
    public static ValidationResultDTO DeleteDepartment_ResponsiblesByDepartment(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _department_ResponsibleList = GetDepartmentResponsibleList_Global(new DepartmentResponsibleDTO { DepartmentDTO = { ID=(int)DepartmentDTO.ID } });
            foreach(var _departmentResponsibleDTO in _department_ResponsibleList)
            {
                _validationResultDTO = DeleteDepartmentResponsible_Global(_departmentResponsibleDTO);
            }
        }
        catch(Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _validationResultDTO;
    }
    #endregion
}
