using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

public class ValueLink_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateValueLink_Global(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = ValueLink_Validator.CreateValueLink_Validation(ValueLinkDTO);
        if (_validationResultDTO.Result)
        {
            ValueLinkDTO.AddedDate = DateTime.Now;
            _validationResultDTO = ValueLink_Repository.CreateValueLink(ValueLinkDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateValueLink_Global(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = ValueLink_Validator.UpdateValueLink_Validation(ValueLinkDTO);
        if (_validationResultDTO.Result)
        {
            ValueLinkDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = ValueLink_Repository.UpdateValueLink(ValueLinkDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteValueLink_Global(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = ValueLink_Validator.DeleteValueLink_Validation(ValueLinkDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = ValueLink_Repository.DeleteValueLink(ValueLinkDTO);
        }
        return _validationResultDTO;
    }
    public static List<ValueLinkDTO> GetValueLinkList_Global(ValueLinkDTO ValueLinkDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _valuelinkGlobalList = new List<ValueLinkDTO>();
        try
        {
            var _valuelinkList = ValueLink_Repository.GetValueLinkList(ValueLinkDTO, PagedResultDTO);

            // if Value is empty, return list 
            if (_valuelinkList.Count() == 0 || (!ValueLinkDTO.GetParentAttributeDTO && !ValueLinkDTO.GetChildAttributeDTO && !ValueLinkDTO.GetParentValueDTO && !ValueLinkDTO.GetChildValueDTO))
            {
                _valuelinkGlobalList = _valuelinkList;
                return _valuelinkGlobalList;
            }
            else
            {
                _valuelinkGlobalList = GetValueLinkRelatedData(ValueLinkDTO, _valuelinkList);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valuelinkGlobalList;
    }
    internal static List<ValueLinkDTO> GetValueLinkRelatedData(ValueLinkDTO ValueLinkDTO, List<ValueLinkDTO> ValueLinkList)
    {
        var _valuelinkglobalList = new List<ValueLinkDTO>();
        var _parentAttributeDict = new Dictionary<int?, AttributeDTO>();
        var _parentValueDict = new Dictionary<int?, ValueDTO>();
        var _childValueDict = new Dictionary<int?, ValueDTO>();
        var _childAttributeDict = new Dictionary<int?, AttributeDTO>();

        try
        {
            if (ValueLinkDTO.GetParentAttributeDTO)
            {
                ValueLinkDTO.ParentAttributeDTO.AttributeIDArray = ValueLinkList.GroupBy(g => g.ParentAttributeID)
                        .Select(s => s.Key)
                        .ToArray();

                _parentAttributeDict = Attribute_Service.GetAttributeList_Global(ValueLinkDTO.ParentAttributeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (ValueLinkDTO.GetParentValueDTO)
            {
                ValueLinkDTO.ParentValueDTO.ValueIDArray = ValueLinkList.GroupBy(g => g.ParentValueID)
                        .Select(s => s.Key)
                        .ToArray();

                _parentValueDict = Value_Service.GetValueList_Global(ValueLinkDTO.ParentValueDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (ValueLinkDTO.GetChildAttributeDTO)
            {
                ValueLinkDTO.ChildAttributeDTO.AttributeIDArray = ValueLinkList.GroupBy(g => g.ChildAttributeID)
                        .Select(s => s.Key)
                        .ToArray();

                _childAttributeDict = Attribute_Service.GetAttributeList_Global(ValueLinkDTO.ChildAttributeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (ValueLinkDTO.GetChildValueDTO)
            {
                ValueLinkDTO.ChildValueDTO.ValueIDArray = ValueLinkList.GroupBy(g => g.ChildValueID)
                        .Select(s => s.Key)
                        .ToArray();

                _childValueDict = Value_Service.GetValueList_Global(ValueLinkDTO.ChildValueDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }

            foreach (var _valueLinkDTO in ValueLinkList)
            {
                if (ValueLinkDTO.GetParentAttributeDTO && _parentAttributeDict.ContainsKey(_valueLinkDTO.ParentAttributeID))
                {
                    _valueLinkDTO.ParentAttributeDTO = _parentAttributeDict[_valueLinkDTO.ParentAttributeID];
                }
                if (ValueLinkDTO.GetParentValueDTO && _parentValueDict.ContainsKey(_valueLinkDTO.ParentValueID))
                {
                    _valueLinkDTO.ParentValueDTO = _parentValueDict[_valueLinkDTO.ParentValueID];
                }
                if (ValueLinkDTO.GetChildValueDTO && _childValueDict.ContainsKey(_valueLinkDTO.ChildValueID))
                {
                    _valueLinkDTO.ChildValueDTO = _childValueDict[_valueLinkDTO.ChildValueID];
                }
                if (ValueLinkDTO.GetChildAttributeDTO && _childAttributeDict.ContainsKey(_valueLinkDTO.ChildAttributeID))
                {
                    _valueLinkDTO.ChildAttributeDTO = _childAttributeDict[_valueLinkDTO.ChildAttributeID];
                }
                _valuelinkglobalList.Add(_valueLinkDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valuelinkglobalList;
    }
    public static int GetValueLinkTotalCount(PagedResultDTO<ValueLinkDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = ValueLink_Repository.GetValueLinkCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreateMultipleValueLink(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultGlobalDTO = new ValidationResultDTO();
        try
        {
            foreach (var _childValueID in ValueLinkDTO.ChildValueIDArray)
            {
                ValueLinkDTO.ChildValueID = _childValueID;
                var _validationResultDTO = CreateValueLink_Global(ValueLinkDTO);
                if (_validationResultDTO.Result == false)
                {
                    _validationResultGlobalDTO.Result = false;
                    _validationResultGlobalDTO.ValidationResultList.AddRange(_validationResultDTO.ValidationResultList);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultGlobalDTO.Result = false;
            _validationResultGlobalDTO.Message = "Error";
            _validationResultGlobalDTO.Description = string.Format("There was an error trying to save the fields of Department_responsibles. {0}", ex.Message);
        }

        return _validationResultGlobalDTO;
    }
    public static ValidationResultDTO LinkBaseValuesToSubClass(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //Step 1. Link Part Type to Sub Class
            //var _partTypeDTO = new ValueLinkDTO
            //{
            //    ParentAttributeID = (int?)Attribute_Enum.SubClass,
            //    ParentValueID = DecoderDTO.SubClassID,
            //    ChildAttributeID = (int?)Attribute_Enum.PartType,
            //    ChildValueID = DecoderDTO.PartTypeID,
            //    IsActive = DecoderDTO.IsActive,
            //    AddedByID = DecoderDTO.AddedByID,
            //    AddedDate = DecoderDTO.AddedDate
            //};
            //_validationResultDTO = CreateValueLink_Global(_partTypeDTO);
            //if (!_validationResultDTO.Result)
            //    return _validationResultDTO;

            //Step 2. Link Class to Sub Class
            var _classDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int?)Attribute_Enum.SubClass,
                ParentValueID = DecoderDTO.SubClassID,
                ChildAttributeID = (int?)Attribute_Enum.Class,
                ChildValueID = DecoderDTO.ClassID,
                IsActive = DecoderDTO.IsActive,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate
            };
            _validationResultDTO = CreateValueLink_Global(_classDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            //Step 3. Link Sub Class to Sub Class
            var _subClassDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int?)Attribute_Enum.SubClass,
                ParentValueID = DecoderDTO.SubClassID,
                ChildAttributeID = (int?)Attribute_Enum.SubClass,
                ChildValueID = DecoderDTO.SubClassID,
                IsActive = DecoderDTO.IsActive,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate
            };
            _validationResultDTO = CreateValueLink_Global(_subClassDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            //Step 4. Link Symbol (-) to Sub Class
            //var _symbol1DTO = new ValueLinkDTO
            //{
            //    ParentAttributeID = (int?)Attribute_Enum.SubClass,
            //    ParentValueID = DecoderDTO.SubClassID,
            //    ChildAttributeID = (int?)Attribute_Enum.Symbol,
            //    ChildValueID = (int)Value_Enum.Symbol_Enum.Dash,
            //    IsActive = DecoderDTO.IsActive,
            //    AddedByID = DecoderDTO.AddedByID,
            //    AddedDate = DecoderDTO.AddedDate
            //};
            //_validationResultDTO = CreateValueLink_Global(_symbol1DTO);
            //if (!_validationResultDTO.Result)
            //    return _validationResultDTO;
            //Step 4. Link Component to Sub Class
            if (DecoderDTO.PartTypeID == (int?)Value_Enum.PartTypeValue_Enum.Manufactured)
            {
                var _componentTypeDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int?)Attribute_Enum.SubClass,
                    ParentValueID = DecoderDTO.SubClassID,
                    ChildAttributeID = (int?)Attribute_Enum.ComponentType,
                    ChildValueID = DecoderDTO.ComponentTypeID,
                    IsActive = DecoderDTO.IsActive,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate
                };
                _validationResultDTO = CreateValueLink_Global(_componentTypeDTO);
                if (!_validationResultDTO.Result)
                    return _validationResultDTO;
                return _validationResultDTO;
            }
            //Step 5. Link Class Sequence to Sub Class
            var _value_LinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = DecoderDTO.ClassID,
                ChildAttributeID = (int)Attribute_Enum.Class_Sequence
            };
            var _classID = GetValueLinkList_Global(_value_LinkDTO).FirstOrDefault();
            if (_classID != null)
            {
                var _classIDDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int?)Attribute_Enum.SubClass,
                    ParentValueID = DecoderDTO.SubClassID,
                    ChildAttributeID = (int?)Attribute_Enum.Class_Sequence,
                    ChildValueID = _classID.ChildValueID,
                    IsActive = DecoderDTO.IsActive,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate
                };
                _validationResultDTO = CreateValueLink_Global(_classIDDTO);
            }
            //Step 4. Link Symbol (-) to Sub Class
            //var _symbol2DTO = new ValueLinkDTO
            //{
            //    ParentAttributeID = (int?)Attribute_Enum.SubClass,
            //    ParentValueID = DecoderDTO.SubClassID,
            //    ChildAttributeID = (int?)Attribute_Enum.Symbol,
            //    ChildValueID = (int)Value_Enum.Symbol_Enum.Dash,
            //    IsActive = DecoderDTO.IsActive,
            //    AddedByID = DecoderDTO.AddedByID,
            //    AddedDate = DecoderDTO.AddedDate
            //};
            //_validationResultDTO = CreateValueLink_Global(_symbol2DTO);
            //if (!_validationResultDTO.Result)
            //    return _validationResultDTO;
            //Step ??. Add Customer Consigment
            var _customerConsigmentDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int?)Attribute_Enum.SubClass,
                ParentValueID = DecoderDTO.SubClassID,
                ChildAttributeID = (int?)Attribute_Enum.Costumer_Consigment,
                ChildValueID = (int)Value_Enum.Customer_Consigment.Empty,
                IsActive = DecoderDTO.IsActive,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate
            };
            _validationResultDTO = CreateValueLink_Global(_customerConsigmentDTO);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    #endregion 

}
