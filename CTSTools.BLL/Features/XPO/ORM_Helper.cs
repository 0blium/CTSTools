using CTSTools.BLL.Common;
using CTSTools.DAL.Common;
using Elmah;
using System;
using System.Reflection;

namespace CTSTools.BLL.Features.XPO
{
    public class ORM_Helper
    {
        public static ValidationResultDTO UpdateSchema()
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "La BD se ha actualizado correctamente."
            };
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                Assembly[] array = { typeof(DAL.Features.Management.Edashboard.Dashboard.DashboardXPO).Assembly };
                _unit.UpdateSchema(array);
                _unit.CreateObjectTypeRecords(array);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("Hubo un error al intentar actualizar la BD.");
                throw ex;
            }
            return _validationResultDTO;
        }
    }
}
