
namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
public class Status_Enum
{
    public enum Part_Number_Configurator : int
    {
        Draft = 3,
        Edit = 4,
        Released = 5
    }

    public enum QMS_Document : int
    {
       
        Released = 5,
        Obsolete = 6,
        New = 7
    }

    //Importado para tickets
    public enum Statuses_Enum : int
    {
        Inactive = 17,
        New = 18,
        Closed = 19,
        Active = 20,
        Scrap = 21
    }
}

