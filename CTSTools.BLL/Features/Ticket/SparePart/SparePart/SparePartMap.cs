using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePart;

public class SparePartMap
{
    public static SparePartDTO XPOToDTO(SparePartXPO SparePartXPO)
    {
        var _sparepartDTO = new SparePartDTO();
        try
        {
            _sparepartDTO.ID = SparePartXPO.Oid;
            _sparepartDTO.Name = SparePartXPO.Name;
            _sparepartDTO.Description = SparePartXPO.Description;
            _sparepartDTO.ManufactureID = SparePartXPO.ManufactureID;
            _sparepartDTO.NameWithManufactureID = $"{SparePartXPO.Name} - {SparePartXPO.ManufactureID}";
            _sparepartDTO.AddedDate = (SparePartXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SparePartXPO.AddedDate : (DateTime?)null;
            _sparepartDTO.AddedByID = (SparePartXPO.AddedBy != null) ? SparePartXPO.AddedBy.Oid : 0;
            _sparepartDTO.AddedByName = (SparePartXPO.AddedBy != null) ? SparePartXPO.AddedBy.Name : "Unnassigned";
            _sparepartDTO.LastUpdate = (SparePartXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SparePartXPO.LastUpdate : (DateTime?)null;
            _sparepartDTO.LastUpdateByID = (SparePartXPO.LastUpdateBy != null) ? SparePartXPO.LastUpdateBy.Oid : 0;
            _sparepartDTO.LastUpdateByName = (SparePartXPO.LastUpdateBy != null) ? SparePartXPO.LastUpdateBy.Name : "Unnassigned";
            _sparepartDTO.IsActive = SparePartXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartDTO;
    }

    public static SparePartXPO DTOtoXPO(SparePartDTO SparePartDTO, UnitOfWork UnitOfWork)
    {
        SparePartXPO _sparepartXPO;
        try
        {
            _sparepartXPO = SparePartDTO.ID == null || SparePartDTO.ID == 0 ? new SparePartXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SparePartXPO>(SparePartDTO.ID);
            _sparepartXPO.Name = _sparepartXPO.Name == SparePartDTO.Name ? _sparepartXPO.Name : SparePartDTO.Name;
            _sparepartXPO.Description = _sparepartXPO.Description == SparePartDTO.Description ? _sparepartXPO.Description : SparePartDTO.Description;
            _sparepartXPO.ManufactureID = _sparepartXPO.ManufactureID == SparePartDTO.ManufactureID ? _sparepartXPO.ManufactureID : SparePartDTO.ManufactureID;
            _sparepartXPO.AddedDate = _sparepartXPO.AddedDate ?? SparePartDTO.AddedDate;
            _sparepartXPO.AddedBy = _sparepartXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(SparePartDTO.AddedByID);
            _sparepartXPO.LastUpdate = _sparepartXPO.LastUpdate == SparePartDTO.LastUpdate ? _sparepartXPO.LastUpdate : SparePartDTO.LastUpdate;
            _sparepartXPO.LastUpdateBy = (_sparepartXPO.LastUpdateBy?.Oid == SparePartDTO.LastUpdateByID) ? _sparepartXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SparePartDTO.LastUpdateByID);
            _sparepartXPO.IsActive = (bool)(_sparepartXPO.IsActive == SparePartDTO.IsActive ? (bool)_sparepartXPO.IsActive : (bool)SparePartDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartXPO;
    }
}
