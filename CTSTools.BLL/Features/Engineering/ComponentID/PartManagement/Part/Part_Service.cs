using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class_Sequence;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;

public class Part_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreatePart_Global(PartDTO PartDTO)
    {
        //Step 1.
        var _validationResultDTO = Part_Validator.CreatePart_Validation(PartDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2.
        var _part_attributeDTO = new Part_AttributeDTO
        {
            ValueIDArray = PartDTO.ValueList.Select(s => s.ID).ToArray(),
            DecoderID = PartDTO.DecoderID,
        };
        _validationResultDTO = Part_Attribute_Validator.ValidatePart_Attribute(_part_attributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3.
        _validationResultDTO = CreatePartNumber(PartDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        else
            PartDTO = _validationResultDTO.Data;
        //Step 4. Create Description
        _validationResultDTO = CreatePartDescription(PartDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        else 
            PartDTO = _validationResultDTO.Data;
        //Step 5. Create Part
        _validationResultDTO = Part_Repository.CreatePart(PartDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        else
            PartDTO.ID = _validationResultDTO.Data;

        //Step 5. Create Part_Attribute            
        _validationResultDTO = Part_Attribute_Service.CreateMultiplePart_Attribute_Global(PartDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        else
            _validationResultDTO.Data = PartDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdatePart_Global(PartDTO PartDTO)
    {
        var _validationResultDTO = Part_Validator.UpdatePart_Validation(PartDTO);
        if (_validationResultDTO.Result)
        {
            PartDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Part_Repository.UpdatePart(PartDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeletePart_Global(PartDTO PartDTO)
    {
        var _validationResultDTO = Part_Validator.DeletePart_Validation(PartDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Part_Repository.DeletePart(PartDTO);
        }
        return _validationResultDTO;
    }
    public static List<PartDTO> GetPartList_Global(PartDTO PartDTO, PagedResultDTO<PartDTO> PagedPartDTO = null)
    {
        var _facilityGlobalList = new List<PartDTO>();
        try
        {
            var _facilityList = Part_Repository.GetPartList(PartDTO, PagedPartDTO);
            _facilityGlobalList = _facilityList;
            return _facilityGlobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _facilityGlobalList;
    }
    public static int GetTotalCount(PagedResultDTO<PartDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Part_Repository.GetPartCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreatePartNumber(PartDTO PartDTO)
    {
        var _validationReusultDTO = new ValidationResultDTO();
        var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = (int)PartDTO.DecoderID, GetValueDTO = true, };
        var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO);
        var _descriptionAttributeList = _decoderStructureList.Where(w => w.DescriptionBody == true).OrderBy(o => o.DescriptionOrder).ToList();

        var _numberAttributeList = _decoderStructureList.Where(w => w.NumberBody == true).OrderBy(o => o.NumberOrder).ToList();
        foreach (var _numberAttributeDTO in _numberAttributeList)
        {
            // Step v. 
            if (_numberAttributeDTO.AttributeID == (int)Attribute_Enum.Class_Sequence)
            {
                _validationReusultDTO = Class_Sequence_Service.GetClass_Sequence_Global(_numberAttributeDTO.ValueName);
                if (!_validationReusultDTO.Result)
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Class ID Error",
                        Description = ""
                    };
                _numberAttributeDTO.ValueDTO.Code = _validationReusultDTO.Data;
            }
            // Step x . 
            else if (_numberAttributeDTO.AttributeID == (int)Attribute_Enum.Variant)
            {
                string _variantValue = PartDTO.ValueList.Where(w => w.AttributeID.ToString() == _numberAttributeDTO.ValueDTO.Code).FirstOrDefault().Code;
                _numberAttributeDTO.ValueDTO.Code = _variantValue;
            }
            // Step xx . 
            var _partAttributeInDescriptionDTO = PartDTO.ValueList.Where(w => w.AttributeID == _numberAttributeDTO.AttributeID).FirstOrDefault();
            if (_partAttributeInDescriptionDTO != null)
            {
                _numberAttributeDTO.ValueDTO.Code = _partAttributeInDescriptionDTO.Code;
            }
            PartDTO.Number += _numberAttributeDTO.ValueDTO.Code;

        }
        _validationReusultDTO.Data = PartDTO;
        return _validationReusultDTO;
    }
    public static ValidationResultDTO CreatePartDescription(PartDTO PartDTO)
    {
        var _validationReusultDTO = new ValidationResultDTO();
        var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = (int)PartDTO.DecoderID, GetValueDTO = true, DescriptionBody = true };
        var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO);
        var _descriptionAttributeList = _decoderStructureList.Where(w => w.DescriptionBody == true).OrderBy(o => o.DescriptionOrder).ToList();

        foreach (var _descriptionAttributeDTO in _descriptionAttributeList)
        {

            var _partDTO = PartDTO.ValueList.Where(w => w.AttributeID == _descriptionAttributeDTO.AttributeID).FirstOrDefault();
            if (_partDTO == null)
            {
                PartDTO.Description += _descriptionAttributeDTO.ValueDTO.Name;

            }
            else
            {
                PartDTO.Description += _partDTO.Name;
            }
        }
        _validationReusultDTO.Data = PartDTO;
        return _validationReusultDTO;
    }
    public static ValidationResultDTO CreateMultipleFromDecoder(List<DecoderDTO> DecoderList) 
    {
        var _validationResultDTO = new ValidationResultDTO();
        try 
        {
            var _partList = new List<PartDTO>();
            foreach (var DecoderDTO in DecoderList) 
            {
                var _valueIDList = DecoderDTO.PartTypeIDArray.ToList();
                _valueIDList.Add((int)Value_Enum.Customer_Consigment.Empty);
                var _valueDTO = new ValueDTO { ValueIDArray = _valueIDList.ToArray() };
                var _valueList = Value_Service.GetValueList_Global(_valueDTO);
                var _newValueList = _valueList.GroupBy(ValueDTO => ValueDTO.AttributeID).Select(groupAttributeID => groupAttributeID.First()).ToList();
                var _partDTO = new PartDTO 
                {
                    DecoderID = DecoderDTO.ID,
                    MfgPartNumber = DecoderDTO.LastUpdateByName,
                    SupplierID = DecoderDTO.ComponentTypeIDArray.FirstOrDefault(),
                    Comment = DecoderDTO.SubClassName,
                    ValueList = _newValueList,
                    AddedByID = DecoderDTO.AddedByID,
                    AddedDate = DateTime.Now,
                    IsActive = true,
                };
                //_partList.Add(_partDTO);
                _validationResultDTO = CreatePart_Global(_partDTO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    #endregion
}
