using System.IO;
using System;
using CTSTools.BLL.Common.Files;
using Elmah;

namespace CTSTools.BLL.Common.Directories;

public class Directory_Service
{
    public static ValidationResultDTO Create(string URL)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(URL))
            {
                _validationResultDTO.Data = URL;
            }
            else
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(URL);
                _validationResultDTO.Data = URL;
            }
        }
        catch (Exception ex)
        {
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = "There was an error trying to create directoty";
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO Delete(string URL)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been  successfully deleted"
        };
        try
        {
            // Get File Directory
            if (Directory.Exists(URL))
                Directory.Delete(URL, true);

        }
        catch (Exception ex)
        {
            _ValidationResultDTO.Description = "Error";
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = string.Format("Hubo un error al intentar borrar la carpeta. {0}", ex.Message);
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ValidationResultDTO;
    }

}
