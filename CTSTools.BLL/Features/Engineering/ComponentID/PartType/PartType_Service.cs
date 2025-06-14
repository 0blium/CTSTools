using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartType_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreatePartType_Global(PartTypeDTO PartTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = PartType_Validator.CreatePartType_Validation(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and create value
            var _value = new ValueDTO {
            Name = PartTypeDTO.Name,
            Code = PartTypeDTO.Code,
            Description = PartTypeDTO.Description,
            AttributeID = (int)Attribute_Enum.PartType,
            AddedByID = PartTypeDTO.AddedByID,
            IsActive = PartTypeDTO.IsActive,
            };
            _validationResultDTO = Value_Service.CreateValue_Global(_value);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. create part type
            PartTypeDTO.AddedDate = DateTime.Now;
            PartTypeDTO.AttributeID = (int)Attribute_Enum.PartType;
            PartTypeDTO.ValueID = _validationResultDTO.Data;
            _validationResultDTO = PartType_Repository.CreatePartType(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdatePartType_Global(PartTypeDTO PartTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = PartType_Validator.UpdatePartType_Validation(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. create ValueDTO and update value
            var _value = new ValueDTO
            {
                ID = PartTypeDTO.ValueID,
                Name = PartTypeDTO.Name,
                Code = PartTypeDTO.Code,
                Description = PartTypeDTO.Description,
                AttributeID = (int)Attribute_Enum.PartType,
                LastUpdateByID = PartTypeDTO.LastUpdateByID,
                IsActive = PartTypeDTO.IsActive,
            };
            _validationResultDTO = Value_Service.UpdateValue_Global(_value);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. Update part type
            PartTypeDTO.LastUpdate = DateTime.Now;
            PartTypeDTO.AttributeID = (int)Attribute_Enum.PartType;
            _validationResultDTO = PartType_Repository.UpdatePartType(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static ValidationResultDTO DeletePartType_Global(PartTypeDTO PartTypeDTO)
        {
            // Step 1. validate information
            var _validationResultDTO = PartType_Validator.DeletePartType_Validation(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 2. Delete ValueDTO
            var _value = new ValueDTO{ ID = PartTypeDTO.ValueID, IsActive = PartTypeDTO.IsActive };
            _validationResultDTO = Value_Service.DeleteValue_Global(_value);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            // Step 3. Delete part type
            _validationResultDTO = PartType_Repository.DeletePartType(PartTypeDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;

            return _validationResultDTO;
        }
        public static List<PartTypeDTO> GetPartTypeList_Global(PartTypeDTO PartTypeDTO, PagedResultDTO<PartTypeDTO> PagedResultDTO = null)
        {
            var _parttypeglobalList = new List<PartTypeDTO>();
            try
            {
                var _parttypeList = PartType_Repository.GetPartTypeList(PartTypeDTO, PagedResultDTO);
                // if PartType is empty, return list
                _parttypeglobalList = _parttypeList;


            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _parttypeglobalList;
        }
        public static int GetPartTypeTotalCount(PagedResultDTO<PartTypeDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = PartType_Repository.GetPartTypeCount(PagedResultDTO.Filter, PagedResultDTO);
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
}
