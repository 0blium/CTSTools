using CTSTools.DAL.Features.Engineering.ComponentID.ComponentType;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentType_DXFilter
    {
        public static GroupOperator GetComponentType_DXFilter(ComponentTypeDTO ComponentTypeDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (ComponentTypeDTO.ID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.Oid), ComponentTypeDTO.ID));
                if (!string.IsNullOrEmpty(ComponentTypeDTO.Name))
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.Name), ComponentTypeDTO.Name));
                if (ComponentTypeDTO.PartTypeID != null || ComponentTypeDTO.PartTypeID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.PartType), ComponentTypeDTO.PartTypeID));
                if (ComponentTypeDTO.PartTypeIDArray != null && ComponentTypeDTO.PartTypeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ComponentTypeXPO.PartType), ComponentTypeDTO.PartTypeIDArray));
                if (ComponentTypeDTO.ComponentTypeIDArray != null && ComponentTypeDTO.ComponentTypeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(ComponentTypeXPO.Oid), ComponentTypeDTO.ComponentTypeIDArray));
                if (ComponentTypeDTO.AddedByID != null && ComponentTypeDTO.AddedByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.AddedBy), ComponentTypeDTO.AddedByID));
                if (ComponentTypeDTO.LastUpdateByID != null && ComponentTypeDTO.LastUpdateByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.LastUpdateBy), ComponentTypeDTO.LastUpdateByID));
                if (ComponentTypeDTO.IsActive != null)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(ComponentTypeXPO.IsActive), ComponentTypeDTO.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
