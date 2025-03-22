using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.DataType;

public class DataTypeMap
{
    public static DataTypeDTO XPOToDTO(DataTypeXPO DataTypeXPO)
    {
        var _datatypeDTO = new DataTypeDTO();
        try
        {
            _datatypeDTO.ID = DataTypeXPO.Oid;
            _datatypeDTO.Name = DataTypeXPO.Name;
            _datatypeDTO.Description = DataTypeXPO.Description;
            _datatypeDTO.AddedDate = (DataTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DataTypeXPO.AddedDate : (DateTime?)null;
            _datatypeDTO.AddedByID = (DataTypeXPO.AddedBy != null) ? DataTypeXPO.AddedBy.Oid : 0;
            _datatypeDTO.AddedByName = (DataTypeXPO.AddedBy != null) ? DataTypeXPO.AddedBy.Name : "Unnassigned";
            _datatypeDTO.LastUpdate = (DataTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DataTypeXPO.LastUpdate : (DateTime?)null;
            _datatypeDTO.LastUpdateByID = (DataTypeXPO.LastUpdateBy != null) ? DataTypeXPO.LastUpdateBy.Oid : 0;
            _datatypeDTO.LastUpdateByName = (DataTypeXPO.LastUpdateBy != null) ? DataTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _datatypeDTO.IsActive = DataTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _datatypeDTO;
    }

    public static DataTypeXPO DTOtoXPO(DataTypeDTO DataTypeDTO, UnitOfWork UnitOfWork)
    {
        DataTypeXPO _datatypeXPO;
        try
        {
            _datatypeXPO = DataTypeDTO.ID == null || DataTypeDTO.ID == 0 ? new DataTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DataTypeXPO>(DataTypeDTO.ID);
            _datatypeXPO.Name = _datatypeXPO.Name == DataTypeDTO.Name ? _datatypeXPO.Name : DataTypeDTO.Name;
            _datatypeXPO.Description = _datatypeXPO.Description == DataTypeDTO.Description ? _datatypeXPO.Description : DataTypeDTO.Description;
            _datatypeXPO.AddedDate = _datatypeXPO.AddedDate != null ? _datatypeXPO.AddedDate : DataTypeDTO.AddedDate;
            _datatypeXPO.AddedBy = (_datatypeXPO.AddedBy != null && _datatypeXPO.AddedBy.Oid == DataTypeDTO.AddedByID) ? _datatypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DataTypeDTO.AddedByID);
            _datatypeXPO.LastUpdate = _datatypeXPO.LastUpdate == DataTypeDTO.LastUpdate ? _datatypeXPO.LastUpdate : DataTypeDTO.LastUpdate;
            _datatypeXPO.LastUpdateBy = (_datatypeXPO.LastUpdateBy != null && _datatypeXPO.LastUpdateBy.Oid == DataTypeDTO.LastUpdateByID) ? _datatypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DataTypeDTO.LastUpdateByID);
            _datatypeXPO.IsActive = _datatypeXPO.IsActive == DataTypeDTO.IsActive ? (bool)_datatypeXPO.IsActive : (bool)DataTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _datatypeXPO;
    }
}
