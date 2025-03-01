using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.Ticket.Item.DataType;

public class DataType_Repository
{
    public static List<DataTypeDTO> GetDataTypeList(DataTypeDTO DataTypeDTO, PagedResultDTO<DataTypeDTO> PagedResultDTO = null)
    {
        var _datatypeList = new List<DataTypeDTO>();
        try
        {
            // DataType Filters
            var _groupOperator = DataType_DXFilter.GetDataType_DXFilter(DataTypeDTO);
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
                var _datatypeCollection = new XPCollection<DataTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = new SortingCollection(new SortProperty(nameof(DataTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_datatypeCollection.AsQueryable().Count() > 0)
                {
                    _datatypeList = _datatypeCollection.Select(DataTypeXPO => DataTypeMap.XPOToDTO(DataTypeXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _datatypeList;
    }
    public static int GetDataTypeCount(DataTypeDTO DataTypeDTO)
    {
        try
        {
            var _groupOperator = DataType_DXFilter.GetDataType_DXFilter(DataTypeDTO);
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DataTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDataType(DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _datatypeXPO = DataTypeMap.DTOtoXPO(DataTypeDTO, _unit);
                _unit.Save(_datatypeXPO);
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
    public static ValidationResultDTO UpdateDataType(DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _datatypeXPO = DataTypeMap.DTOtoXPO(DataTypeDTO, _unit);
                _unit.Save(_datatypeXPO);
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
    public static ValidationResultDTO DeleteDataType(DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _datatypeXPO = DataTypeMap.DTOtoXPO(DataTypeDTO, _unit);
                _unit.Delete(_datatypeXPO);
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
