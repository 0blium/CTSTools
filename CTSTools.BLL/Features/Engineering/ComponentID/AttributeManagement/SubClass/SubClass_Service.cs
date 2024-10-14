using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;
public class SubClass_Service
{
    public static List<SubClassDTO> GetSubClassList_Global(SubClassDTO ClassDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _subClassGlobalList = new List<SubClassDTO>();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeIDArray = [(int)Attribute_Enum.PartType, (int)Attribute_Enum.ComponentType, (int)Attribute_Enum.Class],
                ChildAttributeIDArray = [(int)Attribute_Enum.ComponentType, (int)Attribute_Enum.SubClass, (int)Attribute_Enum.Class],
                GetChildAttributeDTO = true,
                GetParentValueDTO = true,
                GetChildValueDTO = true,
                GetParentAttributeDTO = true,
                ParentValueDTO = { GetAttributeDTO = true },
                ChildValueDTO = { GetAttributeDTO = true }
            };
            var _valuelinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _classList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.ComponentType ||
                                                       w.ParentAttributeID == (int)Attribute_Enum.PartType &&
                                                       w.ChildAttributeID == (int)Attribute_Enum.Class)
                                           .ToList();

            var _subClassesList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.Class &&
                                                            w.ChildAttributeID == (int)Attribute_Enum.SubClass)
                                                .ToList();
            var _componentTypeFromPartTypeList = _valuelinkList.Where(w => w.ParentAttributeID == (int)Attribute_Enum.PartType &&
                                                            w.ChildAttributeID == (int)Attribute_Enum.ComponentType)
                                                .ToList();
            if (_subClassesList.Count() > 0)
            {
                foreach (var _subClassesDTO in _subClassesList)
                {
                    var _subClassDTO = new SubClassDTO();
                    var _classValueLinkDTO = new ValueLinkDTO();
                    _subClassDTO.ID = _subClassesDTO.ID;
                    _subClassDTO.SubClassValueDTO = _subClassesDTO.ChildValueDTO;

                    if (_classList.Count() > 0)
                    {
                        _classValueLinkDTO = _classList.Where(w => w.ChildValueID == _subClassesDTO.ParentValueID)
                                                       .FirstOrDefault();
                        _subClassDTO.ClassDTO = _classValueLinkDTO.ChildValueDTO;
                    }


                    _subClassDTO.ComponentTypeDTO = _classValueLinkDTO.ParentAttributeID == (int)Attribute_Enum.ComponentType ?
                                                    _classValueLinkDTO.ParentValueDTO : null;


                    _subClassDTO.PartTypeDTO = _componentTypeFromPartTypeList.Count() > 0 &&
                                               _classValueLinkDTO.ParentAttributeID == (int)Attribute_Enum.ComponentType ?
                                               _componentTypeFromPartTypeList.Where(w => w.ChildValueID == _classValueLinkDTO.ParentValueID)
                                                                             .FirstOrDefault().ParentValueDTO :
                                               _classValueLinkDTO.ParentValueDTO;

                    _subClassGlobalList.Add(_subClassDTO);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _subClassGlobalList;
    }
    public static ValidationResultDTO CreateSubClass_Global(SubClassDTO SubClassDTO)
    {
        SubClassDTO.SubClassValueDTO.AddedByID = SubClassDTO.AddedByID;
        SubClassDTO.SubClassValueDTO.AddedDate = DateTime.Now;
        //Step 1. Validate fields
        var _validationResultDTO = SubClass_Validator.CreateClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Create the attribute value
        _validationResultDTO = Value_Service.CreateValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Validate Value Link
        SubClassDTO.ChildValueID = _validationResultDTO.Data;
        _validationResultDTO = ValueLink_Validator.CreateValueLink_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 4. Create Value Link 
        _validationResultDTO = ValueLink_Service.CreateValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateSubClass_Global(SubClassDTO SubClassDTO)
    {
        //Step 1.Validate fields
        var _validationResultDTO = SubClass_Validator.UpdateSubClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 2. Update Atttribute Value
        SubClassDTO.SubClassValueDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Value_Service.UpdateValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 3. Update Attribute Value 
        SubClassDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = ValueLink_Service.UpdateValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteSubClass_Global(SubClassDTO SubClassDTO)
    {
        //Step 1.Validate fields
        var _validationResultDTO = SubClass_Validator.DeleteSubClassFields_Validation(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 1. Delete Value Link 
        _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(SubClassDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Value 
        _validationResultDTO = Value_Service.DeleteValue_Global(SubClassDTO.SubClassValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }

}
