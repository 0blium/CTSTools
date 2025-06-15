using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.Class;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClass_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateSubClass_Global(SubClassDTO SubClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = SubClass_Validator.CreateSubClass_Validation(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ClassDTO and create value
            var _subClassDTO = new AttributeManagement.SubClass.SubClassDTO
            {
                SubClassValueDTO = new ValueDTO
                {
                    Name = SubClassDTO.Name,
                    Code = SubClassDTO.Code,
                    Description = SubClassDTO.Description,
                    AttributeID = (int)Attribute_Enum.SubClass,
                    AddedByID = SubClassDTO.AddedByID,
                    IsActive = SubClassDTO.IsActive,
                },
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = Class_Repository.GetClassByID((int)SubClassDTO.ClassID).ValueID,
                ChildAttributeID = (int)Attribute_Enum.SubClass,
                AddedByID = SubClassDTO.AddedByID,
                IsActive = true
            };
            
            _validationResultDTO = AttributeManagement.SubClass.SubClass_Service.CreateSubClass_Global(_subClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. create subclass
            SubClassDTO.AddedDate = DateTime.Now;
            SubClassDTO.AttributeID = (int)Attribute_Enum.SubClass;
            SubClassDTO.ValueID = _subClassDTO.ChildValueID;
            _validationResultDTO = SubClass_Repository.CreateSubClass(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateSubClass_Global(SubClassDTO SubClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = SubClass_Validator.UpdateSubClass_Validation(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and update value
            var _valueDTO = new ValueDTO
            {
                ID = SubClassDTO.ValueID,
                Name = SubClassDTO.Name,
                Code = SubClassDTO.Code,
                Description = SubClassDTO.Description,
                AttributeID = (int)Attribute_Enum.SubClass,
                LastUpdateByID = SubClassDTO.LastUpdateByID,
                IsActive = SubClassDTO.IsActive,
            };
            _validationResultDTO = Value_Service.UpdateValue_Global(_valueDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. update component type
            SubClassDTO.LastUpdate = DateTime.Now;
            SubClassDTO.AttributeID = (int)Attribute_Enum.SubClass;
            _validationResultDTO = SubClass_Repository.CreateSubClass(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteSubClass_Global(SubClassDTO SubClassDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = SubClass_Validator.DeleteSubClass_Validation(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. Delete ValueDTO
            var _valueDTO = new ValueDTO { ID = SubClassDTO.ValueID, IsActive = SubClassDTO.IsActive };
            _validationResultDTO = Value_Service.DeleteValue_Global(_valueDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. Delete part type
            _validationResultDTO = SubClass_Repository.DeleteSubClass(SubClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static List<SubClassDTO> GetSubClassList_Global(SubClassDTO SubClassDTO, PagedResultDTO<SubClassDTO> PagedResultDTO = null)
        {
            var _SubClassglobalList = new List<SubClassDTO>();
            try
            {
                var _SubClassList = SubClass_Repository.GetSubClassList(SubClassDTO, PagedResultDTO);
                // if SubClass is empty, return list
                if (_SubClassList.Count() == 0)
                {
                    _SubClassglobalList = _SubClassList;
                    return _SubClassglobalList;
                }
                if (!SubClassDTO.GetClassDTO)
                {
                    _SubClassglobalList = _SubClassList;
                    return _SubClassglobalList;
                }
                _SubClassglobalList = GetSubClassRelatedData(SubClassDTO, _SubClassList);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _SubClassglobalList;
        }
        public static List<SubClassDTO> GetSubClassRelatedData(SubClassDTO SubClassDTO, List<SubClassDTO> SubClassList)
        {
            var _SubClassglobalList = new List<SubClassDTO>();
            var _ClassDict = new Dictionary<int?, ClassDTO>();
            try
            {
                if (SubClassDTO.GetClassDTO)
                {
                    SubClassDTO.ClassDTO.ClassIDArray = SubClassList.GroupBy(g => g.ClassID)
                            .Select(s => s.Key)
                            .ToArray();

                    _ClassDict = Class_Service.GetClassList_Global(SubClassDTO.ClassDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _SubClassDTO in SubClassList)
                {
                    if (SubClassDTO.GetClassDTO && _ClassDict.ContainsKey(_SubClassDTO.ClassID))
                    {
                        _SubClassDTO.ClassDTO = _ClassDict[_SubClassDTO.ClassID];
                    }
                    _SubClassglobalList.Add(_SubClassDTO);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _SubClassglobalList;
        }
        public static int GetSubClassTotalCount(PagedResultDTO<SubClassDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = SubClass_Repository.GetSubClassCount(PagedResultDTO.Filter, PagedResultDTO);
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
