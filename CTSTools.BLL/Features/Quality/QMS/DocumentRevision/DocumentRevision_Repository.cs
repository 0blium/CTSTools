using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevision_Repository
{
    public static List<DocumentRevisionDTO> GetDocumentRevisionList(DocumentRevisionDTO DocumentRevisionDTO, PagedResultDTO<DocumentRevisionDTO> PagedResultDTO = null)
    {
        var _documentRevisionList = new List<DocumentRevisionDTO>();
        try
        {
            // DocumentRevision Filters
            var _groupOperator = DocumentRevision_DXFilter.GetDocumentRevision_DXFilter(DocumentRevisionDTO);
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

            using (var _session = XPO_Helper.GetNewSession())
           {
                var _documentRevisionCollection = new XPCollection<DocumentRevisionXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DocumentRevisionXPO.Oid), SortingDirection.Ascending))
                };

                if (_documentRevisionCollection.AsQueryable().Count() > 0)
                {
                    _documentRevisionList = _documentRevisionCollection.Select(DocumentRevisionXPO => DocumentRevisionMap.XPOToDTO(DocumentRevisionXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentRevisionList;
    }
    public static int GetDocumentRevisionCount(DocumentRevisionDTO DocumentRevisionDTO, PagedResultDTO<DocumentRevisionDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DocumentRevision_DXFilter.GetDocumentRevision_DXFilter(DocumentRevisionDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DocumentRevisionXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDocumentRevision(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _documentRevisionXPO = DocumentRevisionMap.DTOtoXPO(DocumentRevisionDTO, _unit);
                _unit.Save(_documentRevisionXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }

    public static ValidationResultDTO UpdateMultipleDocumentRevision(List<DocumentRevisionDTO> DocumentRevisionList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _documentRevisionXPOList = DocumentRevisionMap.DTOListToXPOList(DocumentRevisionList, _unit);
            _unit.Save(_documentRevisionXPOList);
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

    public static ValidationResultDTO UpdateDocumentRevision(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _documentRevisionXPO = DocumentRevisionMap.DTOtoXPO(DocumentRevisionDTO, _unit);
            _unit.Save(_documentRevisionXPO);
            _unit.CommitChanges();
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteDocumentRevision(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _documentRevisionXPO = DocumentRevisionMap.DTOtoXPO(DocumentRevisionDTO, _unit);
                _unit.Delete(_documentRevisionXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
