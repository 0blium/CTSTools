using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using DevExpress.XtraReports.Design;
using Elmah;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

public class Part_Attribute_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreatePart_Attribute_Global(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validationResultDTO = Part_Attribute_Validator.CreatePart_Attribute_Validation(Part_AttributeDTO);
        if (_validationResultDTO.Result)
        {
            Part_AttributeDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Part_Attribute_Repository.CreatePart_Attribute(Part_AttributeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdatePart_Attribute_Global(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validationResultDTO = Part_Attribute_Validator.UpdatePart_Attribute_Validation(Part_AttributeDTO);
        if (_validationResultDTO.Result)
        {
            Part_AttributeDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Part_Attribute_Repository.UpdatePart_Attribute(Part_AttributeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeletePart_Attribute_Global(Part_AttributeDTO Part_AttributeDTO)
    {
        var _ValidationResultDTO = Part_Attribute_Validator.DeletePart_Attribute_Validation(Part_AttributeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Part_Attribute_Repository.DeletePart_Attribute(Part_AttributeDTO);
        }

        return _ValidationResultDTO;
    }
    public static List<Part_AttributeDTO> GetPart_AttributeList_Global(Part_AttributeDTO Part_AttributeDTO, PagedResultDTO<Part_AttributeDTO> PagedResultDTO = null)
    {
        var _departmentglobalList = new List<Part_AttributeDTO>();
        try
        {
            var _departmentList = Part_Attribute_Repository.GetPart_AttributeList(Part_AttributeDTO, PagedResultDTO);
            // if Part_Attribute is empty, return list
            if (_departmentList.Count() == 0)
            {
                _departmentglobalList = _departmentList;
                return _departmentglobalList;
            }
            if (!Part_AttributeDTO.GetPartDTO && !Part_AttributeDTO.GetAttributeDTO && !Part_AttributeDTO.GetValueDTO && !Part_AttributeDTO.GetDecoderDTO)
            {
                _departmentglobalList = _departmentList;
                return _departmentglobalList;
            }
            _departmentglobalList = GetPart_AttributeRelatedData(Part_AttributeDTO, _departmentList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentglobalList;
    }
    public static List<Part_AttributeDTO> GetPart_AttributeRelatedData(Part_AttributeDTO Part_AttributeDTO, List<Part_AttributeDTO> Part_AttributeList)
    {
        var _part_attributeglobalList = new List<Part_AttributeDTO>();
        var _partDict = new Dictionary<int?, PartDTO>();
        var _decoderDict = new Dictionary<int?, DecoderDTO>();
        var _attributeDict = new Dictionary<int?, AttributeDTO>();
        var _valueDict = new Dictionary<int?, ValueDTO>();
        try
        {
            if (Part_AttributeDTO.GetPartDTO)
            {
                Part_AttributeDTO.PartDTO.PartIDArray = Part_AttributeList.GroupBy(g => g.PartID)
                                                                          .Select(s => s.Key)
                                                                          .ToArray();

                _partDict = Part_Service.GetPartList_Global(Part_AttributeDTO.PartDTO)
                                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Part_AttributeDTO.GetDecoderDTO)
            {
                Part_AttributeDTO.DecoderDTO.DecoderIDArray = Part_AttributeList.GroupBy(g => g.DecoderID)
                                                                                .Select(s => s.Key)
                                                                                .ToArray();

                _decoderDict = Decoder_Service.GetDecoderList_Global(Part_AttributeDTO.DecoderDTO)
                                              .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Part_AttributeDTO.GetAttributeDTO)
            {
                Part_AttributeDTO.AttributeDTO.AttributeIDArray = Part_AttributeList.GroupBy(g => g.AttributeID)
                                                                                    .Select(s => s.Key)
                                                                                    .ToArray();

                _attributeDict = Attribute_Service.GetAttributeList_Global(Part_AttributeDTO.AttributeDTO)
                                                  .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Part_AttributeDTO.GetValueDTO)
            {
                Part_AttributeDTO.ValueDTO.ValueIDArray = Part_AttributeList.GroupBy(g => g.ValueID)
                                                                            .Select(s => s.Key)
                                                                            .ToArray();

                _valueDict = Value_Service.GetValueList_Global(Part_AttributeDTO.ValueDTO)
                                          .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _part_attributeDTO in Part_AttributeList)
            {
                if (_part_attributeDTO.GetDecoderDTO && _decoderDict.ContainsKey(_part_attributeDTO.DecoderDTO.ID))
                {
                    _part_attributeDTO.DecoderDTO = _decoderDict[_part_attributeDTO.DecoderDTO.ID];
                }
                if (Part_AttributeDTO.GetAttributeDTO && _attributeDict.ContainsKey((int)_part_attributeDTO.AttributeDTO.ID))
                {
                    _part_attributeDTO.AttributeDTO = _attributeDict[_part_attributeDTO.AttributeDTO.ID];
                }
                if (Part_AttributeDTO.GetValueDTO && _valueDict.ContainsKey((int)_part_attributeDTO.ValueDTO.ID))
                {
                    _part_attributeDTO.ValueDTO = _valueDict[_part_attributeDTO.ValueDTO.ID];
                }
                if (Part_AttributeDTO.GetPartDTO && _partDict.ContainsKey((int)_part_attributeDTO.PartDTO.ID))
                {
                    _part_attributeDTO.PartDTO = _partDict[_part_attributeDTO.PartDTO.ID];
                }
                _part_attributeglobalList.Add(Part_AttributeDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _part_attributeglobalList;
    }
    public static int GetPart_AttributeTotalCount(PagedResultDTO<Part_AttributeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Part_Attribute_Repository.GetPart_AttributeCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion


    public static ValidationResultDTO CreateMultiplePart_Attribute_Global(PartDTO PartDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        var _part_attributeList = new List<Part_AttributeDTO>();
        var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = (int)PartDTO.DecoderID, GetValueDTO = true };
        var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO).Where( w => w.AttributeID != (int)Attribute_Enum.Symbol);
        foreach (var _descriptionAttributeDTO in _decoderStructureList)
        {
            var _part_attributeDTO = new Part_AttributeDTO();
            var _partDTO = PartDTO.ValueList.Where(w => w.AttributeID == _descriptionAttributeDTO.AttributeID).FirstOrDefault();
            if (_partDTO == null)
            {
                _part_attributeDTO = new Part_AttributeDTO
                {
                    ValueID = _descriptionAttributeDTO.ValueID,
                    AttributeID = _descriptionAttributeDTO.AttributeID,
                    DecoderID = PartDTO.DecoderID,
                    PartID = PartDTO.ID,
                    AddedByID = PartDTO.AddedByID,
                    AddedDate = DateTime.Now,
                    IsActive = true
                };
            }
            else
            {
                _part_attributeDTO = new Part_AttributeDTO
                {
                    ValueID = _partDTO.ID,
                    AttributeID = _partDTO.AttributeID,
                    DecoderID = PartDTO.DecoderID,
                    PartID = PartDTO.ID,
                    AddedByID = PartDTO.AddedByID,
                    AddedDate = DateTime.Now,
                    IsActive = true
                };
            }
            _validationResultDTO = Part_Attribute_Validator.CreatePart_Attribute_Validation(_part_attributeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            _part_attributeList.Add(_part_attributeDTO);
        }
        _validationResultDTO = Part_Attribute_Repository.CreateMultiplePart_Attribute(_part_attributeList);
        return _validationResultDTO;
    }

}
