using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using Elmah;
using System;
using System.Configuration;
using System.DirectoryServices;
namespace CTSTools.BLL.Features.Users.ActiveDirectory
{
    public class ActiveDirectory_Helper
    {
        public static ActiveDirectoryDTO GetActiveDirectoryUser(UserDTO UserDTO)
        {
            var _activeDirectoryDTO = new ActiveDirectoryDTO { Login = UserDTO.Login };
            try
            {

                //Instantiate a new Directory with the SHVUS domain AD server
                var _directory = new DirectoryEntry();
                if (UserDTO.FacilityDTO.ID == (int)Facility_Enum.Canada)
                {
                    string _activeDirectoryIP = ConfigurationManager.AppSettings["CanadaActiveDirectoryIP"].ToString();
                    _directory = new DirectoryEntry($"LDAP://dc1.crownebs.com");
                    _directory.Username = "admin.cts";
                    _directory.Password = "#crn.F0nt4n4#\r\n ";
                }
                if (UserDTO.FacilityDTO.ID == (int)Facility_Enum.Garland)
                {
                    string _activeDirectoryIP = ConfigurationManager.AppSettings["GarlandActiveDirectoryIP"].ToString();
                    _directory = new DirectoryEntry("LDAP://Garland1");
                    _directory.Username = "admin.cts";
                    _directory.Password = "#crn.G4rl4nd#";
                }
                if (UserDTO.FacilityDTO.ID == (int)Facility_Enum.Fontana)
                {
                    string _activeDirectoryIP = ConfigurationManager.AppSettings["FontanaActiveDirectoryIP"].ToString();
                    _directory = new DirectoryEntry("LDAP://dc1.crownebs.com");
                    _directory.Username = "admin.cts";
                    _directory.Password = "#crn.F0nt4n4#";


                }
                //store the filter for the search
                string _filter = String.Format("(SAMAccountName={0})", _activeDirectoryDTO.Login.Split('\\')[1]);
                //instantiate a new directory searcher, and assign the directory to search
                using var _findUser = new DirectorySearcher(_directory);
                //Set the scope of the search
                using var _dsSearcher = new DirectorySearcher(_directory);
                _dsSearcher.Filter = _filter;
                SearchResult _result = _dsSearcher.FindOne();
                if (_result != null)
                {
                    var properties = _result?.Properties;
                    _activeDirectoryDTO.Fullname = properties?["name"][0].ToString();
                }
                else
                {
                    _activeDirectoryDTO.Validation_ResultDTO = new ValidationResultDTO
                    {
                        Result = false,
                        Message = "User account not found",
                        Description = "The record hasn't been found in the active directory."
                    };
                }
                //catch any exception that may occurr in the proccess
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _activeDirectoryDTO.Validation_ResultDTO = new ValidationResultDTO
                {
                    Result = false,
                    Message = "Error!",
                    Description = string.Format("There was an error to search the user on active directory. {0}", ex.Message)
                };
            }
            return _activeDirectoryDTO;
        }
    }
}
