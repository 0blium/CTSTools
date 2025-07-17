using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;
using CTSTools.BLL.Features.Maintenance.Tickets.Ticket;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;

public class SparePartUsage_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSparePartUsage_Global(SparePartUsageDTO SparePartUsageDTO)
    {
        var _ValidationResultDTO = SparePartUsage_Validator.CreateSparePartUsage_Validation(SparePartUsageDTO);
        if (_ValidationResultDTO.Result)
        {
            SparePartUsageDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = SparePartUsage_Repository.CreateSparePartUsage(SparePartUsageDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<SparePartUsageDTO>(SparePartUsageDTO, (int)SparePartUsageDTO.AddedByID, (int)SparePartUsageDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSparePartUsage_Global(SparePartUsageDTO SparePartUsageDTO)
    {
        var _ValidationResultDTO = SparePartUsage_Validator.UpdateSparePartUsage_Validation(SparePartUsageDTO);
        var _previousSparePartUsageDTO = GetSparePartUsageList_Global(new SparePartUsageDTO { ID = SparePartUsageDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            SparePartUsageDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SparePartUsage_Repository.UpdateSparePartUsage(SparePartUsageDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<SparePartUsageDTO>(_previousSparePartUsageDTO, SparePartUsageDTO, (int)SparePartUsageDTO.LastUpdateByID, (int)SparePartUsageDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSparePartUsage_Global(SparePartUsageDTO SparePartUsageDTO)
    {
        var _ValidationResultDTO = SparePartUsage_Validator.DeleteSparePartUsage_Validation(SparePartUsageDTO);
        var _previousSparePartUsageDTO = GetSparePartUsageList_Global(new SparePartUsageDTO { ID = SparePartUsageDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SparePartUsage_Repository.DeleteSparePartUsage(SparePartUsageDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<SparePartUsageDTO>(_previousSparePartUsageDTO, (int)SparePartUsageDTO.LastUpdateByID, (int)SparePartUsageDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<SparePartUsageDTO> GetSparePartUsageList_Global(SparePartUsageDTO SparePartUsageDTO, PagedResultDTO<SparePartUsageDTO> PagedResultDTO = null)
    {
        var _sparepartusageglobalList = new List<SparePartUsageDTO>();
        try
        {
            var _sparepartusageList = SparePartUsage_Repository.GetSparePartUsageList(SparePartUsageDTO, PagedResultDTO);
            // if SparePartUsage is empty, return list
            if (_sparepartusageList.Count() == 0)
            {
                _sparepartusageglobalList = _sparepartusageList;
                return _sparepartusageglobalList;
            }
            if (!SparePartUsageDTO.GetTicketDTO && !SparePartUsageDTO.GetSparePartInventoryDTO && !SparePartUsageDTO.GetItem_LineDTO && !SparePartUsageDTO.GetSparePart_LotDTO)
            {
                _sparepartusageglobalList = _sparepartusageList;
                return _sparepartusageglobalList;
            }
            _sparepartusageglobalList = GetSparePartUsageRelatedData(SparePartUsageDTO, _sparepartusageList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartusageglobalList;
    }



    public static List<SparePartUsageDTO> GetSparePartUsageRelatedData(SparePartUsageDTO SparePartUsageDTO, List<SparePartUsageDTO> SparePartUsageList)
    {
        var _sparepartusageglobalList = new List<SparePartUsageDTO>();
        var _ticketDict = new Dictionary<int?, TicketDTO>();
        var _sparepartinventoryDict = new Dictionary<int?, SparePartInventoryDTO>();
        var _item_lineDict = new Dictionary<int?, Item_LineDTO>();
        var _sparepart_lotDict = new Dictionary<int?, SparePart_LotDTO>();

        try
        {
            if (SparePartUsageDTO.GetTicketDTO)
            {
                SparePartUsageDTO.TicketDTO.TicketIDArray = SparePartUsageList.GroupBy(g => g.TicketID)
                        .Select(s => s.Key)
                        .ToArray();

                _ticketDict = Ticket_Service.GetTicketList_Global(SparePartUsageDTO.TicketDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePartUsageDTO.GetSparePartInventoryDTO)
            {
                SparePartUsageDTO.SparePartInventoryDTO.SparePartInventoryIDArray = SparePartUsageList.GroupBy(g => g.SparePartInventoryID)
                        .Select(s => s.Key)
                        .ToArray();
                SparePartUsageDTO.SparePartInventoryDTO.SparePartDTO.GetImage = true;
                _sparepartinventoryDict = SparePartInventory_Service.GetSparePartInventoryList_Global(SparePartUsageDTO.SparePartInventoryDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePartUsageDTO.GetItem_LineDTO)
            {
                SparePartUsageDTO.Item_LineDTO.Item_LineIDArray = SparePartUsageList.GroupBy(g => g.Item_LineID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_lineDict = Item_Line_Service.GetItem_LineList_Global(SparePartUsageDTO.Item_LineDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePartUsageDTO.GetSparePart_LotDTO)
            {
                SparePartUsageDTO.SparePart_LotDTO.SparePart_LotIDArray = SparePartUsageList.GroupBy(g => g.SparePart_LotID)
                        .Select(s => s.Key)
                        .ToArray();

                _sparepart_lotDict = SparePart_Lot_Service.GetSparePart_LotList_Global(SparePartUsageDTO.SparePart_LotDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _sparepartusageDTO in SparePartUsageList)
            {
                if (SparePartUsageDTO.GetTicketDTO && _ticketDict.ContainsKey(_sparepartusageDTO.TicketID))
                {
                    _sparepartusageDTO.TicketDTO = _ticketDict[_sparepartusageDTO.TicketID];
                }
                if (SparePartUsageDTO.GetSparePartInventoryDTO && _sparepartinventoryDict.ContainsKey(_sparepartusageDTO.SparePartInventoryID))
                {
                    _sparepartusageDTO.SparePartInventoryDTO = _sparepartinventoryDict[_sparepartusageDTO.SparePartInventoryID];
                }
                if (SparePartUsageDTO.GetItem_LineDTO && _item_lineDict.ContainsKey(_sparepartusageDTO.Item_LineID))
                {
                    _sparepartusageDTO.Item_LineDTO = _item_lineDict[_sparepartusageDTO.Item_LineID];
                }
                if (SparePartUsageDTO.GetSparePart_LotDTO && _sparepart_lotDict.ContainsKey(_sparepartusageDTO.SparePart_LotID))
                {
                    _sparepartusageDTO.SparePart_LotDTO = _sparepart_lotDict[_sparepartusageDTO.SparePart_LotID];
                }
                _sparepartusageglobalList.Add(_sparepartusageDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartusageglobalList;
    }




    public static int GetSparePartUsageTotalCount(PagedResultDTO<SparePartUsageDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SparePartUsage_Repository.GetSparePartUsageCount(PagedResultDTO.Filter, PagedResultDTO);
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
