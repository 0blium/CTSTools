using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.DAL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLog_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateChangeLog_Global(ChangeLogDTO ChangeLogDTO)
    {
        var _ValidationResultDTO = ChangeLog_Validator.CreateChangeLog_Validation(ChangeLogDTO);
        if (_ValidationResultDTO.Result)
        {
            ChangeLogDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = ChangeLog_Repository.CreateChangeLog(ChangeLogDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateChangeLog_Global(ChangeLogDTO ChangeLogDTO)
    {
        var _ValidationResultDTO = ChangeLog_Validator.UpdateChangeLog_Validation(ChangeLogDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = ChangeLog_Repository.UpdateChangeLog(ChangeLogDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteChangeLog_Global(ChangeLogDTO ChangeLogDTO)
    {
        var _ValidationResultDTO = ChangeLog_Validator.DeleteChangeLog_Validation(ChangeLogDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = ChangeLog_Repository.DeleteChangeLog(ChangeLogDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ChangeLogDTO> GetChangeLogList_Global(ChangeLogDTO ChangeLogDTO, PagedResultDTO<ChangeLogDTO> PagedResultDTO = null)
    {
        var _changelogglobalList = new List<ChangeLogDTO>();
        try
        {
            var _changelogList = ChangeLog_Repository.GetChangeLogList(ChangeLogDTO, PagedResultDTO);
            // if ChangeLog is empty, return list
            if (_changelogList.Count() == 0)
            {
                _changelogglobalList = _changelogList;
                return _changelogglobalList;
            }
            if (!ChangeLogDTO.GetUserDTO)
            {
                _changelogglobalList = _changelogList;
                return _changelogglobalList;
            }
            _changelogglobalList = GetChangeLogRelatedData(ChangeLogDTO, _changelogList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _changelogglobalList;
    }

    public static List<ChangeLogDTO> GetChangeLogRelatedData(ChangeLogDTO ChangeLogDTO, List<ChangeLogDTO> ChangeLogList)
    {
        var _changelogglobalList = new List<ChangeLogDTO>();
        var _userDict = new Dictionary<int?, UserDTO>();

        try
        {
            if (ChangeLogDTO.GetUserDTO)
            {
                ChangeLogDTO.UserDTO.UserIDArray = ChangeLogList.GroupBy(g => g.UserDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _userDict = User_Service.GetUserList_Global(ChangeLogDTO.UserDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _changelogDTO in ChangeLogList)
            {
                if (ChangeLogDTO.GetUserDTO && _userDict.ContainsKey(_changelogDTO.UserDTO.ID))
                {
                    _changelogDTO.UserDTO = _userDict[_changelogDTO.UserDTO.ID];
                }
                _changelogglobalList.Add(_changelogDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _changelogglobalList;
    }

    public static int GetChangeLogTotalCount(PagedResultDTO<ChangeLogDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = PagedResultDTO.dxFilters != null ? PagedResultDTO.DataList.Count : ChangeLog_Repository.GetChangeLogCount(PagedResultDTO.Filter);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion


    public static ValidationResultDTO BuildChangeLogActionCreate<T>(T NewObject, int UserID, int RecordID)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //1.- Get Type of Object
            Type _newObjectType = NewObject.GetType();

            //2.- Get properties list of Object
            PropertyInfo[] _props = _newObjectType.GetProperties();

            //4.- Get Sequence of change
            var _changeGroup = AssetManagementSQL.GetChangeGroupSequence();

            //5.- Exclude some properties
            _props = ExcludeProperties(_props);

            //6.- Save Properties.
            foreach (var _property in _props)
            {
                string _newValue = string.Empty;
                //7.- Set new value
                if (_property.PropertyType.IsClass && _property.PropertyType != typeof(string) && _property.Name.Contains("DTO"))
                {
                    _newValue = GetDTOPropertyValue(_property.GetValue(NewObject));
                }
                else
                {
                    _newValue = Convert.ToString(_property.GetValue(NewObject));
                }

                if (_newValue != string.Empty)
                {
                    var _changeLogDTO = new ChangeLogDTO();
                    _changeLogDTO.RecordID = RecordID;
                    _changeLogDTO.Table = NewObject.GetType().Name.Replace("DTO", "");
                    _changeLogDTO.Field = _property.Name;
                    _changeLogDTO.NewValue = _newValue;
                    _changeLogDTO.Action = nameof(Action_Enum.Create);
                    _changeLogDTO.UserDTO.ID = UserID;
                    _changeLogDTO.ChangeGroup = _changeGroup;

                    _validationResultDTO = CreateChangeLog_Global(_changeLogDTO);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha habido un error intentando guardar el registro.");
        }
        return _validationResultDTO;
    }

    public static ValidationResultDTO BuildChangeLogActionDelete<T>(T OldObject, int UserID, int RecordID)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //1.- Get Type of Object
            Type _oldObjectType = OldObject.GetType();

            //2.- Get properties list of Object
            PropertyInfo[] _props = _oldObjectType.GetProperties();

            //3.- Get Sequence of change
            var _changeGroup = AssetManagementSQL.GetChangeGroupSequence();

            //4.- Exclude some properties
            _props = ExcludeProperties(_props);

            //5.- Save Properties.
            foreach (var _property in _props)
            {

                string _oldValue = string.Empty;
                if (_property.PropertyType.IsClass && _property.PropertyType != typeof(string) && _property.Name.Contains("DTO"))
                {
                    _oldValue = GetDTOPropertyValue(_property.GetValue(OldObject));
                }
                else
                {
                    _oldValue = Convert.ToString(_property.GetValue(OldObject));
                }


                if (_oldValue != string.Empty)
                {
                    var _changeLogDTO = new ChangeLogDTO();
                    _changeLogDTO.RecordID = RecordID;
                    _changeLogDTO.Table = OldObject.GetType().Name.Replace("DTO", "");
                    _changeLogDTO.Field = _property.Name;
                    _changeLogDTO.OldValue = _oldValue;
                    _changeLogDTO.Action = nameof(Action_Enum.Delete);
                    _changeLogDTO.UserDTO.ID = UserID;
                    _changeLogDTO.ChangeGroup = _changeGroup;

                    _validationResultDTO = CreateChangeLog_Global(_changeLogDTO);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha habido un error intentando guardar el registro.");
        }
        return _validationResultDTO;
    }



    public static ValidationResultDTO BuildChangeLogActionUpdate<T>(T OldObject, T NewObject, int UserID, int RecordID)
    {
        var _validationResultDTO = new ValidationResultDTO();
        //Dictionary to save field name as key , and NewValue & OldValue as values
        var _propsDict = new Dictionary<string, (string NewValue, string OldValue)>();
        try
        {
            //1.- Get Type of Old Object
            Type _oldObjectType = OldObject.GetType();

            //2.- Get properties list of Old Object
            PropertyInfo[] _oldProps = _oldObjectType.GetProperties();

            //3.- Get Type of Old Object
            Type _newObjectType = NewObject.GetType();

            //4.- Get properties list of Old Object
            PropertyInfo[] _newProps = _oldObjectType.GetProperties();

            //5.- Get Sequence of change
            var _changeGroup = AssetManagementSQL.GetChangeGroupSequence();


            //6.- Exclude some properties
            _oldProps = ExcludeProperties(_oldProps);

            _newProps = ExcludeProperties(_newProps);

            //7.- Get Properties values.
            foreach (var _newProperty in _newProps)
            {
                foreach (var _oldProperty in _oldProps)
                {
                    if (_oldProperty.Name == _newProperty.Name && _oldProperty != null)
                    {

                        if (_newProperty.PropertyType.IsClass && _newProperty.PropertyType != typeof(string) && _newProperty.Name.Contains("DTO"))
                        {
                            _propsDict.Add(_oldProperty.Name, (GetDTOPropertyValue(_newProperty.GetValue(NewObject)), GetDTOPropertyValue(_oldProperty.GetValue(OldObject))));
                        }
                        else
                        {
                            //Key: Property Name , Values: (New Value, Old Value)
                            _propsDict.Add(_oldProperty.Name, (Convert.ToString(_newProperty.GetValue(NewObject)), Convert.ToString(_oldProperty.GetValue(OldObject))));
                        }

                        break;
                    }
                }
            }


            //8.-Save Change Log
            foreach (var _property in _propsDict)
            {
                if (_property.Value.NewValue != string.Empty)
                {
                    if (_property.Value.NewValue != _property.Value.OldValue)
                    {
                        var _changeLogDTO = new ChangeLogDTO();
                        _changeLogDTO.RecordID = RecordID;
                        _changeLogDTO.Table = OldObject.GetType().Name.Replace("DTO", "");
                        _changeLogDTO.Field = _property.Key;
                        _changeLogDTO.NewValue = _property.Value.NewValue;
                        _changeLogDTO.OldValue = _property.Value.OldValue;
                        _changeLogDTO.Action = nameof(Action_Enum.Update);
                        _changeLogDTO.UserDTO.ID = UserID;
                        _changeLogDTO.ChangeGroup = _changeGroup;

                        _validationResultDTO = CreateChangeLog_Global(_changeLogDTO);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha habido un error intentando guardar el registro.");
        }
        return _validationResultDTO;
    }

    //Function to get information from a DTO as property
    private static string GetDTOPropertyValue(Object DTOPropertyObject)
    {
        //Get DTO propierties
        PropertyInfo[] _dtoProps = DTOPropertyObject?.GetType().GetProperties();

        //Find ID Property
        PropertyInfo _dtoIDProp = Array.Find(_dtoProps, p => p.Name == "ID");

        //Get ID value
        var _newDTOID = _dtoIDProp != null ? Convert.ToString(_dtoIDProp.GetValue(DTOPropertyObject)) : "";

        return _newDTOID;
    }

    private static PropertyInfo[] ExcludeProperties(PropertyInfo[] PropertiesInfoArray)
    {
        string[] _excluededProperties = { nameof(UserDTO.AddedByID), nameof(UserDTO.AddedDate), nameof(UserDTO.LastUpdate), nameof(UserDTO.LastUpdateByID), nameof(FileDTO) };

        return PropertiesInfoArray.Where(PI => !_excluededProperties.Contains(PI.Name)).Where(PI => !PI.Name.Contains("Get") && !PI.Name.Contains("IDArray") && !PI.Name.Contains("List")).ToArray();
    }
}
