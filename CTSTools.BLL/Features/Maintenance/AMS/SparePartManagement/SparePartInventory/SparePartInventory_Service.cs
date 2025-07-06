using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;

public class SparePartInventory_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSparePartInventory_Global(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _ValidationResultDTO = SparePartInventory_Validator.CreateSparePartInventory_Validation(SparePartInventoryDTO);
        if (_ValidationResultDTO.Result)
        {
            if (SparePartInventoryDTO.MinQty > SparePartInventoryDTO.MaxQty)
            {
                var aux = SparePartInventoryDTO.MaxQty;
                SparePartInventoryDTO.MaxQty = SparePartInventoryDTO.MinQty;
                SparePartInventoryDTO.MinQty = aux;
            }
            SparePartInventoryDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = SparePartInventory_Repository.CreateSparePartInventory(SparePartInventoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSparePartInventory_Global(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _ValidationResultDTO = SparePartInventory_Validator.UpdateSparePartInventory_Validation(SparePartInventoryDTO);
        var _previousSparePartInventoryDTO = GetSparePartInventoryList_Global(new SparePartInventoryDTO { ID = SparePartInventoryDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            if (SparePartInventoryDTO.MinQty > SparePartInventoryDTO.MaxQty)
            {
                var aux = SparePartInventoryDTO.MaxQty;
                SparePartInventoryDTO.MaxQty = SparePartInventoryDTO.MinQty;
                SparePartInventoryDTO.MinQty = aux;
            }
            SparePartInventoryDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SparePartInventory_Repository.UpdateSparePartInventory(SparePartInventoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSparePartInventory_Global(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _ValidationResultDTO = SparePartInventory_Validator.DeleteSparePartInventory_Validation(SparePartInventoryDTO);
        var _previousSparePartInventoryDTO = GetSparePartInventoryList_Global(new SparePartInventoryDTO { ID = SparePartInventoryDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SparePartInventory_Repository.DeleteSparePartInventory(SparePartInventoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<SparePartInventoryDTO> GetSparePartInventoryList_Global(SparePartInventoryDTO SparePartInventoryDTO, PagedResultDTO<SparePartInventoryDTO> PagedResultDTO = null)
    {
        var _sparepartinventoryglobalList = new List<SparePartInventoryDTO>();
        try
        {
            var _sparepartinventoryList = SparePartInventory_Repository.GetSparePartInventoryList(SparePartInventoryDTO, PagedResultDTO);
            // if SparePartInventory is empty, return list
            if (_sparepartinventoryList.Count() == 0)
            {
                _sparepartinventoryglobalList = _sparepartinventoryList;
                return _sparepartinventoryglobalList;
            }
            if (!SparePartInventoryDTO.GetSparePartDTO && !SparePartInventoryDTO.GetSupportGroupDTO)
            {
                _sparepartinventoryglobalList = _sparepartinventoryList;
                return _sparepartinventoryglobalList;
            }
            _sparepartinventoryglobalList = GetSparePartInventoryRelatedData(SparePartInventoryDTO, _sparepartinventoryList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartinventoryglobalList;
    }



    public static List<SparePartInventoryDTO> GetSparePartInventoryRelatedData(SparePartInventoryDTO SparePartInventoryDTO, List<SparePartInventoryDTO> SparePartInventoryList)
    {
        var _sparepartinventoryglobalList = new List<SparePartInventoryDTO>();
        var _sparepartDict = new Dictionary<int?, SparePartDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();

        try
        {
            if (SparePartInventoryDTO.GetSparePartDTO)
            {
                SparePartInventoryDTO.SparePartDTO.SparePartIDArray = SparePartInventoryList.GroupBy(g => g.SparePartID)
                        .Select(s => s.Key)
                        .ToArray();

                _sparepartDict = SparePart_Service.GetSparePartList_Global(SparePartInventoryDTO.SparePartDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SparePartInventoryDTO.GetSupportGroupDTO)
            {
                SparePartInventoryDTO.SupportGroupDTO.SupportGroupIDArray = SparePartInventoryList.GroupBy(g => g.SupportGroupID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(SparePartInventoryDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _sparepartinventoryDTO in SparePartInventoryList)
            {
                if (SparePartInventoryDTO.GetSparePartDTO && _sparepartDict.ContainsKey(_sparepartinventoryDTO.SparePartID))
                {
                    _sparepartinventoryDTO.SparePartDTO = _sparepartDict[_sparepartinventoryDTO.SparePartID];
                }
                if (SparePartInventoryDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_sparepartinventoryDTO.SupportGroupID))
                {
                    _sparepartinventoryDTO.SupportGroupDTO = _supportgroupDict[_sparepartinventoryDTO.SupportGroupID];
                }
                _sparepartinventoryglobalList.Add(_sparepartinventoryDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartinventoryglobalList;
    }




    public static int GetSparePartInventoryTotalCount(PagedResultDTO<SparePartInventoryDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SparePartInventory_Repository.GetSparePartInventoryCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    public static ValidationResultDTO SparePartInventoryTransaction(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {

            //Get Spare Part Usage to Ticket
            var _sparePartUsageList = SparePartUsage_Service.GetSparePartUsageList_Global(new SparePartUsageDTO { TicketID = SparePartUsageDTO.TicketID });
            int _sparePartUsageQty = 0;
            if (_sparePartUsageList.Count() > 0)
            {
                //Read sparepart usage list
                foreach (var _sparepartUsageDTO in _sparePartUsageList)
                {
                    _sparePartUsageQty = _sparepartUsageDTO.Quantity;
                    while (_sparePartUsageQty > 0)
                    {
                        if (_validationResultDTO.Result)
                        {
                            //get sparepart Lot
                            var _sparePart_LotDTO = new SparePart_LotDTO { ID = _sparepartUsageDTO.SparePart_LotID, IsActive = true };
                            _sparePart_LotDTO = SparePart_Lot_Service.GetSparePart_LotList_Global(_sparePart_LotDTO).Where(w => w.AvailableQty > 0).FirstOrDefault();

                            //Validate if the available Qty  of lot is greater than Sparepart usage in ticket
                            if (_sparePart_LotDTO.AvailableQty > _sparePartUsageQty)
                            {
                                //Decrease Available Qty
                                _sparePart_LotDTO.AvailableQty -= _sparePartUsageQty;
                                _sparePart_LotDTO.LastUpdateByID = SparePartUsageDTO.LastUpdateByID;
                                //Update Spare Part Lot
                                _validationResultDTO = SparePart_Lot_Service.UpdateSparePart_Lot_Global(_sparePart_LotDTO);
                                //the request quantity is 0
                                _sparePartUsageQty = 0;

                            }
                            //Validate if Available Quantity of lot is equal sparepart usage qty  
                            else if (_sparePart_LotDTO.AvailableQty == _sparePartUsageQty)
                            {
                                //decrease request quantity 
                                _sparePartUsageQty -= _sparePart_LotDTO.AvailableQty;
                                _sparePart_LotDTO.AvailableQty = 0;
                                _sparePart_LotDTO.IsActive = false;
                                _sparePart_LotDTO.LastUpdateByID = SparePartUsageDTO.LastUpdateByID;
                                //update SparePArt Lot
                                _validationResultDTO = SparePart_Lot_Service.UpdateSparePart_Lot_Global(_sparePart_LotDTO);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (_validationResultDTO.Result)
                    {
                        var _sparePartInventoryDTO = GetSparePartInventoryList_Global(
                            new SparePartInventoryDTO
                            {
                                ID = _sparepartUsageDTO.SparePartInventoryID
                            }).FirstOrDefault();
                        _sparePartInventoryDTO.AvailableQty -= _sparepartUsageDTO.Quantity;
                        _sparePartInventoryDTO.LastUpdateByID = SparePartUsageDTO.LastUpdateByID;
                        _validationResultDTO = UpdateSparePartInventory_Global(_sparePartInventoryDTO);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the transaction. ");
        }
        return _validationResultDTO;
    }

    #endregion
}
