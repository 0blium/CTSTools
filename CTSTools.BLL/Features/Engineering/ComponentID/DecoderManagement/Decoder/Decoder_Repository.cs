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


namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
public class Decoder_Repository
{
    public static List<DecoderDTO> GetDecoderList(DecoderDTO DecoderDTO, PagedResultDTO<DecoderDTO> PagedResultDTO = null)
    {
        var _decoderList = new List<DecoderDTO>();
        try
        {
            // Decoder Filters
            var _groupOperator = Decoder_DXFilter.GetDecoder_DXFilter(DecoderDTO);
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
            var _decoderCollection = new XPCollection<DecoderXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DecoderXPO.Oid), SortingDirection.Ascending))
            };

            if (_decoderCollection.AsQueryable().Count() > 0)
            {
                _decoderList = _decoderCollection.Select(DecoderXPO => DecoderMap.XPOToDTO(DecoderXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderList;
    }
    public static DecoderDTO GetDecoderByID(int DecoderID)
    {
        var _decoderDTO = new DecoderDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderXPO = _unit.GetObjectByKey<DecoderXPO>(DecoderID);
            if (_decoderXPO != null)
                _decoderDTO = DecoderMap.XPOToDTO(_decoderXPO);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderDTO;
    }
    public static int GetDecoderCount(DecoderDTO DecoderDTO, PagedResultDTO<DecoderDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Decoder_DXFilter.GetDecoder_DXFilter(DecoderDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<DecoderXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderXPO = DecoderMap.DTOtoXPO(DecoderDTO, _unit);
            _unit.Save(_decoderXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _decoderXPO.Oid;
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
    public static ValidationResultDTO UpdateDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderXPO = DecoderMap.DTOtoXPO(DecoderDTO, _unit);
            _unit.Save(_decoderXPO);
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
    public static ValidationResultDTO DeleteDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderXPO = DecoderMap.DTOtoXPO(DecoderDTO, _unit);
            _unit.Delete(_decoderXPO);
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
    public static ValidationResultDTO CreateMultiple(List<DecoderDTO> DecoderDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = DecoderMap.DTOListToXPOList(DecoderDTOList, _unit);
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
