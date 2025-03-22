using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValue_DXFilter
{
    public static GroupOperator GetUserDefinedValue_DXFilter(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (UserDefinedValueDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.Oid), UserDefinedValueDTO.ID));
            }
            if (UserDefinedValueDTO.UserDefinedValueIDArray != null && UserDefinedValueDTO.UserDefinedValueIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedValueXPO.Oid), UserDefinedValueDTO.UserDefinedValueIDArray));
            }
            if (UserDefinedValueDTO.Item_LineDTO.ID != null || UserDefinedValueDTO.Item_LineDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.Item_Line), UserDefinedValueDTO.Item_LineDTO.ID));
            }
            if (UserDefinedValueDTO.Item_LineIDArray != null && UserDefinedValueDTO.Item_LineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedValueXPO.Item_Line), UserDefinedValueDTO.Item_LineIDArray));
            }
            if (UserDefinedValueDTO.SupportGroupDTO.ID != null || UserDefinedValueDTO.SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.SupportGroup), UserDefinedValueDTO.SupportGroupDTO.ID));
            }
            if (UserDefinedValueDTO.SupportGroupIDArray != null && UserDefinedValueDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedValueXPO.SupportGroup), UserDefinedValueDTO.SupportGroupIDArray));
            }
            if (UserDefinedValueDTO.UserDefinedDTO.ID != null || UserDefinedValueDTO.UserDefinedDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.UserDefined), UserDefinedValueDTO.UserDefinedDTO.ID));
            }
            if (UserDefinedValueDTO.UserDefinedIDArray != null && UserDefinedValueDTO.UserDefinedIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserDefinedValueXPO.UserDefined), UserDefinedValueDTO.UserDefinedIDArray));
            }
            if (UserDefinedValueDTO.AddedByID != null && UserDefinedValueDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.AddedBy), UserDefinedValueDTO.AddedByID));
            }
            if (UserDefinedValueDTO.LastUpdateByID != null && UserDefinedValueDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.LastUpdateBy), UserDefinedValueDTO.LastUpdateByID));
            }
            if (UserDefinedValueDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserDefinedValueXPO.IsActive), UserDefinedValueDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
