using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.AdvancedSettings.TransactionOrigin;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.TransactionOrigin;

public class TransactionOriginMap
{
    public static TransactionOriginDTO XPOToDTO(TransactionOriginXPO TransactionOriginXPO)
    {
        var _transactionoriginDTO = new TransactionOriginDTO();
        try
        {
            _transactionoriginDTO.ID = TransactionOriginXPO.Oid;
            _transactionoriginDTO.Name = TransactionOriginXPO.Name;
            _transactionoriginDTO.Description = TransactionOriginXPO.Description;
            _transactionoriginDTO.AddedDate = (TransactionOriginXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? TransactionOriginXPO.AddedDate : (DateTime?)null;
            _transactionoriginDTO.AddedByID = (TransactionOriginXPO.AddedBy != null) ? TransactionOriginXPO.AddedBy.Oid : 0;
            _transactionoriginDTO.AddedByName = (TransactionOriginXPO.AddedBy != null) ? TransactionOriginXPO.AddedBy.Name : "Unnassigned";
            _transactionoriginDTO.LastUpdate = (TransactionOriginXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? TransactionOriginXPO.LastUpdate : (DateTime?)null;
            _transactionoriginDTO.LastUpdateByID = (TransactionOriginXPO.LastUpdateBy != null) ? TransactionOriginXPO.LastUpdateBy.Oid : 0;
            _transactionoriginDTO.LastUpdateByName = (TransactionOriginXPO.LastUpdateBy != null) ? TransactionOriginXPO.LastUpdateBy.Name : "Unnassigned";
            _transactionoriginDTO.IsActive = TransactionOriginXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _transactionoriginDTO;
    }

    public static TransactionOriginXPO DTOtoXPO(TransactionOriginDTO TransactionOriginDTO, UnitOfWork UnitOfWork)
    {
        TransactionOriginXPO _transactionoriginXPO;
        try
        {
            _transactionoriginXPO = TransactionOriginDTO.ID == null || TransactionOriginDTO.ID == 0 ? new TransactionOriginXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<TransactionOriginXPO>(TransactionOriginDTO.ID);
            _transactionoriginXPO.Name = _transactionoriginXPO.Name == TransactionOriginDTO.Name ? _transactionoriginXPO.Name : TransactionOriginDTO.Name;
            _transactionoriginXPO.Description = _transactionoriginXPO.Description == TransactionOriginDTO.Description ? _transactionoriginXPO.Description : TransactionOriginDTO.Description;
            _transactionoriginXPO.AddedDate = _transactionoriginXPO.AddedDate != null ? _transactionoriginXPO.AddedDate : TransactionOriginDTO.AddedDate;
            _transactionoriginXPO.AddedBy = (_transactionoriginXPO.AddedBy != null) ? _transactionoriginXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(TransactionOriginDTO.AddedByID);
            _transactionoriginXPO.LastUpdate = _transactionoriginXPO.LastUpdate == TransactionOriginDTO.LastUpdate ? _transactionoriginXPO.LastUpdate : TransactionOriginDTO.LastUpdate;
            _transactionoriginXPO.LastUpdateBy = (_transactionoriginXPO.LastUpdateBy != null && _transactionoriginXPO.LastUpdateBy.Oid == TransactionOriginDTO.LastUpdateByID) ? _transactionoriginXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(TransactionOriginDTO.LastUpdateByID);
            _transactionoriginXPO.IsActive = _transactionoriginXPO.IsActive == TransactionOriginDTO.IsActive ? (bool)_transactionoriginXPO.IsActive : (bool)TransactionOriginDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _transactionoriginXPO;
    }
}
