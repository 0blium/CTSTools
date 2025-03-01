using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Currency;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Currency;

public class CurrencyMap
{
    public static CurrencyDTO XPOToDTO(CurrencyXPO CurrencyXPO)
    {
        var _currencyDTO = new CurrencyDTO();
        try
        {
            _currencyDTO.ID = CurrencyXPO.Oid;
            _currencyDTO.Name = CurrencyXPO.Name;
            _currencyDTO.ExchangeRate = CurrencyXPO.ExchangeRate;
            _currencyDTO.Description = CurrencyXPO.Description;
            _currencyDTO.AddedDate = (CurrencyXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? CurrencyXPO.AddedDate : (DateTime?)null;
            _currencyDTO.AddedByID = (CurrencyXPO.AddedBy != null) ? CurrencyXPO.AddedBy.Oid : 0;
            _currencyDTO.AddedByName = (CurrencyXPO.AddedBy != null) ? CurrencyXPO.AddedBy.Name : "Unnassigned";
            _currencyDTO.LastUpdate = (CurrencyXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? CurrencyXPO.LastUpdate : (DateTime?)null;
            _currencyDTO.LastUpdateByID = (CurrencyXPO.LastUpdateBy != null) ? CurrencyXPO.LastUpdateBy.Oid : 0;
            _currencyDTO.LastUpdateByName = (CurrencyXPO.LastUpdateBy != null) ? CurrencyXPO.LastUpdateBy.Name : "Unnassigned";
            _currencyDTO.IsActive = CurrencyXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _currencyDTO;
    }

    public static CurrencyXPO DTOtoXPO(CurrencyDTO CurrencyDTO, UnitOfWork UnitOfWork)
    {
        CurrencyXPO _currencyXPO;
        try
        {
            _currencyXPO = CurrencyDTO.ID == null || CurrencyDTO.ID == 0 ? new CurrencyXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<CurrencyXPO>(CurrencyDTO.ID);
            _currencyXPO.Name = _currencyXPO.Name == CurrencyDTO.Name ? _currencyXPO.Name : CurrencyDTO.Name;
            _currencyXPO.ExchangeRate = CurrencyDTO.ExchangeRate;
            _currencyXPO.Description = _currencyXPO.Description == CurrencyDTO.Description ? _currencyXPO.Description : CurrencyDTO.Description;
            _currencyXPO.AddedDate = _currencyXPO.AddedDate != null ? _currencyXPO.AddedDate : CurrencyDTO.AddedDate;
            _currencyXPO.AddedBy = (_currencyXPO.AddedBy != null) ? _currencyXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(CurrencyDTO.AddedByID);
            _currencyXPO.LastUpdate = _currencyXPO.LastUpdate == CurrencyDTO.LastUpdate ? _currencyXPO.LastUpdate : CurrencyDTO.LastUpdate;
            _currencyXPO.LastUpdateBy = (_currencyXPO.LastUpdateBy != null && _currencyXPO.LastUpdateBy.Oid == CurrencyDTO.LastUpdateByID) ? _currencyXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(CurrencyDTO.LastUpdateByID);
            _currencyXPO.IsActive = (bool)(_currencyXPO.IsActive == CurrencyDTO.IsActive ? _currencyXPO.IsActive : CurrencyDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _currencyXPO;
    }
}
