using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.ComponentType;
using CTSTools.BLL.Features.Engineering.ComponentID.PartType;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.Class
{
    public class Class_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateClass_Global(ClassDTO ClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = Class_Validator.CreateClass_Validation(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ClassDTO and create value
            var _classDTO = new AttributeManagement.Class.ClassDTO
            {
                ClassValueDTO = new ValueDTO
                {
                    Name = ClassDTO.Name,
                    Code = ClassDTO.Code,
                    Description = ClassDTO.Description,
                    IsActive = ClassDTO.IsActive,
                    AttributeID = (int)Attribute_Enum.Class
                },
                ParentAttributeID = ClassDTO.ComponentTypeID == null ? (int)Attribute_Enum.PartType : (int)Attribute_Enum.ComponentType,
                ParentValueID = ClassDTO.ComponentTypeID == null ? PartType_Repository.GetPartTypeByID((int)ClassDTO.PartTypeID).ValueID : ComponentType_Repository.GetComponentTypeByID((int)ClassDTO.ComponentTypeID).ValueID,
                ChildAttributeID = (int)Attribute_Enum.Class,
                AddedByID = ClassDTO.AddedByID,
                IsActive = ClassDTO.IsActive
            };

            _validationResultDTO = AttributeManagement.Class.Class_Service.CreateClass_Global(_classDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            _classDTO = _validationResultDTO.Data;
            // Step 3. create component type
            ClassDTO.AddedDate = DateTime.Now;
            ClassDTO.AttributeID = (int)Attribute_Enum.Class;
            ClassDTO.ValueID = _classDTO.ChildValueID;
            ClassDTO.ValueLinkID = _classDTO.ID;
            _validationResultDTO = Class_Repository.CreateClass(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateClass_Global(ClassDTO ClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = Class_Validator.UpdateClass_Validation(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and update value
            var _value = new ValueDTO
            {
                ID = ClassDTO.ValueID,
                Name = ClassDTO.Name,
                Code = ClassDTO.Code,
                Description = ClassDTO.Description,
                AttributeID = (int)Attribute_Enum.Class,
                LastUpdateByID = ClassDTO.LastUpdateByID,
                IsActive = ClassDTO.IsActive,
            };
            _validationResultDTO = Value_Service.UpdateValue_Global(_value);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. update component type
            ClassDTO.LastUpdate = DateTime.Now;
            ClassDTO.AttributeID = (int)Attribute_Enum.Class;
            _validationResultDTO = Class_Repository.CreateClass(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteClass_Global(ClassDTO ClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = Class_Validator.DeleteClass_Validation(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. Delete ValueDTO
            var _value = new ValueDTO { ID = ClassDTO.ValueID, IsActive = ClassDTO.IsActive };
            _validationResultDTO = Value_Service.DeleteValue_Global(_value);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. Delete part type
            _validationResultDTO = Class_Repository.DeleteClass(ClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static List<ClassDTO> GetClassList_Global(ClassDTO ClassDTO, PagedResultDTO<ClassDTO> PagedResultDTO = null)
        {
            var _ClassglobalList = new List<ClassDTO>();
            try
            {
                var _ClassList = Class_Repository.GetClassList(ClassDTO, PagedResultDTO);
                // if Class is empty, return list
                if (_ClassList.Count() == 0)
                {
                    _ClassglobalList = _ClassList;
                    return _ClassglobalList;
                }
                if (!ClassDTO.GetAttributeDTO && !ClassDTO.GetValueDTO && !ClassDTO.GetValueLinkDTO && !ClassDTO.GetPartTypeDTO && !ClassDTO.GetComponentTypeDTO)
                {
                    _ClassglobalList = _ClassList;
                    return _ClassglobalList;
                }
                _ClassglobalList = GetClassRelatedData(ClassDTO, _ClassList);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _ClassglobalList;
        }
        public static List<ClassDTO> GetClassRelatedData(ClassDTO ClassDTO, List<ClassDTO> ClassList)
        {
            var _classglobalList = new List<ClassDTO>();
            var _attributeDict = new Dictionary<int?, AttributeDTO>();
            var _valueDict = new Dictionary<int?, ValueDTO>();
            var _valuelinkDict = new Dictionary<int?, ValueLinkDTO>();
            var _parttypeDict = new Dictionary<int?, PartTypeDTO>();
            var _componenttypeDict = new Dictionary<int?, ComponentTypeDTO>();

            try
            {
                if (ClassDTO.GetAttributeDTO)
                {
                    ClassDTO.AttributeDTO.AttributeIDArray = ClassList.GroupBy(g => g.AttributeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _attributeDict = Attribute_Service.GetAttributeList_Global(ClassDTO.AttributeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (ClassDTO.GetValueDTO)
                {
                    ClassDTO.ValueDTO.ValueIDArray = ClassList.GroupBy(g => g.ValueID)
                            .Select(s => s.Key)
                            .ToArray();

                    _valueDict = Value_Service.GetValueList_Global(ClassDTO.ValueDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (ClassDTO.GetValueLinkDTO)
                {
                    ClassDTO.ValueLinkDTO.ValueLinkIDArray = ClassList.GroupBy(g => g.ValueLinkID)
                            .Select(s => s.Key)
                            .ToArray();

                    _valuelinkDict = ValueLink_Service.GetValueLinkList_Global(ClassDTO.ValueLinkDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (ClassDTO.GetPartTypeDTO)
                {
                    ClassDTO.PartTypeDTO.PartTypeIDArray = ClassList.GroupBy(g => g.PartTypeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _parttypeDict = PartType_Service.GetPartTypeList_Global(ClassDTO.PartTypeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (ClassDTO.GetComponentTypeDTO)
                {
                    ClassDTO.ComponentTypeDTO.ComponentTypeIDArray = ClassList.GroupBy(g => g.ComponentTypeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _componenttypeDict = ComponentType_Service.GetComponentTypeList_Global(ClassDTO.ComponentTypeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _classDTO in ClassList)
                {
                    if (ClassDTO.GetAttributeDTO && _attributeDict.ContainsKey(_classDTO.AttributeID))
                    {
                        _classDTO.AttributeDTO = _attributeDict[_classDTO.AttributeID];
                    }
                    if (ClassDTO.GetValueDTO && _valueDict.ContainsKey(_classDTO.ValueID))
                    {
                        _classDTO.ValueDTO = _valueDict[_classDTO.ValueID];
                    }
                    if (ClassDTO.GetValueLinkDTO && _valuelinkDict.ContainsKey(_classDTO.ValueLinkID))
                    {
                        _classDTO.ValueLinkDTO = _valuelinkDict[_classDTO.ValueLinkID];
                    }
                    if (ClassDTO.GetPartTypeDTO && _parttypeDict.ContainsKey(_classDTO.PartTypeID))
                    {
                        _classDTO.PartTypeDTO = _parttypeDict[_classDTO.PartTypeID];
                    }
                    if (ClassDTO.GetComponentTypeDTO && _componenttypeDict.ContainsKey(_classDTO.ComponentTypeID))
                    {
                        _classDTO.ComponentTypeDTO = _componenttypeDict[_classDTO.ComponentTypeID];
                    }
                    _classglobalList.Add(_classDTO);
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _classglobalList;
        }
        public static int GetClassTotalCount(PagedResultDTO<ClassDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = Class_Repository.GetClassCount(PagedResultDTO.Filter, PagedResultDTO);
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
