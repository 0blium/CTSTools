using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using CTSTools.DAL.Features.Quality.QMS;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentType;

public class DocumentType_Repository
{
    public static List<DocumentTypeDTO> GetDocumentTypeList(DocumentTypeDTO DocumentTypeDTO, PagedResultDTO<DocumentTypeDTO> PagedResultDTO = null)
    {
        var _documentTypeList = new List<DocumentTypeDTO>();
        try
        {
            // DocumentType Filters
            var _groupOperator = DocumentType_DXFilter.GetDocumentType_DXFilter(DocumentTypeDTO);
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
                var _documentTypeCollection = new XPCollection<DocumentTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DocumentTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_documentTypeCollection.AsQueryable().Count() > 0)
                {
                    _documentTypeList = _documentTypeCollection.Select(DocumentTypeXPO => DocumentTypeMap.XPOToDTO(DocumentTypeXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentTypeList;
    }
    public static int GetDocumentTypeCount(DocumentTypeDTO DocumentTypeDTO, PagedResultDTO<DocumentTypeDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DocumentType_DXFilter.GetDocumentType_DXFilter(DocumentTypeDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DocumentTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDocumentType(DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _documentTypeXPO = DocumentTypeMap.DTOtoXPO(DocumentTypeDTO, _unit);
                _unit.Save(_documentTypeXPO);
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
    public static ValidationResultDTO UpdateDocumentType(DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _documentTypeXPO = DocumentTypeMap.DTOtoXPO(DocumentTypeDTO, _unit);
                _unit.Save(_documentTypeXPO);
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
    public static ValidationResultDTO DeleteDocumentType(DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _documentTypeXPO = DocumentTypeMap.DTOtoXPO(DocumentTypeDTO, _unit);
                _unit.Delete(_documentTypeXPO);
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
