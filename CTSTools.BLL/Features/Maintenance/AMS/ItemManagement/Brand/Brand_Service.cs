using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand
{
    public class Brand_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateBrand_Global(BrandDTO BrandDTO)
        {
            var _validationResultDTO = Brand_Validator.CreateBrand_Validation(BrandDTO);
            if (_validationResultDTO.Result)
            {
                BrandDTO.AddedDate = DateTime.Now;
                _validationResultDTO = Brand_Repository.CreateBrand(BrandDTO);
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateBrand_Global(BrandDTO BrandDTO)
        {
            var _validationResultDTO = Brand_Validator.UpdateBrand_Validation(BrandDTO);
            if (_validationResultDTO.Result)
            {
                BrandDTO.LastUpdate = DateTime.Now;
                _validationResultDTO = Brand_Repository.UpdateBrand(BrandDTO);
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteBrand_Global(BrandDTO BrandDTO)
        {
            var _validationResultDTO = Brand_Validator.DeleteBrand_Validation(BrandDTO);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = Brand_Repository.DeleteBrand(BrandDTO);
            }
            return _validationResultDTO;
        }
        public static List<BrandDTO> GetBrandList_Global(BrandDTO BrandDTO, PagedResultDTO<BrandDTO> PagedResultDTO = null)
        {
            var _brandglobalList = new List<BrandDTO>();
            try
            {
                var _BrandList = Brand_Repository.GetBrandList(BrandDTO, PagedResultDTO);
                // if Brand is empty, return list
                _brandglobalList = _BrandList;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _brandglobalList;
        }

        public static int GetBrandTotalCount(PagedResultDTO<BrandDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = PagedResultDTO.dxFilters != null ? PagedResultDTO.DataList.Count : Brand_Repository.GetBrandCount(PagedResultDTO.Filter);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        // Aqui va la logica 

        #endregion
    }
}
