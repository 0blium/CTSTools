using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class_Sequence;

public class Class_Sequence_Service
{
    public static ValidationResultDTO CreateClass_Sequence_Global(ClassDTO ClassDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        //Step 1. Create Class ID Value
        string _classIDSequence = Class_Sequence_SQL.CreateClass_Sequence(ClassDTO.ClassValueDTO.Name);
        if (_classIDSequence == "0")
        {
            _validationResultDTO = new ValidationResultDTO
            {
                Result = false,
                Message = "Class ID Error",
                Description = ""
            };
        }
        //Step 2. Create Class ID Value
        var _classIDValueDTO = new ValueDTO
        {
            Name = _classIDSequence,
            AttributeID = (int)Attribute_Enum.Class_Sequence,
            IsCounter = true,
            AddedByID = ClassDTO.ClassValueDTO.AddedByID,
            AddedDate = DateTime.Now,
            IsActive = true
        };
        _validationResultDTO = Value_Service.CreateValue_Global(_classIDValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Create Class ID Sequence

        var _classIDValueLinkDTO = new ValueLinkDTO
        {
            ChildAttributeID = (int)Attribute_Enum.Class_Sequence,
            ChildValueID = _validationResultDTO.Data,
            ParentValueID = ClassDTO.ChildValueID,
            ParentAttributeID = (int)Attribute_Enum.Class,
            AddedDate = DateTime.Now,
            AddedByID = ClassDTO.AddedByID,
            IsActive = true
        };
        _validationResultDTO = ValueLink_Service.CreateValueLink_Global(_classIDValueLinkDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMultiple_Sequence_Global(List<ClassDTO> ClassList)
    {
        var _validationResultDTO = new ValidationResultDTO();
        foreach (var ClassDTO in ClassList) 
        {
            _validationResultDTO = CreateClass_Sequence_Global(ClassDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO GetClass_Sequence_Global(string ClassName)
    {
        var _validationResultDTO = new ValidationResultDTO();
        //Step 1. Create Class ID Value
        int _classIDSequence = Convert.ToInt32(Class_Sequence_SQL.GetClass_Sequence(ClassName));
        if (_classIDSequence == 0)
        {
            _validationResultDTO = new ValidationResultDTO
            {
                Result = false,
                Message = "Class ID Error",
                Description = ""
            };
        }
        _validationResultDTO.Data = _classIDSequence.ToString("D4");
        return _validationResultDTO;
    }


}
