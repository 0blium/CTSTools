using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;

public class Attribute_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate fields
        var _validationResultDTO = Attribute_Validator.CreateAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Save records
        AttributeDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Attribute_Repository.CreateAttribute(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Create the variant record
        var _valueDTO = new ValueDTO
        {
            Name = AttributeDTO.Name,
            AttributeID = (int)Attribute_Enum.Variant,
            Code = Convert.ToString(_validationResultDTO.Data),
            IsActive = AttributeDTO.IsActive,
            AddedByID = AttributeDTO.AddedByID,
            AddedDate = AttributeDTO.AddedDate
        };
        _validationResultDTO = Value_Service.CreateValue_Global(_valueDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate the attribute
        var _validationResultDTO = Attribute_Validator.UpdateAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Update the attribute
        AttributeDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Attribute_Repository.UpdateAttribute(AttributeDTO);
        //Step 3. Update Variant 
        var _valueDTO = new ValueDTO { AttributeID = (int)Attribute_Enum.Variant, Code = AttributeDTO.ID.ToString() };
        var _variantValueDTO = Value_Service.GetValueList_Global(_valueDTO).FirstOrDefault();
        if (_variantValueDTO != null)
        {
            _variantValueDTO.Name = AttributeDTO.Name;
            _validationResultDTO = Value_Service.UpdateValue_Global(_variantValueDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate fields
        var _validationResultDTO = Attribute_Validator.DeleteAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Attribute
        _validationResultDTO = Attribute_Repository.DeleteAttribute(AttributeDTO);
        return _validationResultDTO;
    }
    public static List<AttributeDTO> GetAttributeList_Global(AttributeDTO AttributeDTO, PagedResultDTO<AttributeDTO> PagedResultDTO = null)
    {
        var _attributeglobalList = new List<AttributeDTO>();
        try
        {
            var _attributeList = Attribute_Repository.GetAttributeList(AttributeDTO, PagedResultDTO);
            if (_attributeList.Count() == 0 || (!AttributeDTO.GetValueList))
                return _attributeList;
            else
                _attributeglobalList = GetAttributeRelatedData(AttributeDTO, _attributeList);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _attributeglobalList;
    }
    public static List<AttributeDTO> GetAttributeRelatedData(AttributeDTO AttributeDTO, List<AttributeDTO> AttributeList)
    {
        var _attributeglobalList = new List<AttributeDTO>();
        var _valueList = new List<ValueDTO>();
        try
        {
            if ((bool)AttributeDTO.GetValueList)
            {
                AttributeDTO.ValueDTO.AttributeIDArray = AttributeList.GroupBy(g => g.ID)
                        .Select(s => s.Key)
                        .ToArray();
                _valueList = Value_Service.GetValueList_Global(AttributeDTO.ValueDTO);
            }
            foreach (var _attributeDTO in AttributeList)
            {
                if ((bool)AttributeDTO.GetValueList && _valueList.Count() > 0)
                    _attributeDTO.ValueList = _valueList.Where(w => w.AttributeID == _attributeDTO.ID).ToList();
                _attributeglobalList.Add(_attributeDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _attributeglobalList;
    }

    public static int GetAttributeTotalCount(PagedResultDTO<AttributeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Attribute_Repository.GetAttributeCount(PagedResultDTO.Filter, PagedResultDTO);
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
