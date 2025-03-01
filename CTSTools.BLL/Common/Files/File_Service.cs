using Elmah;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CTSTools.BLL.Common.Files;

public class File_Service
{

    #region Global functions
    public static ValidationResultDTO CreateMultipleFiles_Global(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {

            // Save Files
            foreach (var _fileDTO in FileDTO.FileList)
            {
                _fileDTO.FileDirectory = FileDTO.FileDirectory;
                _fileDTO.ID = FileDTO.ID;
                _ValidationResultDTO = CreateFile_Global(_fileDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to Save the file";
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO CreateFile_Global(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            // Validate file
            _ValidationResultDTO = Size_Validation(FileDTO);
            if (_ValidationResultDTO.Result)
                _ValidationResultDTO = CreateFile(FileDTO);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to Save the file";
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateFile_Global(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been  successfully updated"
        };
        try
        {
            // Validate File
            _validationResultDTO = Size_Validation(FileDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // Delete all files
            _validationResultDTO = DeleteMultipleFiles(FileDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // Save File
            _validationResultDTO = CreateFile(FileDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = "There was an error trying to save the file";
        }
        return _validationResultDTO;
    }
    public static FileDTO GetFile(FileDTO FileDTO)
    {
        try
        {
            // Validate if directory exist
            if (!Directory.Exists(FileDTO.URL))
                return FileDTO;
            // Get Files
            FileDTO.DirectoryArray = Directory.GetFiles(FileDTO.URL);
            FileDTO.DirectoryArray = FileDTO.DirectoryArray.Where(S => !S.Contains(".db")).ToArray();
            if (FileDTO.DirectoryArray.Count() == 0)
                return FileDTO;

            FileDTO.Name = Path.GetFileNameWithoutExtension(FileDTO.DirectoryArray[0]);
            FileDTO.FileName = Path.GetFileName(FileDTO.DirectoryArray[0]);
            FileDTO.Extension = Path.GetExtension(FileDTO.DirectoryArray[0]);
            FileDTO.MIMEType = MimeMapping.GetMimeMapping(FileDTO.FileName);
            FileDTO.URL = string.Format("{0}\\{1}", FileDTO.URL, Path.GetFileName(FileDTO.DirectoryArray[0])).Replace("\\", "/");
            var bytes = File.ReadAllBytes(FileDTO.URL);
            FileDTO.Data = Convert.ToBase64String(bytes);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return FileDTO;
    }
    public static List<FileDTO> GetFileList(FileDTO FileDTO)
    {
        var _fileList = new List<FileDTO>();
        try
        {
            // Validate if directory exist
            if (!Directory.Exists(FileDTO.URL))
                return _fileList;
            // Get Files
            FileDTO.DirectoryArray = Directory.GetFiles(FileDTO.URL);
            FileDTO.DirectoryArray = FileDTO.DirectoryArray.Where(S => !S.Contains(".db")).ToArray();

            foreach (var _directory in FileDTO.DirectoryArray)
            {
                var _fileDTO = new FileDTO();
                _fileDTO.Name = Path.GetFileNameWithoutExtension(_directory);
                _fileDTO.Extension = Path.GetExtension(_directory);
                _fileDTO.FileName = _fileDTO.Name + _fileDTO.Extension;
                _fileDTO.Icon = GetIconURLByFileExtension(_fileDTO);
                _fileDTO.URL = string.Format("{0}{1}", FileDTO.URL, Path.GetFileName(_directory)).Replace("\\", "/");
                _fileList.Add(_fileDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _fileList;
    }
    #endregion

    #region Business Logic       
    public static ValidationResultDTO CreateFile(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            //Remove invalid characters
            char[] _invalidChar = Path.GetInvalidFileNameChars();
            foreach (char _result in _invalidChar)
            {
                FileDTO.Name = FileDTO.Name.Replace(_result.ToString(), "");
            }
            //Convert file to Byte Array
            Byte[] _convertImageBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);

            // Validate if exist a folder
            if (!Directory.Exists(FileDTO.URL))
                // Create Directory
                Directory.CreateDirectory(FileDTO.URL);

            
            // Save file on new folder
            File.WriteAllBytes(String.Format("{0}\\{1}", FileDTO.URL, FileDTO.Name), _convertImageBytes);
            _ValidationResultDTO.Data = FileDTO.URL;
        }
        catch (Exception ex)
        {
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = ex.Message;
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteMultipleFiles(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been  successfully deleted"
        };
        try
        {
            // Validate if directory exist
            if (!Directory.Exists(FileDTO.URL))
                return _ValidationResultDTO;

            FileDTO.URL = string.Format("{0}\\", FileDTO.URL);
            if (Directory.GetFiles(string.Format("{0}\\", FileDTO.URL)).Length == 0)
                return _ValidationResultDTO;

            foreach (string _fileDirectories in Directory.GetFiles(FileDTO.URL))
            {
                string fileName = Path.GetFileName(_fileDirectories);
                // Ignore 'Thumbs.db' because throw exception 
                if (fileName != null && !fileName.Equals("Thumbs.db", StringComparison.OrdinalIgnoreCase))
                    File.Delete(_fileDirectories);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to delete the file";
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Size_Validation(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            //Validate file size Limit (5MB)
            if (Convert.ToInt32(FileDTO.Size) > (int)File_Enum.MB_5)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Error loading file",
                    Description = String.Format("File {0} exceeds the limit (5 MB)", FileDTO.Name)
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "Errors!";
                _ValidationResultDTO.Description = "There is a list of errors exist.";
                _ValidationResultDTO.ValidationResultList = _validation_ResultList;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Errors!";
            _ValidationResultDTO.Description = string.Format("There was an error while trying to save the record. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
    public static string GetIconURLByFileExtension(FileDTO FileDTO)
    {
        string _path = "../../../Common/Assets/img/FileIcons/";
        string _icon = "doc.png";
        switch (FileDTO.Extension.ToLower())
        {
            default:
                _icon = "txt.png";
                break;
            case ".docx":
                _icon = "doc.png";
                break;
            case ".jpg":
                _icon = "image.png";
                break;
            case ".pdf":
                _icon = "pdf.png";
                break;
            case ".png":
                _icon = "image.png";
                break;
            case ".xlsx":
                _icon = "xls.png";
                break;
            case ".msg":
                _icon = "email.png";
                break;
        }

        return string.Format("{0}{1}", _path, _icon);
    }

    #endregion


    //Migración del modulo
    public static ValidationResultDTO DeleteFile(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been  successfully deleted"
        };
        try
        {
            //Get File Directory
            FileDTO = GetDirectory(FileDTO).Data;
            FileDTO.URL = String.Format("{0}{1}\\", FileDTO.URL, FileDTO.ID);

            if (Directory.Exists(FileDTO.URL))
            {
                // Update URL to file directory
                FileDTO.URL = string.Format("{0}{1}{2}", FileDTO.URL, FileDTO.Name, FileDTO.Extension);

                // Delete File
                File.Delete(FileDTO.URL);
            }
            else
            {
                _ValidationResultDTO.Description = "Error";
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "The path to this file has not been found.";
            }
        }
        catch (Exception ex)
        {
            _ValidationResultDTO.Description = "Error";
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = string.Format("There was an error trying to delete the file. {0}", ex.Message);
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO GetDirectory(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO();
        try
        {
            switch (FileDTO.FileDirectory)
            {
                case (int)FileDirectory_Enum.ItemHeaderPictureDirectory:
                    FileDTO.URL = ConfigurationManager.AppSettings["ItemHeaderPictureDirectory"].ToString();
                    break;
                case (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory:
                    FileDTO.URL = ConfigurationManager.AppSettings["ItemHeaderAttachmentDirectory"].ToString();
                    break; ;
                case (int)FileDirectory_Enum.ItemLineAttachmentsDirectory:
                    FileDTO.URL = ConfigurationManager.AppSettings["ItemLineAttachmentDirectory"].ToString();
                    break;
                case (int)FileDirectory_Enum.SparePartPictureDirectory:
                    FileDTO.URL = ConfigurationManager.AppSettings["SparePartPictureDirectory"].ToString();
                    break;
                case (int)FileDirectory_Enum.TicketAttachmentsDirectory:
                    FileDTO.URL = ConfigurationManager.AppSettings["TicketAttachmentDirectory"].ToString();
                    break;
            }
            _ValidationResultDTO.Data = FileDTO;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error!";
            _ValidationResultDTO.Description = string.Format("There was an error trying to validate the directory of USTechPortal. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO SaveFile(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            //Remove invalid characters
            char[] _invalidChar = Path.GetInvalidFileNameChars();
            foreach (char _result in _invalidChar)
            {
                FileDTO.Name = FileDTO.Name.Replace(_result.ToString(), "");
            }
            //Convert file to Byte Array
            Byte[] _convertImageBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);

            // Get File Directory URL
            FileDTO = GetDirectory(FileDTO).Data;
            FileDTO.URL = String.Format("{0}\\{1}", FileDTO.URL, FileDTO.ID);

            // Validate if exist a folder
            if (!Directory.Exists(FileDTO.URL))
            {
                // Create Directory
                Directory.CreateDirectory(FileDTO.URL);
            }

            // Save file on new folder
            File.WriteAllBytes(String.Format("{0}\\{1}", FileDTO.URL, FileDTO.Name), _convertImageBytes);
            _ValidationResultDTO.Data = FileDTO.URL;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to upload the file";
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO FileSize_Validation(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            //Validate file size Limit (5MB)
            if (Convert.ToInt32(FileDTO.Size) > (int)File_Enum.MB_5)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Error loading file",
                    Description = String.Format("File {0} exceeds the limit (5 MB)", FileDTO.Name)
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "Errors!";
                _ValidationResultDTO.Description = "There is a list of errors exist.";
                _ValidationResultDTO.ValidationResultList = _validation_ResultList;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Errors!";
            _ValidationResultDTO.Description = string.Format("There was an error while trying to save the record. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO SaveFile_Global(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {
            // Validate file
            _ValidationResultDTO = FileSize_Validation(FileDTO);

            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = SaveFile(FileDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to Save the file";
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO SaveMultipleFiles_Global(FileDTO FileDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been successfully saved"
        };
        try
        {

            // Save Files
            foreach (var _fileDTO in FileDTO.FileList)
            {
                _fileDTO.FileDirectory = FileDTO.FileDirectory;
                _fileDTO.ID = FileDTO.ID;
                _ValidationResultDTO = SaveFile_Global(_fileDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error";
            _ValidationResultDTO.Description = "There was an error trying to Save the file";
        }
        return _ValidationResultDTO;
    }
}
