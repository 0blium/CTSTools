using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartType_DXFilter
    {
        public static GroupOperator GetPartType_DXFilter(PartTypeDTO PartTypeDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (PartTypeDTO.ID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(PartTypeXPO.Oid), PartTypeDTO.ID));
                if (!string.IsNullOrEmpty(PartTypeDTO.Name))
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(PartTypeXPO.Name), PartTypeDTO.Name));
                if (PartTypeDTO.PartTypeIDArray != null && PartTypeDTO.PartTypeIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(PartTypeXPO.Oid), PartTypeDTO.PartTypeIDArray));
                if (PartTypeDTO.AddedByID != null && PartTypeDTO.AddedByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(PartTypeXPO.AddedBy), PartTypeDTO.AddedByID));
                if (PartTypeDTO.LastUpdateByID != null && PartTypeDTO.LastUpdateByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(PartTypeXPO.LastUpdateBy), PartTypeDTO.LastUpdateByID));
                if (PartTypeDTO.IsActive != null)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(PartTypeXPO.IsActive), PartTypeDTO.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
