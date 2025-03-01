using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Tickets.Category;

public class Category_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateCategory_Global(CategoryDTO CategoryDTO)
    {
        var _ValidationResultDTO = Category_Validator.CreateCategory_Validation(CategoryDTO);
        if (_ValidationResultDTO.Result)
        {
            CategoryDTO.HasParent = (CategoryDTO.ParentID == 0 || CategoryDTO.ParentID == null) ? false : true;

            CategoryDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Category_Repository.CreateCategory(CategoryDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<CategoryDTO>(CategoryDTO, (int)CategoryDTO.AddedByID, (int)CategoryDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateCategory_Global(CategoryDTO CategoryDTO)
    {
        var _ValidationResultDTO = Category_Validator.UpdateCategory_Validation(CategoryDTO);
        var _previousCategoryDTO = GetCategoryList_Global(new CategoryDTO { ID = CategoryDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            CategoryDTO.HasParent = (CategoryDTO.ParentID == 0 || CategoryDTO.ParentID == null) ? false : true;
            CategoryDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Category_Repository.UpdateCategory(CategoryDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<CategoryDTO>(_previousCategoryDTO, CategoryDTO, (int)CategoryDTO.LastUpdateByID, (int)CategoryDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteCategory_Global(CategoryDTO CategoryDTO)
    {
        var _ValidationResultDTO = Category_Validator.DeleteCategory_Validation(CategoryDTO);
        var _previousCategoryDTO = GetCategoryList_Global(new CategoryDTO { ID = CategoryDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Category_Repository.DeleteCategory(CategoryDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<CategoryDTO>(_previousCategoryDTO, (int)CategoryDTO.LastUpdateByID, (int)CategoryDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<CategoryDTO> GetCategoryList_Global(CategoryDTO CategoryDTO, PagedResultDTO<CategoryDTO> PagedResultDTO = null)
    {
        var _categoryglobalList = new List<CategoryDTO>();
        try
        {
            var _categoryList = Category_Repository.GetCategoryList(CategoryDTO, PagedResultDTO);
            // if Category is empty, return list
            if (_categoryList.Count() == 0)
            {
                _categoryglobalList = _categoryList;
                return _categoryglobalList;
            }
            if (!CategoryDTO.GetSupportGroupDTO)
            {
                _categoryglobalList = _categoryList;
                return _categoryglobalList;
            }
            _categoryglobalList = GetCategoryRelatedData(CategoryDTO, _categoryList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _categoryglobalList;
    }



    public static List<CategoryDTO> GetCategoryRelatedData(CategoryDTO CategoryDTO, List<CategoryDTO> CategoryList)
    {
        var _categoryglobalList = new List<CategoryDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();

        try
        {
            if (CategoryDTO.GetSupportGroupDTO)
            {
                CategoryDTO.SupportGroupDTO.SupportGroupIDArray = CategoryList.GroupBy(g => g.SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(CategoryDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _categoryDTO in CategoryList)
            {
                if (CategoryDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_categoryDTO.SupportGroupDTO.ID))
                {
                    _categoryDTO.SupportGroupDTO = _supportgroupDict[_categoryDTO.SupportGroupDTO.ID];
                }
                _categoryglobalList.Add(_categoryDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _categoryglobalList;
    }




    public static int GetCategoryTotalCount(PagedResultDTO<CategoryDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Category_Repository.GetCategoryCount(PagedResultDTO.Filter, PagedResultDTO);
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
