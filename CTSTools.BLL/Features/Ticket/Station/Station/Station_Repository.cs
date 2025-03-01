using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Station;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo.DB;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.Station;

public class Station_Repository
{
    public static List<StationDTO> GetStationList(StationDTO StationDTO, PagedResultDTO<StationDTO> PagedResultDTO = null)
    {
        var _stationList = new List<StationDTO>();
        try
        {
            // Station Filters
            var _groupOperator = Station_DXFilter.GetStation_DXFilter(StationDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _stationCollection = new XPCollection<StationXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(StationXPO.Oid), SortingDirection.Ascending))
                };

                if (_stationCollection.AsQueryable().Count() > 0)
                {
                    _stationList = _stationCollection.Select(StationXPO => StationMap.XPOToDTO(StationXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _stationList;
    }
    public static int GetStationCount(StationDTO StationDTO, PagedResultDTO<StationDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Station_DXFilter.GetStation_DXFilter(StationDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<StationXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateStation(StationDTO StationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _stationXPO = StationMap.DTOtoXPO(StationDTO, _unit);
                _unit.Save(_stationXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateStation(StationDTO StationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _stationXPO = StationMap.DTOtoXPO(StationDTO, _unit);
                _unit.Save(_stationXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteStation(StationDTO StationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _stationXPO = StationMap.DTOtoXPO(StationDTO, _unit);
                _unit.Delete(_stationXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            if (ex.Message.Contains("Number:547"))
            {
                _validationResultDTO = Exception_Service.GetErrorTypeByMessage(ex.Message);
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to delete the record.");
            }
        }
        return _validationResultDTO;
    }
}
