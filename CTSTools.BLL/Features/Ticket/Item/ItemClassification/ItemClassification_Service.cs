using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.ItemClassification;

public class ItemClassification_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItemClassification_Global(ItemClassificationDTO ItemClassificationDTO)
    {
        var _ValidationResultDTO = ItemClassification_Validator.CreateItemClassification_Validation(ItemClassificationDTO);
        if (_ValidationResultDTO.Result)
        {
            ItemClassificationDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = ItemClassification_Repository.CreateItemClassification(ItemClassificationDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateItemClassification_Global(ItemClassificationDTO ItemClassificationDTO)
    {
        var _ValidationResultDTO = ItemClassification_Validator.UpdateItemClassification_Validation(ItemClassificationDTO);
        if (_ValidationResultDTO.Result)
        {
            ItemClassificationDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = ItemClassification_Repository.UpdateItemClassification(ItemClassificationDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteItemClassification_Global(ItemClassificationDTO ItemClassificationDTO)
    {
        var _ValidationResultDTO = ItemClassification_Validator.DeleteItemClassification_Validation(ItemClassificationDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = ItemClassification_Repository.DeleteItemClassification(ItemClassificationDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ItemClassificationDTO> GetItemClassificationList_Global(ItemClassificationDTO ItemClassificationDTO, PagedResultDTO<ItemClassificationDTO> PagedResultDTO = null)
    {
        var _itemclassificationglobalList = new List<ItemClassificationDTO>();
        try
        {
            var _itemclassificationList = ItemClassification_Repository.GetItemClassificationList(ItemClassificationDTO, PagedResultDTO);
            // if ItemClassification is empty, return list
            _itemclassificationglobalList = _itemclassificationList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _itemclassificationglobalList;
    }


    public static int GetItemClassificationTotalCount(PagedResultDTO<ItemClassificationDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = ItemClassification_Repository.GetItemClassificationCount(PagedResultDTO.Filter, PagedResultDTO);
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
