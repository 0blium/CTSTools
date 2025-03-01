using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.ChangeLog;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLog_Repository
{
    public static List<ChangeLogDTO> GetChangeLogList(ChangeLogDTO ChangeLogDTO, PagedResultDTO<ChangeLogDTO> PagedResultDTO = null)
    {
        var _changelogList = new List<ChangeLogDTO>();
        try
        {
            // ChangeLog Filters
            var _groupOperator = ChangeLog_DXFilter.GetChangeLog_DXFilter(ChangeLogDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO != null && PagedResultDTO.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _changelogCollection = new XPCollection<ChangeLogXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = new SortingCollection(new SortProperty(nameof(ChangeLogXPO.Oid), SortingDirection.Ascending))
                };

                if (_changelogCollection.AsQueryable().Count() > 0)
                {
                    _changelogList = _changelogCollection.Select(ChangeLogXPO => ChangeLogMap.XPOToDTO(ChangeLogXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _changelogList;
    }
    public static int GetChangeLogCount(ChangeLogDTO ChangeLogDTO)
    {
        try
        {
            var _groupOperator = ChangeLog_DXFilter.GetChangeLog_DXFilter(ChangeLogDTO);
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<ChangeLogXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateChangeLog(ChangeLogDTO ChangeLogDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _changelogXPO = ChangeLogMap.DTOtoXPO(ChangeLogDTO, _unit);
                _unit.Save(_changelogXPO);
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
    public static ValidationResultDTO UpdateChangeLog(ChangeLogDTO ChangeLogDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _changelogXPO = ChangeLogMap.DTOtoXPO(ChangeLogDTO, _unit);
                _unit.Save(_changelogXPO);
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
    public static ValidationResultDTO DeleteChangeLog(ChangeLogDTO ChangeLogDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _changelogXPO = ChangeLogMap.DTOtoXPO(ChangeLogDTO, _unit);
                _unit.Delete(_changelogXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
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
}
