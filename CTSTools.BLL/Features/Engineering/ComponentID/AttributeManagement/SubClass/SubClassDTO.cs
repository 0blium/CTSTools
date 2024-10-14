using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;

public class SubClassDTO: ValueLinkDTO
{
    #region Base Properties
    public ValueDTO PartTypeDTO { get; set; }
    public ValueDTO ComponentTypeDTO { get; set; }
    public ValueDTO ClassDTO { get; set; }
    public ValueDTO SubClassValueDTO { get; set; }
    #endregion

    #region Constructorx
    public SubClassDTO()
    {
        PartTypeDTO = new ValueDTO();
        ComponentTypeDTO = new ValueDTO();
        ClassDTO = new ValueDTO();        
    }
    #endregion
}

