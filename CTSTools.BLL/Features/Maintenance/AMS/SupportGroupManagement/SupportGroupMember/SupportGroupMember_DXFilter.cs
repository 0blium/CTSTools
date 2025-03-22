using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMember_DXFilter
    {  
       public static GroupOperator GetSupportGroupMember_DXFilter(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (SupportGroupMemberDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.Oid), SupportGroupMemberDTO.ID));
               }
               if (SupportGroupMemberDTO.SupportGroupMemberIDArray != null && SupportGroupMemberDTO.SupportGroupMemberIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupMemberXPO.Oid), SupportGroupMemberDTO.SupportGroupMemberIDArray));
               }
               if ( SupportGroupMemberDTO.SupportGroupDTO.ID != null || SupportGroupMemberDTO.SupportGroupDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.SupportGroup), SupportGroupMemberDTO.SupportGroupDTO.ID));
               }
               if (SupportGroupMemberDTO.SupportGroupIDArray != null && SupportGroupMemberDTO.SupportGroupIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupMemberXPO.SupportGroup), SupportGroupMemberDTO.SupportGroupIDArray));
               }
               if ( SupportGroupMemberDTO.UserDTO.ID != null || SupportGroupMemberDTO.UserDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.User), SupportGroupMemberDTO.UserDTO.ID));
               }
               if (SupportGroupMemberDTO.UserIDArray != null && SupportGroupMemberDTO.UserIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupMemberXPO.User), SupportGroupMemberDTO.UserIDArray));
               }
               if ( SupportGroupMemberDTO.RoleDTO.ID != null || SupportGroupMemberDTO.RoleDTO.ID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.Role), SupportGroupMemberDTO.RoleDTO.ID));
               }
               if (SupportGroupMemberDTO.RoleIDArray != null && SupportGroupMemberDTO.RoleIDArray.Count() > 0 )
               {
                    _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupMemberXPO.Role), SupportGroupMemberDTO.RoleIDArray));
               }
               if (SupportGroupMemberDTO.AddedByID != null && SupportGroupMemberDTO.AddedByID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.AddedBy), SupportGroupMemberDTO.AddedByID));
               }
               if (SupportGroupMemberDTO.LastUpdateByID != null && SupportGroupMemberDTO.LastUpdateByID > 0)
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.LastUpdateBy), SupportGroupMemberDTO.LastUpdateByID));
               }
               if (SupportGroupMemberDTO.IsActive != null )
               {
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupMemberXPO.IsActive), SupportGroupMemberDTO.IsActive));
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
