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

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand
{
    public class Brand_Repository
    {
        public static List<BrandDTO> GetBrandList(BrandDTO BrandDTO, PagedResultDTO<BrandDTO> PagedResultDTO = null)
        {
            var _brandList = new List<BrandDTO>();
            try
            {
                // Brand Filters
                var _groupOperator = Brand_DXFilter.GetBrand_DXFilter(BrandDTO);
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
                    var _brandCollection = new XPCollection<BrandXPO>(_session, _groupOperator, _sortProperty)
                    {
                        TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                        SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                        Sorting = new SortingCollection(new SortProperty(nameof(BrandXPO.Oid), SortingDirection.Ascending))
                    };

                    if (_brandCollection.AsQueryable().Count() > 0)
                    {
                        _brandList = _brandCollection.Select(BrandXPO => BrandMap.XPOToDTO(BrandXPO)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _brandList;
        }
        public static int GetBrandCount(BrandDTO BrandDTO)
        {
            try
            {
                var _groupOperator = Brand_DXFilter.GetBrand_DXFilter(BrandDTO);
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<BrandXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateBrand(BrandDTO BrandDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _brandXPO = BrandMap.DTOtoXPO(BrandDTO, _unit);
                    _unit.Save(_brandXPO);
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
        public static ValidationResultDTO UpdateBrand(BrandDTO BrandDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _brandXPO = BrandMap.DTOtoXPO(BrandDTO, _unit);
                    _unit.Save(_brandXPO);
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
        public static ValidationResultDTO DeleteBrand(BrandDTO BrandDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _brandXPO = BrandMap.DTOtoXPO(BrandDTO, _unit);
                    _unit.Delete(_brandXPO);
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
}
