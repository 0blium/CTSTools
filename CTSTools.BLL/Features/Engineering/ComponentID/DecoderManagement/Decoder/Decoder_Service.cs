using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Atrribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;

public class Decoder_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDecoder_Global(DecoderDTO DecoderDTO)
    {
        //Step 1. Set status
        DecoderDTO.StatusID = (int?)Status_Enum.Part_Number_Configurator.Draft;
        //Step 2. Validate fields
        var _validationResultDTO = Decoder_Validator.CreateDecoder_Validation(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Create the decoder
        DecoderDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Decoder_Repository.CreateDecoder(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        DecoderDTO.ID = _validationResultDTO.Data;
        //Step 4. Add Decoder Structure Attributes
        _validationResultDTO = DecoderStructure_Service.LinkBaseAttributes(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 5. Link Values to the Sub Class
        _validationResultDTO = ValueLink_Service.LinkBaseValuesToSubClass(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO.Data = DecoderDTO.ID;
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDecoder_Global(DecoderDTO DecoderDTO)
    {
        var _ValidationResultDTO = Decoder_Validator.UpdateDecoder_Validation(DecoderDTO);
        if (_ValidationResultDTO.Result)
        {
            DecoderDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Decoder_Repository.UpdateDecoder(DecoderDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDecoder_Global(DecoderDTO DecoderDTO)
    {
        //Step 1. Get Decoder information
        DecoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderDTO.ID);
        //Step 2. Validate fields
        var _validationResultDTO = Decoder_Validator.DeleteDecoder_Validation(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Delete Value Link Values
        _validationResultDTO = DeleteValueLinkFromDecoder(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 4. Delete Decoder Structure Values
        _validationResultDTO = DeleteDecoderStructureFromDecoder(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 5. Delete decoder record        
        _validationResultDTO = Decoder_Repository.DeleteDecoder(DecoderDTO);

        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMultiple_Global(List<DecoderDTO> DecoderList)
    {
        //Step 2. Validate fields
        var _validationResultDTO = Decoder_Validator.CreateMultiple_Validation(DecoderList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Create the decoder
        _validationResultDTO = Decoder_Repository.CreateMultiple(DecoderList);
        //if (!_validationResultDTO.Result)
        //    return _validationResultDTO;
        //DecoderList.ID = _validationResultDTO.Data;
        ////Step 4. Add Decoder Structure Attributes
        //_validationResultDTO = DecoderStructure_Service.LinkBaseAttributes(DecoderList);
        //if (!_validationResultDTO.Result)
        //    return _validationResultDTO;
        ////Step 5. Link Values to the Sub Class
        //_validationResultDTO = ValueLink_Service.LinkBaseValuesToSubClass(DecoderList);
        //if (!_validationResultDTO.Result)
        //    return _validationResultDTO;
        //_validationResultDTO.Data = DecoderList.ID;
        return _validationResultDTO;
    }
    public static List<DecoderDTO> GetDecoderList_Global(DecoderDTO DecoderDTO, PagedResultDTO<DecoderDTO> PagedResultDTO = null)
    {
        var _decoderglobalList = new List<DecoderDTO>();
        try
        {
            var _decoderList = Decoder_Repository.GetDecoderList(DecoderDTO, PagedResultDTO);
            // if Decoder is empty, return list
            if (_decoderList.Count() == 0)
            {
                _decoderglobalList = _decoderList;
                return _decoderglobalList;
            }
            if (!(bool)DecoderDTO.GetStatusDTO)
            {
                _decoderglobalList = _decoderList;
                return _decoderglobalList;
            }
            _decoderglobalList = GetDecoderRelatedData(DecoderDTO, _decoderList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderglobalList;
    }
    public static List<DecoderDTO> GetDecoderRelatedData(DecoderDTO DecoderDTO, List<DecoderDTO> DecoderList)
    {
        var _decoderglobalList = new List<DecoderDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();

        try
        {
            if ((bool)DecoderDTO.GetStatusDTO)
            {
                DecoderDTO.StatusDTO.StatusIDArray = DecoderList.GroupBy(g => g.StatusDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DecoderDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _decoderDTO in DecoderList)
            {
                if ((bool)DecoderDTO.GetStatusDTO && _statusDict.ContainsKey(_decoderDTO.StatusDTO.ID))
                {
                    _decoderDTO.StatusDTO = _statusDict[_decoderDTO.StatusDTO.ID];
                }
                _decoderglobalList.Add(_decoderDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _decoderglobalList;
    }
    public static int GetDecoderTotalCount(PagedResultDTO<DecoderDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Decoder_Repository.GetDecoderCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO DeleteValueLinkFromDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.SubClass,
                ParentValueID = DecoderDTO.SubClassID
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            if (_valueLinkList.Count() > 0)
                _validationResultDTO = ValueLink_Repository.DeleteMultipleValueLink(_valueLinkList);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteDecoderStructureFromDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _decoderStructureDTO = new DecoderStructureDTO
            {
                DecoderID = DecoderDTO.ID
            };
            var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO);
            if (_decoderStructureList.Count() > 0)
                _validationResultDTO = DecoderStructure_Repository.DeleteMultipleDecoderStructure(_decoderStructureList);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO GetDecoderStructureFromDecoder(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            // Step 1.
            var _decoderDTO = GetDecoderList_Global(DecoderDTO).FirstOrDefault();
            if (_decoderDTO == null)
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Not found",
                    Description = "There's no decoder for this subclass. Please, contact with Engineering Team",
                    ErrorCode = (int)Decoder_ErrorCode.NoDecoder
                };
            //Step 2. Validate if is inactive
            if (_decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Released)
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Not released",
                    Description = "This decoder is not available for use. Contact to Engineering team if you consider this is an error.",
                    ErrorCode = (int)Decoder_ErrorCode.NoReleased
                };
            // Step 3. 
            var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = _decoderDTO.ID, GetAttributeDTO = true, AttributeDTO = { GetValueList = true }, GetAttributeValueLinkList = true, SubClassID = DecoderDTO.SubClassID };
            var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO);
            var _filteredDecoderStructuredList = _decoderStructureList.Where(w => (w.AttributeID != (int)Attribute_Enum.Class && 
                                                                                   w.AttributeID != (int)Attribute_Enum.SubClass && 
                                                                                   w.AttributeID != (int)Attribute_Enum.Symbol &&
                                                                                   w.DescriptionBody == true) || 
                                                                                   w.AttributeID == (int)Attribute_Enum.Costumer_Consigment)
                                                                      .ToList();
            if (_filteredDecoderStructuredList.Count() == 0)
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Error",
                    Description = "No attributes were found it",
                };
            _decoderDTO.DecoderStructureList = _decoderStructureList;
            _validationResultDTO.Data = _decoderDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO SubmitDecoder_Global(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = Decoder_Validator.SubmitDecoder_Validation(DecoderDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        var _decoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderDTO.ID);
        _decoderDTO.StatusID = (int)Status_Enum.Part_Number_Configurator.Released;
        _decoderDTO.LastUpdateByID = DecoderDTO.LastUpdateByID;
        _decoderDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = UpdateDecoder_Global(_decoderDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO EditDecoder_Global(DecoderDTO DecoderDTO)
    {
        DecoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderDTO.ID);
        DecoderDTO.StatusID = (int)Status_Enum.Part_Number_Configurator.Edit;
        DecoderDTO.LastUpdate = DateTime.Now;
        var _validationResultDTO = UpdateDecoder_Global(DecoderDTO);
        return _validationResultDTO;
    }

    #endregion
}

