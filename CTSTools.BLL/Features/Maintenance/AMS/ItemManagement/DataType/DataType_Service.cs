using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.DataType;

public class DataType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDataType_Global(DataTypeDTO DataTypeDTO)
    {
        var _ValidationResultDTO = DataType_Validator.CreateDataType_Validation(DataTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DataTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DataType_Repository.CreateDataType(DataTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDataType_Global(DataTypeDTO DataTypeDTO)
    {
        var _ValidationResultDTO = DataType_Validator.UpdateDataType_Validation(DataTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DataTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DataType_Repository.UpdateDataType(DataTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDataType_Global(DataTypeDTO DataTypeDTO)
    {
        var _ValidationResultDTO = DataType_Validator.DeleteDataType_Validation(DataTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DataType_Repository.DeleteDataType(DataTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DataTypeDTO> GetDataTypeList_Global(DataTypeDTO DataTypeDTO, PagedResultDTO<DataTypeDTO> PagedResultDTO = null)
    {
        var _datatypeglobalList = new List<DataTypeDTO>();
        try
        {
            var _datatypeList = DataType_Repository.GetDataTypeList(DataTypeDTO, PagedResultDTO);
            // if DataType is empty, return list
            _datatypeglobalList = _datatypeList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _datatypeglobalList;
    }

    public static int GetDataTypeTotalCount(PagedResultDTO<DataTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = PagedResultDTO.dxFilters != null ? PagedResultDTO.DataList.Count : DataType_Repository.GetDataTypeCount(PagedResultDTO.Filter);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}

