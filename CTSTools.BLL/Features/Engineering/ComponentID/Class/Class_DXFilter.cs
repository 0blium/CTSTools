using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.Class
{
    public class Class_DXFilter
    {
        public static GroupOperator GetClass_DXFilter(ClassDTO ClassDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (ClassDTO.ID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.Oid), ClassDTO.ID));
                if (!string.IsNullOrEmpty(ClassDTO.Name))
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.Name), ClassDTO.Name));
                if (ClassDTO.ClassIDArray != null && ClassDTO.ClassIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.Oid), ClassDTO.ClassIDArray));
                if (ClassDTO.AttributeID != null || ClassDTO.AttributeID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.Attribute), ClassDTO.AttributeID));
                if (ClassDTO.AttributeIDArray != null && ClassDTO.AttributeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.Attribute), ClassDTO.AttributeIDArray));
                if (ClassDTO.ValueID != null || ClassDTO.ValueID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.Value), ClassDTO.ValueID));
                if (ClassDTO.ValueIDArray != null && ClassDTO.ValueIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.Value), ClassDTO.ValueIDArray));
                if (ClassDTO.ValueLinkID != null || ClassDTO.ValueLinkID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.ValueLink), ClassDTO.ValueLinkID));
                if (ClassDTO.ValueLinkIDArray != null && ClassDTO.ValueLinkIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.ValueLink), ClassDTO.ValueLinkIDArray));
                if (ClassDTO.PartTypeID != null || ClassDTO.PartTypeID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.PartType), ClassDTO.PartTypeID));
                if (ClassDTO.PartTypeIDArray != null && ClassDTO.PartTypeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.PartType), ClassDTO.PartTypeIDArray));
                if (ClassDTO.ComponentTypeID != null || ClassDTO.ComponentTypeID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.ComponentType), ClassDTO.ComponentTypeID));
                if (ClassDTO.ComponentTypeIDArray != null && ClassDTO.ComponentTypeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ClassXPO.ComponentType), ClassDTO.ComponentTypeIDArray));
                if (ClassDTO.AddedByID != null && ClassDTO.AddedByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.AddedBy), ClassDTO.AddedByID));
                if (ClassDTO.LastUpdateByID != null && ClassDTO.LastUpdateByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.LastUpdateBy), ClassDTO.LastUpdateByID));
                if (ClassDTO.IsActive != null)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ClassXPO.IsActive), ClassDTO.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
