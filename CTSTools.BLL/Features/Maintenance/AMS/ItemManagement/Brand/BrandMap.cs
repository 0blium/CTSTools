using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand
{
    public class BrandMap
    {
        public static BrandDTO XPOToDTO(BrandXPO BrandXPO)
        {
            var _brandDTO = new BrandDTO();
            try
            {
                _brandDTO.ID = BrandXPO.Oid;
                _brandDTO.Name = BrandXPO.Name;
                _brandDTO.Description = BrandXPO.Description;
                _brandDTO.AddedDate = (BrandXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? BrandXPO.AddedDate : (DateTime?)null;
                _brandDTO.AddedByID = (BrandXPO.AddedBy != null) ? BrandXPO.AddedBy.Oid : 0;
                _brandDTO.AddedByName = (BrandXPO.AddedBy != null) ? BrandXPO.AddedBy.Name : "Unnassigned";
                _brandDTO.LastUpdate = (BrandXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? BrandXPO.LastUpdate : (DateTime?)null;
                _brandDTO.LastUpdateByID = (BrandXPO.LastUpdateBy != null) ? BrandXPO.LastUpdateBy.Oid : 0;
                _brandDTO.LastUpdateByName = (BrandXPO.LastUpdateBy != null) ? BrandXPO.LastUpdateBy.Name : "Unnassigned";
                _brandDTO.IsActive = BrandXPO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _brandDTO;
        }

        public static BrandXPO DTOtoXPO(BrandDTO BrandDTO, UnitOfWork UnitOfWork)
        {
            BrandXPO _brandXPO;
            try
            {
                _brandXPO = BrandDTO.ID == null || BrandDTO.ID == 0 ? new BrandXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<BrandXPO>(BrandDTO.ID);
                _brandXPO.Name = _brandXPO.Name == BrandDTO.Name ? _brandXPO.Name : BrandDTO.Name;
                _brandXPO.Description = _brandXPO.Description == BrandDTO.Description ? _brandXPO.Description : BrandDTO.Description;
                _brandXPO.AddedDate = _brandXPO.AddedDate != null ? _brandXPO.AddedDate : BrandDTO.AddedDate;
                _brandXPO.AddedBy = _brandXPO.AddedBy != null ? _brandXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(BrandDTO.AddedByID);
                _brandXPO.LastUpdate = _brandXPO.LastUpdate == BrandDTO.LastUpdate ? _brandXPO.LastUpdate : BrandDTO.LastUpdate;
                _brandXPO.LastUpdateBy = (_brandXPO.LastUpdateBy != null && _brandXPO.LastUpdateBy.Oid == BrandDTO.LastUpdateByID) ? _brandXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(BrandDTO.LastUpdateByID);
                _brandXPO.IsActive = _brandXPO.IsActive == BrandDTO.IsActive ? (bool)_brandXPO.IsActive : (bool)BrandDTO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _brandXPO;
        }
    }
}
