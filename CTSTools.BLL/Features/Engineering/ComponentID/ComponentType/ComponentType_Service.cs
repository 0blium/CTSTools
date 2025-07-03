using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.PartType;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentType_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateComponentType_Global(ComponentTypeDTO ComponentTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = ComponentType_Validator.CreateComponentType_Validation(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and create value
            var _valueDTO = new ValueDTO
            {
                Name = ComponentTypeDTO.Name,
                Code = ComponentTypeDTO.Code,
                Description = ComponentTypeDTO.Description,
                AttributeID = (int)Attribute_Enum.ComponentType,
                AddedByID = ComponentTypeDTO.AddedByID,
                IsActive = ComponentTypeDTO.IsActive,
            };
            _validationResultDTO = Value_Service.CreateValue_Global(_valueDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. create component type
            ComponentTypeDTO.AddedDate = DateTime.Now;
            ComponentTypeDTO.AttributeID = (int)Attribute_Enum.ComponentType;
            ComponentTypeDTO.ValueID = _validationResultDTO.Data;
            _validationResultDTO = ComponentType_Repository.CreateComponentType(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateComponentType_Global(ComponentTypeDTO ComponentTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = ComponentType_Validator.UpdateComponentType_Validation(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and update value
            var _valueDTO = new ValueDTO
            {
                ID = ComponentTypeDTO.ValueID,
                Name = ComponentTypeDTO.Name,
                Code = ComponentTypeDTO.Code,
                Description = ComponentTypeDTO.Description,
                AttributeID = (int)Attribute_Enum.ComponentType,
                LastUpdateByID = ComponentTypeDTO.LastUpdateByID,
                IsActive = ComponentTypeDTO.IsActive,
            };
            _validationResultDTO = Value_Service.UpdateValue_Global(_valueDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. update component type
            ComponentTypeDTO.LastUpdate = DateTime.Now;
            ComponentTypeDTO.AttributeID = (int)Attribute_Enum.ComponentType;
            _validationResultDTO = ComponentType_Repository.CreateComponentType(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteComponentType_Global(ComponentTypeDTO ComponentTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = ComponentType_Validator.DeleteComponentType_Validation(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. Delete ValueDTO
            var _valueDTO = new ValueDTO { ID = ComponentTypeDTO.ValueID, IsActive = ComponentTypeDTO.IsActive };
            _validationResultDTO = Value_Service.DeleteValue_Global(_valueDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. Delete part type
            _validationResultDTO = ComponentType_Repository.DeleteComponentType(ComponentTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static List<ComponentTypeDTO> GetComponentTypeList_Global(ComponentTypeDTO ComponentTypeDTO, PagedResultDTO<ComponentTypeDTO> PagedResultDTO = null)
        {
            var _componentTypeglobalList = new List<ComponentTypeDTO>();
            try
            {
                var _componentTypeList = ComponentType_Repository.GetComponentTypeList(ComponentTypeDTO, PagedResultDTO);
                // if ComponentType is empty, return list
                if (_componentTypeList.Count() == 0)
                {
                    _componentTypeglobalList = _componentTypeList;
                    return _componentTypeglobalList;
                }
                if (!ComponentTypeDTO.GetPartTypeDTO)
                {
                    _componentTypeglobalList = _componentTypeList;
                    return _componentTypeglobalList;
                }
                _componentTypeglobalList = GetComponentTypeRelatedData(ComponentTypeDTO, _componentTypeList);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _componentTypeglobalList;
        }
        public static List<ComponentTypeDTO> GetComponentTypeRelatedData(ComponentTypeDTO ComponentTypeDTO, List<ComponentTypeDTO> ComponentTypeList)
        {
            var _componentTypeglobalList = new List<ComponentTypeDTO>();
            var _partTypeDict = new Dictionary<int?, PartTypeDTO>();
            try
            {
                if (ComponentTypeDTO.GetPartTypeDTO)
                {
                    ComponentTypeDTO.PartTypeDTO.PartTypeIDArray = ComponentTypeList.GroupBy(g => g.PartTypeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _partTypeDict = PartType_Service.GetPartTypeList_Global(ComponentTypeDTO.PartTypeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _ComponentTypeDTO in ComponentTypeList)
                {
                    if (ComponentTypeDTO.GetPartTypeDTO && _partTypeDict.ContainsKey(_ComponentTypeDTO.PartTypeID))
                    {
                        _ComponentTypeDTO.PartTypeDTO = _partTypeDict[_ComponentTypeDTO.PartTypeID];
                    }
                    _componentTypeglobalList.Add(_ComponentTypeDTO);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _componentTypeglobalList;
        }
        public static int GetComponentTypeTotalCount(PagedResultDTO<ComponentTypeDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = ComponentType_Repository.GetComponentTypeCount(PagedResultDTO.Filter, PagedResultDTO);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        // Aqui va la logica 

        #endregion
    }
}
