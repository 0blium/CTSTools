using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.SupplyType;

public class SupplyType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSupplyType_Global(SupplyTypeDTO SupplyTypeDTO)
    {
        var _ValidationResultDTO = SupplyType_Validator.CreateSupplyType_Validation(SupplyTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            SupplyTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = SupplyType_Repository.CreateSupplyType(SupplyTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSupplyType_Global(SupplyTypeDTO SupplyTypeDTO)
    {
        var _ValidationResultDTO = SupplyType_Validator.UpdateSupplyType_Validation(SupplyTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            SupplyTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SupplyType_Repository.UpdateSupplyType(SupplyTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSupplyType_Global(SupplyTypeDTO SupplyTypeDTO)
    {
        var _ValidationResultDTO = SupplyType_Validator.DeleteSupplyType_Validation(SupplyTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SupplyType_Repository.DeleteSupplyType(SupplyTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<SupplyTypeDTO> GetSupplyTypeList_Global(SupplyTypeDTO SupplyTypeDTO, PagedResultDTO<SupplyTypeDTO> PagedResultDTO = null)
    {
        var _datatypeglobalList = new List<SupplyTypeDTO>();
        try
        {
            var _datatypeList = SupplyType_Repository.GetSupplyTypeList(SupplyTypeDTO, PagedResultDTO);
            // if SupplyType is empty, return list
            _datatypeglobalList = _datatypeList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _datatypeglobalList;
    }



    public static int GetSupplyTypeTotalCount(PagedResultDTO<SupplyTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SupplyType_Repository.GetSupplyTypeCount(PagedResultDTO.Filter, PagedResultDTO);
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
