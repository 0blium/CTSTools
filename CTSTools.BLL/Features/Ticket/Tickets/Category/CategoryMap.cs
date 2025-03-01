using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using CTSTools.DAL.Features.Ticket.Ticket;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Tickets.Category;

public class CategoryMap
{
    public static CategoryDTO XPOToDTO(CategoryXPO CategoryXPO)
    {
        var _categoryDTO = new CategoryDTO();
        try
        {
            _categoryDTO.ID = CategoryXPO.Oid;
            _categoryDTO.Name = CategoryXPO.Name;
            _categoryDTO.Description = CategoryXPO.Description;
            _categoryDTO.HasParent = CategoryXPO.HasParent;
            _categoryDTO.ParentID = (CategoryXPO.Parent != null) ? CategoryXPO.Parent.Oid : 0;
            _categoryDTO.ParentName = (CategoryXPO.Parent != null) ? CategoryXPO.Parent.Name : "Unnasigned";
            _categoryDTO.NameWithParent = (CategoryXPO.Parent != null) ? $"{CategoryXPO.Parent.Name} - {CategoryXPO.Name}" : $"{CategoryXPO.Name}";
            _categoryDTO.SupportGroupDTO.ID = (CategoryXPO.SupportGroup != null) ? CategoryXPO.SupportGroup.Oid : 0;
            _categoryDTO.SupportGroupDTO.EnglishName = (CategoryXPO.SupportGroup != null) ? CategoryXPO.SupportGroup.EnglishName : "Unnassigned";
            _categoryDTO.AddedDate = (CategoryXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? CategoryXPO.AddedDate : (DateTime?)null;
            _categoryDTO.AddedByID = (CategoryXPO.AddedBy != null) ? CategoryXPO.AddedBy.Oid : 0;
            _categoryDTO.AddedByName = (CategoryXPO.AddedBy != null) ? CategoryXPO.AddedBy.Name : "Unnassigned";
            _categoryDTO.LastUpdate = (CategoryXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? CategoryXPO.LastUpdate : (DateTime?)null;
            _categoryDTO.LastUpdateByID = (CategoryXPO.LastUpdateBy != null) ? CategoryXPO.LastUpdateBy.Oid : 0;
            _categoryDTO.LastUpdateByName = (CategoryXPO.LastUpdateBy != null) ? CategoryXPO.LastUpdateBy.Name : "Unnassigned";
            _categoryDTO.IsActive = CategoryXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _categoryDTO;
    }

    public static CategoryXPO DTOtoXPO(CategoryDTO CategoryDTO, UnitOfWork UnitOfWork)
    {
        CategoryXPO _categoryXPO;
        try
        {
            _categoryXPO = CategoryDTO.ID == null || CategoryDTO.ID == 0 ? new CategoryXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<CategoryXPO>(CategoryDTO.ID);
            _categoryXPO.Name = _categoryXPO.Name == CategoryDTO.Name ? _categoryXPO.Name : CategoryDTO.Name;
            _categoryXPO.Description = _categoryXPO.Description == CategoryDTO.Description ? _categoryXPO.Description : CategoryDTO.Description;
            _categoryXPO.HasParent = CategoryDTO.HasParent == null ? _categoryXPO.HasParent : _categoryXPO.HasParent == (bool)CategoryDTO.HasParent ? _categoryXPO.HasParent : (bool)CategoryDTO.HasParent;
            _categoryXPO.Parent = (_categoryXPO.Parent?.Oid == CategoryDTO.ParentID) ? _categoryXPO.Parent : UnitOfWork.GetObjectByKey<CategoryXPO>(CategoryDTO.ParentID);
            _categoryXPO.SupportGroup = (_categoryXPO.SupportGroup?.Oid == CategoryDTO.SupportGroupDTO.ID) ? _categoryXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(CategoryDTO.SupportGroupDTO.ID);
            _categoryXPO.AddedDate = _categoryXPO.AddedDate ?? CategoryDTO.AddedDate;
            _categoryXPO.AddedBy = _categoryXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(CategoryDTO.AddedByID);
            _categoryXPO.LastUpdate = _categoryXPO.LastUpdate == CategoryDTO.LastUpdate ? _categoryXPO.LastUpdate : CategoryDTO.LastUpdate;
            _categoryXPO.LastUpdateBy = (_categoryXPO.LastUpdateBy?.Oid == CategoryDTO.LastUpdateByID) ? _categoryXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(CategoryDTO.LastUpdateByID);
            _categoryXPO.IsActive = _categoryXPO.IsActive == CategoryDTO.IsActive ? (bool)_categoryXPO.IsActive : (bool)CategoryDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _categoryXPO;
    }
}
