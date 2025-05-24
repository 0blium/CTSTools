using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefined_DXFilter
{
    public static GroupOperator GetUserDefined_DXFilter(UserDefinedDTO UserDefinedDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (UserDefinedDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.Oid), UserDefinedDTO.ID));
            }
            if (UserDefinedDTO.UserDefinedIDArray != null && UserDefinedDTO.UserDefinedIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedXPO.Oid), UserDefinedDTO.UserDefinedIDArray));
            }
            if (UserDefinedDTO.SupportGroupID != null || UserDefinedDTO.SupportGroupID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.SupportGroup), UserDefinedDTO.SupportGroupID));
            }
            if (UserDefinedDTO.SupportGroupIDArray != null && UserDefinedDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedXPO.SupportGroup), UserDefinedDTO.SupportGroupIDArray));
            }
            if (UserDefinedDTO.IsMandatory != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.IsMandatory), UserDefinedDTO.IsMandatory));
            }
            if (UserDefinedDTO.DataTypeID != null || UserDefinedDTO.DataTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.DataType), UserDefinedDTO.DataTypeID));
            }
            if (UserDefinedDTO.DataTypeIDArray != null && UserDefinedDTO.DataTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedXPO.DataType), UserDefinedDTO.DataTypeIDArray));
            }
            if (UserDefinedDTO.AddedByID != null && UserDefinedDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.AddedBy), UserDefinedDTO.AddedByID));
            }
            if (UserDefinedDTO.LastUpdateByID != null && UserDefinedDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.LastUpdateBy), UserDefinedDTO.LastUpdateByID));
            }
            if (UserDefinedDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedXPO.IsActive), UserDefinedDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
