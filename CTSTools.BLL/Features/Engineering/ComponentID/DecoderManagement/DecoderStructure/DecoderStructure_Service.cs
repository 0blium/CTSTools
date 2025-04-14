using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using Elmah;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Activation;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
public class DecoderStructure_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDecoderStructure_Global(DecoderStructureDTO DecoderStructureDTO)
    {
        if ((bool)DecoderStructureDTO.NumberBody && DecoderStructureDTO.NumberOrder == 0)
            DecoderStructureDTO.NumberOrder = SetNewNumberOrder(DecoderStructureDTO);
        if ((bool)DecoderStructureDTO.DescriptionBody && DecoderStructureDTO.DescriptionOrder == 0)
            DecoderStructureDTO.DescriptionOrder = SetNewDescriptionOrder(DecoderStructureDTO);
        var _validationResultDTO = DecoderStructure_Validator.CreateDecoderStructure_Validation(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        _validationResultDTO = DecoderStructure_Repository.CreateDecoderStructure(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        var _attributeDTO = Attribute_Repository.GetAttributeByID((int)DecoderStructureDTO.AttributeID);
        if (DecoderStructureDTO.ValueIDArray.Length > 0 && (bool)_attributeDTO.HasMultipleOptions)
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.SubClass,
                ParentValueID = DecoderStructureDTO.SubClassID,
                ChildAttributeID = DecoderStructureDTO.AttributeID,
                ChildValueIDArray = DecoderStructureDTO.ValueIDArray,
                AddedByID = DecoderStructureDTO.AddedByID,
                AddedDate = DateTime.Now,
                IsActive = true
            };
            _validationResultDTO = ValueLink_Service.CreateMultipleValueLink(_valueLinkDTO);

        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDecoderStructure_Global(DecoderStructureDTO DecoderStructureDTO)
    {
        // Step 1. Validate fields
        var _validationResultDTO = DecoderStructure_Validator.UpdateDecoderStructure_Validation(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2. Define Description order
        var _currentDecoderStructureDTO = DecoderStructure_Repository.GetDecoderStructureByID((int)DecoderStructureDTO.ID);
        if (_currentDecoderStructureDTO.ID != 0 || !(bool)_currentDecoderStructureDTO.DescriptionBody && (bool)DecoderStructureDTO.DescriptionBody)
            DecoderStructureDTO.DescriptionOrder = SetNewDescriptionOrder(DecoderStructureDTO);
        else if (_currentDecoderStructureDTO.ID != 0 || (bool)_currentDecoderStructureDTO.DescriptionBody && !(bool)DecoderStructureDTO.DescriptionBody)
            DecoderStructureDTO.DescriptionOrder = 0;
        // Step 3. Define Number Order
        if (_currentDecoderStructureDTO.ID != 0 || !(bool)_currentDecoderStructureDTO.DescriptionBody && (bool)DecoderStructureDTO.DescriptionBody)
            DecoderStructureDTO.DescriptionOrder = SetNewDescriptionOrder(DecoderStructureDTO);
        else if (_currentDecoderStructureDTO.ID != 0 || (bool)_currentDecoderStructureDTO.DescriptionBody && !(bool)DecoderStructureDTO.DescriptionBody)
            DecoderStructureDTO.DescriptionOrder = 0;
        // Step 4. Update Decoder structure record
        _validationResultDTO = DecoderStructure_Repository.UpdateDecoderStructure(DecoderStructureDTO);
        // Step 5. Update Value Links
        var _attributeDTO = Attribute_Repository.GetAttributeByID((int)DecoderStructureDTO.AttributeID);
        if (DecoderStructureDTO.ValueIDArray.Length > 0 && (bool)_attributeDTO.HasMultipleOptions)
        {
            _validationResultDTO = CreateOrDeleteValueLinkSelection(DecoderStructureDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteDecoderStructure_Global(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = DecoderStructure_Validator.DeleteDecoderStructure_Validation(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        _validationResultDTO = DecoderStructure_Repository.DeleteDecoderStructure(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        var _valueLinkDTO = new ValueLinkDTO
        {
            ParentAttributeID = (int)Attribute_Enum.SubClass,
            ParentValueID = DecoderStructureDTO.SubClassID,
            ChildAttributeID = DecoderStructureDTO.AttributeID
        };
        var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
        if (_valueLinkList.Count() > 0)
        {
            _validationResultDTO = ValueLink_Repository.DeleteMultipleValueLink(_valueLinkList);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
        }

        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMultiple_Global(List<DecoderStructureDTO> DecoderStructureList)
    {
        var _validationResultDTO = new ValidationResultDTO();

        foreach (var DecoderStructureDTO in DecoderStructureList) 
        {
            if ((bool)DecoderStructureDTO.NumberBody && DecoderStructureDTO.NumberOrder == 0)
                DecoderStructureDTO.NumberOrder = SetNewNumberOrder(DecoderStructureDTO);
            if ((bool)DecoderStructureDTO.DescriptionBody && DecoderStructureDTO.DescriptionOrder == 0)
                DecoderStructureDTO.DescriptionOrder = SetNewDescriptionOrder(DecoderStructureDTO);
            _validationResultDTO = DecoderStructure_Validator.CreateDecoderStructure_Validation(DecoderStructureDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
        }

        _validationResultDTO = DecoderStructure_Repository.CreateMultiple(DecoderStructureList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        foreach (var DecoderStructureDTO in DecoderStructureList) 
        {
            var _attributeDTO = Attribute_Repository.GetAttributeByID((int)DecoderStructureDTO.AttributeID);
            if (DecoderStructureDTO.ValueIDArray.Length > 0 && (bool)_attributeDTO.HasMultipleOptions)
            {
                var _valueLinkDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int)Attribute_Enum.SubClass,
                    ParentValueID = DecoderStructureDTO.SubClassID,
                    ChildAttributeID = DecoderStructureDTO.AttributeID,
                    ChildValueIDArray = DecoderStructureDTO.ValueIDArray,
                    AddedByID = DecoderStructureDTO.AddedByID,
                    AddedDate = DateTime.Now,
                    IsActive = true
                };
                _validationResultDTO = ValueLink_Service.CreateMultipleValueLink(_valueLinkDTO);
            }
        }
        return _validationResultDTO;
    }
    public static List<DecoderStructureDTO> GetDecoderStructureList_Global(DecoderStructureDTO DecoderStructureDTO, PagedResultDTO<DecoderStructureDTO> PagedResultDTO = null)
    {
        var _decoderstructureglobalList = new List<DecoderStructureDTO>();
        try
        {
            var _decoderstructureList = DecoderStructure_Repository.GetDecoderStructureList(DecoderStructureDTO, PagedResultDTO);

            // if DecoderStructure is empty, return list
            if (_decoderstructureList.Count() == 0 || (!DecoderStructureDTO.GetAttributeDTO && !DecoderStructureDTO.GetDecoderDTO && !DecoderStructureDTO.GetValueDTO && DecoderStructureDTO!.GetAttributeValueLinkList))
            {
                _decoderstructureglobalList = _decoderstructureList;
                return _decoderstructureglobalList;
            }

            _decoderstructureglobalList = GetDecoderStructureRelatedData(DecoderStructureDTO, _decoderstructureList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderstructureglobalList;
    }
    public static List<DecoderStructureDTO> GetDecoderStructureRelatedData(DecoderStructureDTO DecoderStructureDTO, List<DecoderStructureDTO> DecoderStructureList)
    {
        var _decoderstructureglobalList = new List<DecoderStructureDTO>();
        var _decoderDict = new Dictionary<int?, DecoderDTO>();
        var _attributeDict = new Dictionary<int?, AttributeDTO>();
        var _attributeValueLinkDict = new Dictionary<int?, List<ValueLinkDTO>>();

        var _valueDict = new Dictionary<int?, ValueDTO>();
        try
        {
            if (DecoderStructureDTO.GetDecoderDTO)
            {
                DecoderStructureDTO.DecoderDTO.DecoderIDArray = DecoderStructureList.GroupBy(g => g.DecoderID)
                                                                                    .Select(s => s.Key)
                                                                                    .ToArray();

                _decoderDict = Decoder_Service.GetDecoderList_Global(DecoderStructureDTO.DecoderDTO)
                                              .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DecoderStructureDTO.GetAttributeDTO)
            {
                DecoderStructureDTO.AttributeDTO.AttributeIDArray = DecoderStructureList.GroupBy(g => g.AttributeID)
                                                                                        .Select(s => s.Key)
                                                                                        .ToArray();

                _attributeDict = Attribute_Service.GetAttributeList_Global(DecoderStructureDTO.AttributeDTO)
                                                  .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DecoderStructureDTO.GetValueDTO)
            {
                DecoderStructureDTO.ValueDTO.ValueIDArray = DecoderStructureList.GroupBy(g => g.ValueID)
                                                                                   .Select(s => s.Key)
                                                                                   .ToArray();

                _valueDict = Value_Service.GetValueList_Global(DecoderStructureDTO.SubClassDTO)
                                          .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DecoderStructureDTO.GetAttributeValueLinkList)
            {
                DecoderStructureDTO.AttributeIDArray = DecoderStructureList.GroupBy(g => g.AttributeID)
                                                                                        .Select(s => s.Key)
                                                                                        .ToArray();
                var _attribute_ValueLinkDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int)Attribute_Enum.SubClass,
                    ParentValueID = DecoderStructureDTO.SubClassID,
                    ParentAttributeIDArray = DecoderStructureDTO.AttributeIDArray,
                    GetChildValueDTO = true
                };
                _attributeValueLinkDict = ValueLink_Service.GetValueLinkList_Global(_attribute_ValueLinkDTO)
                                          .GroupBy(g => g.ChildAttributeID)
                                          .ToDictionary(keySelector: m => m.Key, elementSelector: m => m.ToList());
            }



            foreach (var _decoderstructureDTO in DecoderStructureList)
            {
                if (DecoderStructureDTO.GetDecoderDTO && _decoderDict.ContainsKey(_decoderstructureDTO.DecoderID))
                {
                    _decoderstructureDTO.DecoderDTO = _decoderDict[_decoderstructureDTO.DecoderID];
                }
                if (DecoderStructureDTO.GetAttributeDTO && _attributeDict.ContainsKey(_decoderstructureDTO.AttributeID))
                {
                    _decoderstructureDTO.AttributeDTO = _attributeDict[_decoderstructureDTO.AttributeID];
                }
                if (DecoderStructureDTO.GetValueDTO && _valueDict.ContainsKey(_decoderstructureDTO.ValueID))
                {
                    _decoderstructureDTO.ValueDTO = _valueDict[_decoderstructureDTO.ValueID];
                }
                if (DecoderStructureDTO.GetAttributeValueLinkList && _attributeValueLinkDict.ContainsKey(_decoderstructureDTO.AttributeID))
                {
                    var _attributeValueLinkList = _attributeValueLinkDict[_decoderstructureDTO.AttributeID];
                    _decoderstructureDTO.ValueList = _attributeValueLinkList.Select(s => s.ChildValueDTO).ToList();
                    _decoderstructureDTO.ValueIDArray = _attributeValueLinkList.Select(s => s.ChildValueID).ToArray();
                    _decoderstructureDTO.ValueName = string.Join(",", _attributeValueLinkList.Select(s => s.ChildValueDTO.Name).ToArray());
                }
                _decoderstructureglobalList.Add(_decoderstructureDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderstructureglobalList;
    }
    public static int GetDecoderStructureTotalCount(PagedResultDTO<DecoderStructureDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DecoderStructure_Repository.GetDecoderStructureCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreateOrDeleteValueLinkSelection(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _currentAttributeValueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.SubClass,
                ParentValueID = DecoderStructureDTO.SubClassID,
                ChildAttributeID = DecoderStructureDTO.AttributeID
            };
            var _currentAttributeValueLink = ValueLink_Service.GetValueLinkList_Global(_currentAttributeValueLinkDTO);
            var _valueLinkToDeleteList = _currentAttributeValueLink.Where(w => !DecoderStructureDTO.ValueIDArray.Contains(w.ChildValueID)).ToList();
            var _valueLinkToCreateList = DecoderStructureDTO.ValueIDArray.Where(w => !_currentAttributeValueLink.Select(s => s.ChildValueID).Contains(w)).ToArray();
            if (_valueLinkToDeleteList.Count() > 0)
            {
                _validationResultDTO = ValueLink_Repository.DeleteMultipleValueLink(_valueLinkToDeleteList);
            }
            if (_valueLinkToCreateList.Count() > 0)
            {
                var _valueLinkDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int)Attribute_Enum.SubClass,
                    ParentValueID = DecoderStructureDTO.SubClassID,
                    ChildAttributeID = DecoderStructureDTO.AttributeID,
                    ChildValueIDArray = _valueLinkToCreateList,
                    AddedByID = DecoderStructureDTO.AddedByID,
                    AddedDate = DateTime.Now,
                    IsActive = true

                };
                _validationResultDTO = ValueLink_Service.CreateMultipleValueLink(_valueLinkDTO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO LinkBaseAttributes(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            //Step 1. Get 
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = DecoderDTO.ClassID,
                ChildAttributeID = (int)Attribute_Enum.Class_Sequence
            };
            var _classIDDTO = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO).FirstOrDefault();
            //Step 1. Add Class Attribute 
            var _classDTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Class,
                NumberBody = true,
                NumberOrder = 1,
                DescriptionBody = true,
                DescriptionOrder = 1,
                ValueID = DecoderDTO.ClassID,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,

            };
            _validationResultDTO = CreateDecoderStructure_Global(_classDTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 2. Add Sub Class Attribute 
            var _subClassDTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.SubClass,
                NumberBody = true,
                NumberOrder = 2,
                DescriptionBody = true,
                DescriptionOrder = 2,
                ValueID = DecoderDTO.SubClassID,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_subClassDTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 3. Add Symbol Attribute 
            var _symbol1DTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Symbol,
                NumberBody = true,
                NumberOrder = 3,
                DescriptionBody = false,
                DescriptionOrder = 0,
                ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_symbol1DTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            // Step 4. Add Class Sequence Attribute
            var _classidDTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Class_Sequence,
                NumberBody = true,
                NumberOrder = 4,
                ValueID = _classIDDTO.ChildValueID,
                DescriptionBody = false,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_classidDTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 5. Add Symbol Attribute 
            var _symbol2DTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Symbol,
                NumberBody = true,
                NumberOrder = 5,
                DescriptionBody = false,
                DescriptionOrder = 0,
                ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_symbol2DTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 6. Add Variant Attribute 
            var _variantDTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Variant,
                NumberBody = true,
                NumberOrder = 6,
                DescriptionBody = false,
                DescriptionOrder = 0,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_variantDTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 7. Add Symbol Attribute 
            var _symbol3DTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Symbol,
                NumberBody = true,
                NumberOrder = 7,
                DescriptionBody = false,
                DescriptionOrder = 0,
                ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_symbol3DTO);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
            //Step 8. Add Symbol Attribute 
            var _customerConsigment = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID,
                AttributeID = (int?)Attribute_Enum.Costumer_Consigment,
                NumberBody = true,
                NumberOrder = 8,
                DescriptionBody = false,
                DescriptionOrder = 0,
                AddedByID = DecoderDTO.AddedByID,
                AddedDate = DecoderDTO.AddedDate,
            };
            _validationResultDTO = CreateDecoderStructure_Global(_customerConsigment);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO LinkBaseMultipleAttributes(List<DecoderDTO> DecoderList)
    {
        var _validationResultDTO = new ValidationResultDTO();
        var _decoderStructureList = new List<DecoderStructureDTO>();
        try
        {
            foreach (var DecoderDTO in DecoderList) 
            {
                //Step 1. Get 
                var _valueLinkDTO = new ValueLinkDTO
                {
                    ParentAttributeID = (int)Attribute_Enum.Class,
                    ParentValueID = DecoderDTO.ClassID,
                    ChildAttributeID = (int)Attribute_Enum.Class_Sequence
                };
                var _classIDDTO = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO).FirstOrDefault();
                //Step 1. Add Class Attribute 
                var _classDTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Class,
                    NumberBody = true,
                    NumberOrder = 1,
                    DescriptionBody = true,
                    DescriptionOrder = 1,
                    ValueID = DecoderDTO.ClassID,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                // add each DecoderStructure to the following list
                _decoderStructureList.Add(_classDTO);

                //Step 2. Add Sub Class Attribute 
                var _subClassDTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.SubClass,
                    NumberBody = true,
                    NumberOrder = 2,
                    DescriptionBody = true,
                    DescriptionOrder = 2,
                    ValueID = DecoderDTO.SubClassID,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_subClassDTO);

                //Step 3. Add Symbol Attribute 
                var _symbol1DTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Symbol,
                    NumberBody = true,
                    NumberOrder = 3,
                    DescriptionBody = false,
                    DescriptionOrder = 0,
                    ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_symbol1DTO);

                // Step 4. Add Class Sequence Attribute
                var _classidDTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Class_Sequence,
                    NumberBody = true,
                    NumberOrder = 4,
                    ValueID = _classIDDTO.ChildValueID,
                    DescriptionBody = false,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_classidDTO);

                //Step 5. Add Symbol Attribute 
                var _symbol2DTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Symbol,
                    NumberBody = true,
                    NumberOrder = 5,
                    DescriptionBody = false,
                    DescriptionOrder = 0,
                    ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_symbol2DTO);

                //Step 6. Add Variant Attribute 
                var _variantDTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Variant,
                    NumberBody = true,
                    NumberOrder = 6,
                    DescriptionBody = false,
                    DescriptionOrder = 0,
                    ValueID = (int)Value_Enum.Customer_Consigment.CustomerConsigment,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_variantDTO);

                //Step 7. Add Symbol Attribute 
                var _symbol3DTO = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Symbol,
                    NumberBody = true,
                    NumberOrder = 7,
                    DescriptionBody = false,
                    DescriptionOrder = 0,
                    ValueID = (int)Value_Enum.Symbol_Enum.Dash,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_symbol3DTO);

                //Step 8. Add Symbol Attribute 
                var _customerConsigment = new DecoderStructureDTO
                {
                    DecoderID = DecoderDTO.ID,
                    AttributeID = (int?)Attribute_Enum.Costumer_Consigment,
                    NumberBody = true,
                    NumberOrder = 8,
                    DescriptionBody = false,
                    DescriptionOrder = 0,
                    ValueID = (int)Value_Enum.Customer_Consigment.Empty,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DecoderDTO.AddedDate,
                };
                _decoderStructureList.Add(_customerConsigment);
            }

            // Create multiple Decoder Structure
            _validationResultDTO = CreateMultiple_Global(_decoderStructureList);
            if (_validationResultDTO.Result == false) return _validationResultDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO ReorderNumberOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            int _oldNumberOrder = 0;
            var _dswithsamenumberDTO = DecoderStructure_Repository.GetDecoderStructureByID((int)DecoderStructureDTO.NewChangedDecoderStructureID);
            if (_dswithsamenumberDTO != null)
            {
                _oldNumberOrder = _dswithsamenumberDTO.NumberOrder;
                _dswithsamenumberDTO.NumberOrder = DecoderStructureDTO.NumberOrder;
                _validationResultDTO = DecoderStructure_Repository.UpdateDecoderStructure(_dswithsamenumberDTO);
                _validationResultDTO.Data = _oldNumberOrder;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO ReorderDescriptionOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            int _oldDescriptionOrder = 0;
            var _dswithsamenumberDTO = DecoderStructure_Repository.GetDecoderStructureByID((int)DecoderStructureDTO.NewChangedDecoderStructureID);
            if (_dswithsamenumberDTO != null)
            {
                _oldDescriptionOrder = _dswithsamenumberDTO.DescriptionOrder;
                _dswithsamenumberDTO.DescriptionOrder = DecoderStructureDTO.DescriptionOrder;
                _validationResultDTO = DecoderStructure_Repository.UpdateDecoderStructure(_dswithsamenumberDTO);
                _validationResultDTO.Data = _oldDescriptionOrder;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static int GetLastNumberOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        int _lastNumberOrder = 0;
        try
        {
            var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = DecoderStructureDTO.DecoderID };
            if (GetDecoderStructureList_Global(_decoderStructureDTO).Count() > 0)
            {
                _lastNumberOrder = GetDecoderStructureList_Global(_decoderStructureDTO).OrderBy(o => o.NumberOrder)
                                                                                       .LastOrDefault()
                                                                                       .NumberOrder;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return _lastNumberOrder;

    }
    public static int SetNewNumberOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        return GetLastNumberOrder(DecoderStructureDTO) + 1;
    }
    public static int GetLastDescriptionOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        int _lastDescriptionOrder = 0;
        try
        {
            var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = DecoderStructureDTO.DecoderID };
            if (GetDecoderStructureList_Global(_decoderStructureDTO).Count() > 0)
            {
                _lastDescriptionOrder = GetDecoderStructureList_Global(_decoderStructureDTO).OrderBy(o => o.DescriptionOrder)
                                                                                            .LastOrDefault()
                                                                                            .DescriptionOrder;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _lastDescriptionOrder;
    }
    public static int SetNewDescriptionOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        return GetLastDescriptionOrder(DecoderStructureDTO) + 1;
    }
    public static ValidationResultDTO UpdateNumberOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = DecoderStructure_Validator.UpdateDecoderStructure_Validation(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        _validationResultDTO = ReorderNumberOrder(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        DecoderStructureDTO.NumberOrder = _validationResultDTO.Data;
        _validationResultDTO = DecoderStructure_Repository.UpdateDecoderStructure(DecoderStructureDTO);

        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDescriptionOrder(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = DecoderStructure_Validator.UpdateDecoderStructure_Validation(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        _validationResultDTO = ReorderDescriptionOrder(DecoderStructureDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        DecoderStructureDTO.DescriptionOrder = _validationResultDTO.Data;
        _validationResultDTO = DecoderStructure_Repository.UpdateDecoderStructure(DecoderStructureDTO);

        return _validationResultDTO;
    }
    #endregion
}
