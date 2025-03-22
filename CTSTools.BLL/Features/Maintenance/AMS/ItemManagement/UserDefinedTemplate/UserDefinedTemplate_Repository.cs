using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplate_Repository
{
    public static List<UserDefinedTemplateDTO> GetUserDefinedTemplateList(UserDefinedTemplateDTO UserDefinedTemplateDTO, PagedResultDTO<UserDefinedTemplateDTO> PagedResultDTO = null)
    {
        var _userdefinedtemplateList = new List<UserDefinedTemplateDTO>();
        try
        {
            // UserDefinedTemplate Filters
            var _groupOperator = UserDefinedTemplate_DXFilter.GetUserDefinedTemplate_DXFilter(UserDefinedTemplateDTO);
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
                var _userdefinedtemplateCollection = new XPCollection<UserDefinedTemplateXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(UserDefinedTemplateXPO.Oid), SortingDirection.Ascending))
                };

                if (_userdefinedtemplateCollection.AsQueryable().Count() > 0)
                {
                    _userdefinedtemplateList = _userdefinedtemplateCollection.Select(UserDefinedTemplateXPO => UserDefinedTemplateMap.XPOToDTO(UserDefinedTemplateXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedtemplateList;
    }
    public static int GetUserDefinedTemplateCount(UserDefinedTemplateDTO UserDefinedTemplateDTO, PagedResultDTO<UserDefinedTemplateDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = UserDefinedTemplate_DXFilter.GetUserDefinedTemplate_DXFilter(UserDefinedTemplateDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<UserDefinedTemplateXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUserDefinedTemplate(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedtemplateXPO = UserDefinedTemplateMap.DTOtoXPO(UserDefinedTemplateDTO, _unit);
                _unit.Save(_userdefinedtemplateXPO);
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
    public static ValidationResultDTO UpdateUserDefinedTemplate(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedtemplateXPO = UserDefinedTemplateMap.DTOtoXPO(UserDefinedTemplateDTO, _unit);
                _unit.Save(_userdefinedtemplateXPO);
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
    public static ValidationResultDTO DeleteUserDefinedTemplate(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedtemplateXPO = UserDefinedTemplateMap.DTOtoXPO(UserDefinedTemplateDTO, _unit);
                _unit.Delete(_userdefinedtemplateXPO);
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
