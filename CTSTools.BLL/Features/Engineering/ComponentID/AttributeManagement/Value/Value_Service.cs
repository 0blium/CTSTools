using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
public class Value_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateValue_Global(ValueDTO ValueDTO)
    {
        // Step 1. 
        var _validationResultDTO = Value_Validator.CreateValue_Validation(ValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2.
        ValueDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Value_Repository.CreateValue(ValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateValue_Global(ValueDTO ValueDTO)
    {
        var _ValidationResultDTO = Value_Validator.UpdateValue_Validation(ValueDTO);
        if (_ValidationResultDTO.Result)
        {
            ValueDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Value_Repository.UpdateValue(ValueDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteValue_Global(ValueDTO ValueDTO)
    {
        var _validationResultDTO = Value_Validator.DeleteValue_Validation(ValueDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Repository.DeleteValue(ValueDTO);
        }
        return _validationResultDTO;
    }
    public static List<ValueDTO> GetValueList_Global(ValueDTO ValueDTO, PagedResultDTO<ValueDTO> PagedResultDTO = null)
    {
        var _valueglobalList = new List<ValueDTO>();
        try
        {
            var _valueList = Value_Repository.GetValueList(ValueDTO, PagedResultDTO);
            // if Value is empty, return list
            if (_valueList.Count() == 0)
            {
                _valueglobalList = _valueList;
                return _valueglobalList;
            }
            if (!ValueDTO.GetAttributeDTO)
            {
                _valueglobalList = _valueList;
                return _valueglobalList;
            }
            _valueglobalList = GetValueRelatedData(ValueDTO, _valueList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valueglobalList;
    }
    public static List<ValueDTO> GetValueRelatedData(ValueDTO ValueDTO, List<ValueDTO> ValueList)
    {
        var _valueglobalList = new List<ValueDTO>();
        var _attributeDict = new Dictionary<int?, AttributeDTO>();

        try
        {
            if (ValueDTO.GetAttributeDTO)
            {
                ValueDTO.AttributeDTO = new AttributeDTO();
                ValueDTO.AttributeDTO.AttributeIDArray = ValueList.GroupBy(g => g.AttributeID)
                        .Select(s => s.Key)
                        .ToArray();

                _attributeDict = Attribute_Service.GetAttributeList_Global(ValueDTO.AttributeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _valueDTO in ValueList)
            {
                if (ValueDTO.GetAttributeDTO && _attributeDict.ContainsKey(_valueDTO.AttributeID))
                {
                    _valueDTO.AttributeDTO = _attributeDict[_valueDTO.AttributeID];
                }
                _valueglobalList.Add(_valueDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valueglobalList;
    }
    public static int GetValueTotalCount(PagedResultDTO<ValueDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Value_Repository.GetValueCount(PagedResultDTO.Filter, PagedResultDTO);
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
