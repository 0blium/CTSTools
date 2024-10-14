using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;

public class MailGroup_DXFilter
{  
   public static GroupOperator GetMailGroup_DXFilter(MailGroupDTO MailGroupDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (MailGroupDTO.ID > 0)
           {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupXPO.Oid), MailGroupDTO.ID));
           }
           if (MailGroupDTO.MailGroupIDArray != null && MailGroupDTO.MailGroupIDArray.Count() > 0 )
           {
                _groupOperator.Operands.Add(new InOperator(nameof(MailGroupXPO.Oid), MailGroupDTO.MailGroupIDArray));
           }
           if (MailGroupDTO.AddedByID != null)
           {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupXPO.AddedBy), MailGroupDTO.AddedByID));
           }
           if (MailGroupDTO.LastUpdateByID != null)
           {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupXPO.LastUpdateBy), MailGroupDTO.LastUpdateByID));
           }
           if (MailGroupDTO.IsActive != null)
           {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupXPO.IsActive), MailGroupDTO.IsActive));
           }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
