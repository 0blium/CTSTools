using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;

public class Part_Repository
{
    public static List<PartDTO> GetPartList(PartDTO PartDTO, PagedResultDTO<PartDTO> PagedResultDTO = null)
    {
        var _partList = new List<PartDTO>();
        try
        {
            // Part Filters
            var _groupOperator = Part_DXFilter.GetPart_DXFilter(PartDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }

            using var _session = XPO_Helper.GetNewSession();
            var _partCollection = new XPCollection<PartXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(PartXPO.Oid), SortingDirection.Ascending))
            };

            if (_partCollection.AsQueryable().Count() > 0)
            {
                _partList = _partCollection.Select(PartXPO => PartMap.XPOToDTO(PartXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _partList;
    }
    public static PartDTO GetPartByID(int PartID)
    {
        var _partDTO = new PartDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _partXPO = _unit.GetObjectByKey<PartXPO>(PartID);
            if (_partXPO != null)
                _partDTO = PartMap.XPOToDTO(_partXPO);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _partDTO;
    }
    public static int GetPartCount(PartDTO PartDTO, PagedResultDTO<PartDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Part_DXFilter.GetPart_DXFilter(PartDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<PartXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreatePart(PartDTO PartDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _partXPO = PartMap.DTOtoXPO(PartDTO, _unit);
            _unit.Save(_partXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _partXPO.Oid;
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
    public static ValidationResultDTO UpdatePart(PartDTO PartDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _partXPO = PartMap.DTOtoXPO(PartDTO, _unit);
            _unit.Save(_partXPO);
            _unit.CommitChanges();

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
    public static ValidationResultDTO DeletePart(PartDTO PartDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _partXPO = PartMap.DTOtoXPO(PartDTO, _unit);
            _unit.Delete(_partXPO);
            _unit.CommitChanges();
            _unit.PurgeDeletedObjects();

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
    public static ValidationResultDTO CreateMultiple(List<PartDTO> PartDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = PartMap.DTOListToXPOList(PartDTOList, _unit);
            _unit.Save(_xPOList);
            _unit.CommitChanges();

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
