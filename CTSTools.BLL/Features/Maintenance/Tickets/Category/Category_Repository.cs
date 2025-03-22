using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Category;

public class Category_Repository
{
    public static List<CategoryDTO> GetCategoryList(CategoryDTO CategoryDTO, PagedResultDTO<CategoryDTO> PagedResultDTO = null)
    {
        var _categoryList = new List<CategoryDTO>();
        try
        {
            // Category Filters
            var _groupOperator = Category_DXFilter.GetCategory_DXFilter(CategoryDTO);
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
                var _categoryCollection = new XPCollection<CategoryXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(CategoryXPO.Oid), SortingDirection.Ascending))
                };

                if (_categoryCollection.AsQueryable().Count() > 0)
                {
                    _categoryList = _categoryCollection.Select(CategoryXPO => CategoryMap.XPOToDTO(CategoryXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _categoryList;
    }
    public static int GetCategoryCount(CategoryDTO CategoryDTO, PagedResultDTO<CategoryDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Category_DXFilter.GetCategory_DXFilter(CategoryDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<CategoryXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateCategory(CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _categoryXPO = CategoryMap.DTOtoXPO(CategoryDTO, _unit);
                _unit.Save(_categoryXPO);
                _unit.CommitChanges();
                CategoryDTO.ID = _categoryXPO.Oid;
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
    public static ValidationResultDTO UpdateCategory(CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _categoryXPO = CategoryMap.DTOtoXPO(CategoryDTO, _unit);
                _unit.Save(_categoryXPO);
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
    public static ValidationResultDTO DeleteCategory(CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _categoryXPO = CategoryMap.DTOtoXPO(CategoryDTO, _unit);
                _unit.Delete(_categoryXPO);
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
