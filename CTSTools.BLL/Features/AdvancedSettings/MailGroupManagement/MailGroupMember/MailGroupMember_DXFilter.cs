using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;

namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMember_DXFilter
    {  
       public static GroupOperator GetMailGroupMember_DXFilter(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (MailGroupMemberDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.Oid), MailGroupMemberDTO.ID));
               }
               if (MailGroupMemberDTO.MailGroupMemberIDArray != null && MailGroupMemberDTO.MailGroupMemberIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(MailGroupMemberXPO.Oid), MailGroupMemberDTO.MailGroupMemberIDArray));
               }
               if ( MailGroupMemberDTO.MailGroupDTO.ID != null || MailGroupMemberDTO.MailGroupDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.MailGroup), MailGroupMemberDTO.MailGroupDTO.ID));
               }
               if (MailGroupMemberDTO.MailGroupIDArray != null && MailGroupMemberDTO.MailGroupIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(MailGroupMemberXPO.MailGroup), MailGroupMemberDTO.MailGroupIDArray));
               }
               if ( MailGroupMemberDTO.UserDTO.ID != null || MailGroupMemberDTO.UserDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.User), MailGroupMemberDTO.UserDTO.ID));
               }
               if (MailGroupMemberDTO.UserIDArray != null && MailGroupMemberDTO.UserIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(MailGroupMemberXPO.User), MailGroupMemberDTO.UserIDArray));
               }
               if (MailGroupMemberDTO.AddedByID != null)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.AddedBy), MailGroupMemberDTO.AddedByID));
               }
               if (MailGroupMemberDTO.LastUpdateByID != null)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.LastUpdateBy), MailGroupMemberDTO.LastUpdateByID));
               }
               if (MailGroupMemberDTO.IsActive != null)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(MailGroupMemberXPO.IsActive), MailGroupMemberDTO.IsActive));
               }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
