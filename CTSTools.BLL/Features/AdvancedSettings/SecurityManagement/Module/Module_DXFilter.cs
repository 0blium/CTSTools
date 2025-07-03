using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;

public class Module_DXFilter
{
    public static GroupOperator GetModule_DXFilter(ModuleDTO ModuleDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ModuleDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ModuleXPO.Oid), ModuleDTO.ID));
            if (!string.IsNullOrEmpty(ModuleDTO.Name))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ModuleXPO.Name), ModuleDTO.Name));
            if (ModuleDTO.ModuleIDArray != null && ModuleDTO.ModuleIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(ModuleXPO.Oid), ModuleDTO.ModuleIDArray));
            if (ModuleDTO.AddedByID != null && ModuleDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ModuleXPO.AddedBy), ModuleDTO.AddedByID));
            if (ModuleDTO.LastUpdateByID != null && ModuleDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ModuleXPO.LastUpdateBy), ModuleDTO.LastUpdateByID));
            if (ModuleDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ModuleXPO.IsActive), ModuleDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
