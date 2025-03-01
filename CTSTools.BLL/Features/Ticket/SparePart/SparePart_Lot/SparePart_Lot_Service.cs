using CTSTools.BLL.Common;
using CTSTools.BLL.Features.ChangeLog;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.BLL.Features.Ticket.Item.TransactionOrigin;
using CTSTools.BLL.Features.Ticket.Provider;
using CTSTools.BLL.Features.Ticket.SparePart.SparePart;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SparePart;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePart_Lot;

public class SparePart_Lot_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSparePart_Lot_Global(SparePart_LotDTO SparePart_LotDTO)
    {
        //Get Spare Part inventory by spare part and support group
        var _sparePartInventoryDTO = SparePartInventory_Service.GetSparePartInventoryList_Global(
            new SparePartInventoryDTO
            {
                SparePartDTO = SparePart_LotDTO.SparePartDTO,
                SupportGroupDTO = SparePart_LotDTO.SupportGroupDTO
            }).FirstOrDefault();
        SparePart_LotDTO.SparePartInventoryDTO = _sparePartInventoryDTO;
        var _ValidationResultDTO = SparePart_Lot_Validator.CreateSparePart_Lot_Validation(SparePart_LotDTO);
        if (_ValidationResultDTO.Result)
        {
            SparePart_LotDTO.AvailableQty = SparePart_LotDTO.Quantity;
            SparePart_LotDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = GetSparePart_LotSerial(SparePart_LotDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //Get Spare Part Lot Serial
            _ValidationResultDTO = SparePart_Lot_Repository.CreateSparePart_Lot(SparePart_LotDTO);
        }
        //Save change log
        if (_ValidationResultDTO.Result)
        {
            //ChangeLog_Service.BuildChangeLogActionCreate<SparePart_LotDTO>(SparePart_LotDTO, (int)SparePart_LotDTO.AddedByID, (int)SparePart_LotDTO.ID);
        }
        //update Available Quantity of SparePart Inventory
        if (_ValidationResultDTO.Result)
        {
            IncreaseSparePartInventoryBySparePartLot(SparePart_LotDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSparePart_Lot_Global(SparePart_LotDTO SparePart_LotDTO)
    {
        //Get Spare Part inventory by spare part and support group
        var _sparePartInventoryDTO = SparePartInventory_Service.GetSparePartInventoryList_Global(
           new SparePartInventoryDTO
           {
               SparePartDTO = SparePart_LotDTO.SparePartDTO,
               SupportGroupDTO = SparePart_LotDTO.SupportGroupDTO
           }).FirstOrDefault();
        SparePart_LotDTO.SparePartInventoryDTO = _sparePartInventoryDTO;
        var _ValidationResultDTO = SparePart_Lot_Validator.UpdateSparePart_Lot_Validation(SparePart_LotDTO);
        var _previousSparePart_LotDTO = GetSparePart_LotList_Global(new SparePart_LotDTO { ID = SparePart_LotDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            SparePart_LotDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SparePart_Lot_Repository.UpdateSparePart_Lot(SparePart_LotDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog_Service.BuildChangeLogActionUpdate<SparePart_LotDTO>(_previousSparePart_LotDTO, SparePart_LotDTO, (int)SparePart_LotDTO.LastUpdateByID, (int)SparePart_LotDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSparePart_Lot_Global(SparePart_LotDTO SparePart_LotDTO)
    {
        var _ValidationResultDTO = SparePart_Lot_Validator.DeleteSparePart_Lot_Validation(SparePart_LotDTO);
        var _previousSparePart_LotDTO = GetSparePart_LotList_Global(new SparePart_LotDTO { ID = SparePart_LotDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SparePart_Lot_Repository.DeleteSparePart_Lot(SparePart_LotDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            DecreaseSparePartInventoryBySparePartLot(SparePart_LotDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog_Service.BuildChangeLogActionDelete<SparePart_LotDTO>(_previousSparePart_LotDTO, (int)SparePart_LotDTO.LastUpdateByID, (int)SparePart_LotDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<SparePart_LotDTO> GetSparePart_LotList_Global(SparePart_LotDTO SparePart_LotDTO, PagedResultDTO<SparePart_LotDTO> PagedResultDTO = null)
    {
        var _sparepart_lotglobalList = new List<SparePart_LotDTO>();
        try
        {
            var _sparepart_lotList = SparePart_Lot_Repository.GetSparePart_LotList(SparePart_LotDTO, PagedResultDTO);
            // if SparePart_Lot is empty, return list
            if (_sparepart_lotList.Count() == 0)
            {
                _sparepart_lotglobalList = _sparepart_lotList;
                return _sparepart_lotglobalList;
            }
            if (!SparePart_LotDTO.GetProviderDTO && !SparePart_LotDTO.GetSparePartDTO && !SparePart_LotDTO.GetSparePartInventoryDTO && !SparePart_LotDTO.GetSupportGroupDTO && !SparePart_LotDTO.GetTransactionOriginDTO)
            {
                _sparepart_lotglobalList = _sparepart_lotList;
                return _sparepart_lotglobalList;
            }
            _sparepart_lotglobalList = GetSparePart_LotRelatedData(SparePart_LotDTO, _sparepart_lotList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepart_lotglobalList;
    }



    public static List<SparePart_LotDTO> GetSparePart_LotRelatedData(SparePart_LotDTO SparePart_LotDTO, List<SparePart_LotDTO> SparePart_LotList)
    {
        var _sparepart_lotglobalList = new List<SparePart_LotDTO>();
        var _providerDict = new Dictionary<int?, ProviderDTO>();
        var _sparepartDict = new Dictionary<int?, SparePartDTO>();
        var _sparepartinventoryDict = new Dictionary<int?, SparePartInventoryDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();
        var _transactionoriginDict = new Dictionary<int?, TransactionOriginDTO>();

        try
        {
            if (SparePart_LotDTO.GetProviderDTO)
            {
                SparePart_LotDTO.ProviderDTO.ProviderIDArray = SparePart_LotList.GroupBy(g => g.ProviderDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _providerDict = Provider_Service.GetProviderList_Global(SparePart_LotDTO.ProviderDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePart_LotDTO.GetSparePartDTO)
            {
                SparePart_LotDTO.SparePartDTO.SparePartIDArray = SparePart_LotList.GroupBy(g => g.SparePartDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _sparepartDict = SparePart_Service.GetSparePartList_Global(SparePart_LotDTO.SparePartDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePart_LotDTO.GetSparePartInventoryDTO)
            {
                SparePart_LotDTO.SparePartInventoryDTO.SparePartInventoryIDArray = SparePart_LotList.GroupBy(g => g.SparePartInventoryDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _sparepartinventoryDict = SparePartInventory_Service.GetSparePartInventoryList_Global(SparePart_LotDTO.SparePartInventoryDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePart_LotDTO.GetSupportGroupDTO)
            {
                SparePart_LotDTO.SupportGroupDTO.SupportGroupIDArray = SparePart_LotList.GroupBy(g => g.SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(SparePart_LotDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePart_LotDTO.GetTransactionOriginDTO)
            {
                SparePart_LotDTO.TransactionOriginDTO.TransactionOriginIDArray = SparePart_LotList.GroupBy(g => g.TransactionOriginDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _transactionoriginDict = TransactionOrigin_Service.GetTransactionOriginList_Global(SparePart_LotDTO.TransactionOriginDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _sparepart_lotDTO in SparePart_LotList)
            {
                if (SparePart_LotDTO.GetProviderDTO && _providerDict.ContainsKey(_sparepart_lotDTO.ProviderDTO.ID))
                {
                    _sparepart_lotDTO.ProviderDTO = _providerDict[_sparepart_lotDTO.ProviderDTO.ID];
                }
                if (SparePart_LotDTO.GetSparePartDTO && _sparepartDict.ContainsKey(_sparepart_lotDTO.SparePartDTO.ID))
                {
                    _sparepart_lotDTO.SparePartDTO = _sparepartDict[_sparepart_lotDTO.SparePartDTO.ID];
                }
                if (SparePart_LotDTO.GetSparePartInventoryDTO && _sparepartinventoryDict.ContainsKey(_sparepart_lotDTO.SparePartInventoryDTO.ID))
                {
                    _sparepart_lotDTO.SparePartInventoryDTO = _sparepartinventoryDict[_sparepart_lotDTO.SparePartInventoryDTO.ID];
                }
                if (SparePart_LotDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_sparepart_lotDTO.SupportGroupDTO.ID))
                {
                    _sparepart_lotDTO.SupportGroupDTO = _supportgroupDict[_sparepart_lotDTO.SupportGroupDTO.ID];
                }
                if (SparePart_LotDTO.GetTransactionOriginDTO && _transactionoriginDict.ContainsKey(_sparepart_lotDTO.TransactionOriginDTO.ID))
                {
                    _sparepart_lotDTO.TransactionOriginDTO = _transactionoriginDict[_sparepart_lotDTO.TransactionOriginDTO.ID];
                }
                _sparepart_lotglobalList.Add(_sparepart_lotDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepart_lotglobalList;
    }




    public static int GetSparePart_LotTotalCount(PagedResultDTO<SparePart_LotDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SparePart_Lot_Repository.GetSparePart_LotCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    public static ValidationResultDTO GetSparePart_LotSerial(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _lotSerial = string.Format("LOT{0}", AssetManagementSQL.GetSparePartLotSerial());
            SparePart_LotDTO.Serial = _lotSerial;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha ocurrido un error. {0}", ex.Message);
        }
        return _validationResultDTO;
    }

    public static ValidationResultDTO IncreaseSparePartInventoryBySparePartLot(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //1.-Get SparePart Inventory
            var _sparePartInventoryDTO = SparePartInventory_Service.GetSparePartInventoryList_Global(
                new SparePartInventoryDTO
                {
                    ID = SparePart_LotDTO.SparePartInventoryDTO.ID
                }).FirstOrDefault();
            //2.- Increase Available Qty
            _sparePartInventoryDTO.AvailableQty += SparePart_LotDTO.Quantity;
            _sparePartInventoryDTO.LastUpdateByID = SparePart_LotDTO.AddedByID;
            //3.- Update SparePart Inventory
            _validationResultDTO = SparePartInventory_Service.UpdateSparePartInventory_Global(_sparePartInventoryDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DecreaseSparePartInventoryBySparePartLot(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //1.-Get SparePart Inventory
            var _sparePartInventoryDTO = SparePartInventory_Service.GetSparePartInventoryList_Global(
                new SparePartInventoryDTO
                {
                    ID = SparePart_LotDTO.SparePartInventoryDTO.ID
                }).FirstOrDefault();
            //2.- Increase Available Qty
            _sparePartInventoryDTO.AvailableQty -= SparePart_LotDTO.Quantity;
            _sparePartInventoryDTO.LastUpdateByID = SparePart_LotDTO.LastUpdateByID;
            //3.- Update SparePart Inventory
            _validationResultDTO = SparePartInventory_Service.UpdateSparePartInventory_Global(_sparePartInventoryDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _validationResultDTO;
    }

    #endregion
}
