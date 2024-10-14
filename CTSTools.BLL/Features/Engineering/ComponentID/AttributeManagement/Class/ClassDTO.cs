
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;


public class ClassDTO : ValueLinkDTO
{
    #region Base Properties
    public ValueDTO PartTypeDTO { get; set; }
    public ValueDTO ComponentTypeDTO { get; set; }
    public ValueDTO ClassValueDTO { get; set; }
    #endregion

    #region Constructor
    public ClassDTO()
    {
        PartTypeDTO = new ValueDTO();
        ComponentTypeDTO = new ValueDTO();
    }
    #endregion
}

