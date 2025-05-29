using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplate_DXFilter
{
    public static GroupOperator GetUserDefinedTemplate_DXFilter(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (UserDefinedTemplateDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.Oid), UserDefinedTemplateDTO.ID));
            }
            if (UserDefinedTemplateDTO.UserDefinedTemplateIDArray != null && UserDefinedTemplateDTO.UserDefinedTemplateIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedTemplateXPO.Oid), UserDefinedTemplateDTO.UserDefinedTemplateIDArray));
            }
            if (UserDefinedTemplateDTO.Item_HeaderID != null || UserDefinedTemplateDTO.Item_HeaderID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.Item_Header), UserDefinedTemplateDTO.Item_HeaderID));
            }
            if (UserDefinedTemplateDTO.Item_SupportGroupDTO.ID != null || UserDefinedTemplateDTO.Item_SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.Item_SupportGroup), UserDefinedTemplateDTO.Item_SupportGroupDTO.ID));
            }
            if (UserDefinedTemplateDTO.Item_SupportGroupIDArray != null && UserDefinedTemplateDTO.Item_SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedTemplateXPO.Item_SupportGroup), UserDefinedTemplateDTO.Item_SupportGroupIDArray));
            }
            if (UserDefinedTemplateDTO.UserDefinedDTO.ID != null || UserDefinedTemplateDTO.UserDefinedDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.UserDefined), UserDefinedTemplateDTO.UserDefinedDTO.ID));
            }
            if (UserDefinedTemplateDTO.UserDefinedIDArray != null && UserDefinedTemplateDTO.UserDefinedIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedTemplateXPO.UserDefined), UserDefinedTemplateDTO.UserDefinedIDArray));
            }
            if (UserDefinedTemplateDTO.AddedByID != null && UserDefinedTemplateDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.AddedBy), UserDefinedTemplateDTO.AddedByID));
            }
            if (UserDefinedTemplateDTO.LastUpdateByID != null && UserDefinedTemplateDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.LastUpdateBy), UserDefinedTemplateDTO.LastUpdateByID));
            }
            if (UserDefinedTemplateDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedTemplateXPO.IsActive), UserDefinedTemplateDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
