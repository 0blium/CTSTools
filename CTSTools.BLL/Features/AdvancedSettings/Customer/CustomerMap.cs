using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;


namespace CTSTools.BLL.Features.AdvancedSettings.Customer;

public class CustomerMap
{
    public static CustomerDTO XPOToDTO(CustomerXPO CustomerXPO)
    {
        var _customerDTO = new CustomerDTO();
        try
        {
            _customerDTO.ID = CustomerXPO.Oid;
            _customerDTO.Name = CustomerXPO.Name;
            _customerDTO.Description = CustomerXPO.Description;
            _customerDTO.AddedDate = (CustomerXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? CustomerXPO.AddedDate : (DateTime?)null;
            _customerDTO.AddedByID = (CustomerXPO.AddedBy != null) ? CustomerXPO.AddedBy.Oid : 0;
            _customerDTO.AddedByName = (CustomerXPO.AddedBy != null) ? CustomerXPO.AddedBy.Name : "Unnassigned";
            _customerDTO.LastUpdate = (CustomerXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? CustomerXPO.LastUpdate : (DateTime?)null;
            _customerDTO.LastUpdateByID = (CustomerXPO.LastUpdateBy != null) ? CustomerXPO.LastUpdateBy.Oid : 0;
            _customerDTO.LastUpdateByName = (CustomerXPO.LastUpdateBy != null) ? CustomerXPO.LastUpdateBy.Name : "Unnassigned";
            _customerDTO.IsActive = CustomerXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _customerDTO;
    }

    public static CustomerXPO DTOtoXPO(CustomerDTO CustomerDTO, UnitOfWork UnitOfWork)
    {
        CustomerXPO _customerXPO;
        try
        {
            _customerXPO = CustomerDTO.ID == null || CustomerDTO.ID == 0 ? new CustomerXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<CustomerXPO>(CustomerDTO.ID);
            _customerXPO.Name = _customerXPO.Name == CustomerDTO.Name ? _customerXPO.Name : CustomerDTO.Name;
            _customerXPO.Description = _customerXPO.Description == CustomerDTO.Description ? _customerXPO.Description : CustomerDTO.Description;
            _customerXPO.AddedDate = _customerXPO.AddedDate != null ? _customerXPO.AddedDate : CustomerDTO.AddedDate;
            _customerXPO.AddedBy = (_customerXPO.AddedBy != null) ? _customerXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(CustomerDTO.AddedByID);
            _customerXPO.LastUpdate = _customerXPO.LastUpdate == CustomerDTO.LastUpdate ? _customerXPO.LastUpdate : CustomerDTO.LastUpdate;
            _customerXPO.LastUpdateBy = (_customerXPO.LastUpdateBy != null && _customerXPO.LastUpdateBy.Oid == CustomerDTO.LastUpdateByID) ? _customerXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(CustomerDTO.LastUpdateByID);
            _customerXPO.IsActive = _customerXPO.IsActive == CustomerDTO.IsActive ? (bool)_customerXPO.IsActive : (bool)CustomerDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _customerXPO;
    }

}
