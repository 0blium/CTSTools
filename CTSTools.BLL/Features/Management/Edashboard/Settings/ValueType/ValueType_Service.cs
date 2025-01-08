using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;

public class ValueType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateValueType_Global(ValueTypeDTO ValueTypeDTO)
    {
        var _ValidationResultDTO = ValueType_Validator.CreateValueType_Validation(ValueTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            ValueTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = ValueType_Repository.CreateValueType(ValueTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateValueType_Global(ValueTypeDTO ValueTypeDTO)
    {
        var _ValidationResultDTO = ValueType_Validator.UpdateValueType_Validation(ValueTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            ValueTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = ValueType_Repository.UpdateValueType(ValueTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteValueType_Global(ValueTypeDTO ValueTypeDTO)
    {
        var _ValidationResultDTO = ValueType_Validator.DeleteValueType_Validation(ValueTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = ValueType_Repository.DeleteValueType(ValueTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ValueTypeDTO> GetValueTypeList_Global(ValueTypeDTO ValueTypeDTO, PagedResultDTO<ValueTypeDTO> PagedResultDTO = null)
    {
        var _valuetypeglobalList = new List<ValueTypeDTO>();
        try
        {
            var _valuetypeList = ValueType_Repository.GetValueTypeList(ValueTypeDTO, PagedResultDTO);
            // if ValueType is empty, return list
            _valuetypeglobalList = _valuetypeList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valuetypeglobalList;
    }




    public static int GetValueTypeTotalCount(PagedResultDTO<ValueTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = ValueType_Repository.GetValueTypeCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
