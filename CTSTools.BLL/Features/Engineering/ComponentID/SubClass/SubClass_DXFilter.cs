using CTSTools.BLL.Features.Engineering.ComponentID.SubClass;
using CTSTools.DAL.Features.Engineering.ComponentID.SubClass;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClass_DXFilter
    {
        public static GroupOperator GetSubClass_DXFilter(SubClassDTO SubClassDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (SubClassDTO.ID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.Oid), SubClassDTO.ID));
                if (!string.IsNullOrEmpty(SubClassDTO.Name))
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.Name), SubClassDTO.Name));
                if (SubClassDTO.ClassID != null || SubClassDTO.ClassID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.Class), SubClassDTO.ClassID));
                if (SubClassDTO.ClassIDArray != null && SubClassDTO.ClassIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(SubClassXPO.Class), SubClassDTO.ClassIDArray));
                if (SubClassDTO.SubClassIDArray != null && SubClassDTO.SubClassIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(SubClassXPO.Oid), SubClassDTO.SubClassIDArray));
                if (SubClassDTO.SubClassNameArray != null && SubClassDTO.SubClassNameArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(SubClassXPO.Name), SubClassDTO.SubClassNameArray));
                if (SubClassDTO.AddedByID != null && SubClassDTO.AddedByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.AddedBy), SubClassDTO.AddedByID));
                if (SubClassDTO.LastUpdateByID != null && SubClassDTO.LastUpdateByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.LastUpdateBy), SubClassDTO.LastUpdateByID));
                if (SubClassDTO.IsActive != null)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClassXPO.IsActive), SubClassDTO.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
