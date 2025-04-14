using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
public class DecoderStructure_Repository
{
    public static List<DecoderStructureDTO> GetDecoderStructureList(DecoderStructureDTO DecoderStructureDTO, PagedResultDTO<DecoderStructureDTO> PagedResultDTO = null)
    {
        var _decoderstructureList = new List<DecoderStructureDTO>();
        try
        {
            // DecoderStructure Filters
            var _groupOperator = DecoderStructure_DXFilter.GetDecoderStructure_DXFilter(DecoderStructureDTO);
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
            var _decoderstructureCollection = new XPCollection<DecoderStructureXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DecoderStructureXPO.Oid), SortingDirection.Ascending))
            };
            if (_decoderstructureCollection.AsQueryable().Count() > 0)
            {
                _decoderstructureList = DecoderStructureMap.XPCollectionToList(_decoderstructureCollection);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderstructureList;
    }
    public static int GetDecoderStructureCount(DecoderStructureDTO DecoderStructureDTO, PagedResultDTO<DecoderStructureDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DecoderStructure_DXFilter.GetDecoderStructure_DXFilter(DecoderStructureDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<DecoderStructureXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static DecoderStructureDTO GetDecoderStructureByID(int DecoderStructureID)
    {
        var _decoderStructureDTO = new DecoderStructureDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderStructureXPO = _unit.GetObjectByKey<DecoderStructureXPO>(DecoderStructureID);
            if (_decoderStructureXPO != null)
                _decoderStructureDTO = DecoderStructureMap.XPOToDTO(_decoderStructureXPO);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderStructureDTO;
    }


    public static ValidationResultDTO CreateDecoderStructure(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderstructureXPO = DecoderStructureMap.DTOtoXPO(DecoderStructureDTO, _unit);
            _unit.Save(_decoderstructureXPO);
            _unit.CommitChanges();
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
    public static ValidationResultDTO UpdateDecoderStructure(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderstructureXPO = DecoderStructureMap.DTOtoXPO(DecoderStructureDTO, _unit);
            _unit.Save(_decoderstructureXPO);
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
    public static ValidationResultDTO DeleteDecoderStructure(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderstructureXPO = DecoderStructureMap.DTOtoXPO(DecoderStructureDTO, _unit);
            _unit.Delete(_decoderstructureXPO);
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
    public static ValidationResultDTO DeleteMultipleDecoderStructure(List<DecoderStructureDTO> DecoderStructureList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderStructureXPOList = DecoderStructureMap.DTOListToXPOList(DecoderStructureList, _unit);
            _unit.Delete(_decoderStructureXPOList);
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
    public static ValidationResultDTO CreateMultiple(List<DecoderStructureDTO> DecoderStructureDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = DecoderStructureMap.DTOListToXPOList(DecoderStructureDTOList, _unit);
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

