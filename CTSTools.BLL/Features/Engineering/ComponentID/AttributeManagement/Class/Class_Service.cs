using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class_Sequence;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
public class Class_Service
{
    public static List<ClassDTO> GetClassList_Global(ClassDTO ClassDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _classGlobalList = new List<ClassDTO>();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeIDArray = [(int)Attribute_Enum.PartType, (int)Attribute_Enum.ComponentType],
                ChildAttributeID = (int)Attribute_Enum.Class,
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true,
            };
            var _valuelinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);


            var _componentTypeList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.ComponentType).ToList();
            var _componentTypeDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.PartType,
                ChildAttributeID = (int)Attribute_Enum.ComponentType,
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true

            };
            var _partTypeList = ValueLink_Service.GetValueLinkList_Global(_componentTypeDTO);


            foreach (var ComponentTypeDTO in _componentTypeList)
            {
                var _classDTO = new ClassDTO
                {
                    ID = ComponentTypeDTO.ID,
                    ComponentTypeDTO = ComponentTypeDTO.ParentValueDTO,
                    ClassValueDTO = ComponentTypeDTO.ChildValueDTO,

                };
                _classDTO.PartTypeDTO = _partTypeList.Where(w => w.ChildAttributeID == _classDTO.ComponentTypeDTO.AttributeID)
                                                     .ToList()
                                                     .FirstOrDefault()
                                                     .ParentValueDTO;
                _classGlobalList.Add(_classDTO);
            }
            var _partTypeValuesList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.PartType)
                                                    .ToList();
            foreach (var PartTypeDTO in _partTypeValuesList)
            {
                var _classDTO = new ClassDTO
                {
                    ID = PartTypeDTO.ID,
                    PartTypeDTO = PartTypeDTO.ParentValueDTO,
                    ClassValueDTO = PartTypeDTO.ChildValueDTO,
                };
                _classGlobalList.Add(_classDTO);
            }

            //if Value is empty, return list

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _classGlobalList;
    }

    public static ValidationResultDTO CreateClass_Global(ClassDTO ClassDTO)
    {
        ClassDTO.ClassValueDTO.AddedByID = ClassDTO.AddedByID;
        ClassDTO.ClassValueDTO.AddedDate = DateTime.Now;
        //Step 1. Validate fields
        var _validationResultDTO = Class_Validator.CreateClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Create the Class value
        _validationResultDTO = Value_Service.CreateValue_Global(ClassDTO.ClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Validate Value Link
        ClassDTO.ChildValueID = _validationResultDTO.Data;
        ClassDTO.ClassValueDTO.ID = _validationResultDTO.Data;
        _validationResultDTO = ValueLink_Validator.CreateValueLink_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            return _validationResultDTO;
        }
        //Step 4. Create Value Link 
        _validationResultDTO = ValueLink_Service.CreateValueLink_Global(ClassDTO);
        ClassDTO.ID = _validationResultDTO.Data;
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            return _validationResultDTO;
        }
        //Step 5. Create Class ID Value
        ClassDTO.ClassValueDTO.ID = ClassDTO.ChildValueID;
        _validationResultDTO = Class_Sequence_Service.CreateClass_Sequence_Global(ClassDTO);
        if (!_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
            _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ClassDTO);
            return _validationResultDTO;
        }


        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateClass_Global(ClassDTO ClassDTO)
    {

        //Step 1. Validate fields
        var _validationResultDTO = Class_Validator.UpdateClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step X. Update Atttribute Value
        ClassDTO.ClassValueDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Value_Service.UpdateValue_Global(ClassDTO.ClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step Y. Update Attribute Value 
        ClassDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = ValueLink_Service.UpdateValueLink_Global(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        return _validationResultDTO;
    }

    public static ValidationResultDTO DeleteClass_Global(ClassDTO ClassDTO)
    {
        //Step 1. Delete Value Link 
        var _validationResultDTO = Class_Validator.DeleteClassFields_Validation(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Value 
        _validationResultDTO = Value_Service.DeleteValue_Global(ClassDTO.ClassValueDTO);
        if (_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }

}
