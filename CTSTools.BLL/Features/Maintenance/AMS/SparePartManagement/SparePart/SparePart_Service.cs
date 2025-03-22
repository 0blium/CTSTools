using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.ChangeLog;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart;

public class SparePart_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSparePart_Global(SparePartDTO SparePartDTO)
    {
        var _ValidationResultDTO = SparePart_Validator.CreateSparePart_Validation(SparePartDTO);
        if (_ValidationResultDTO.Result)
        {
            SparePartDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = SparePart_Repository.CreateSparePart(SparePartDTO);
            SparePartDTO.ID = _ValidationResultDTO.Data;
        }
        //Save SparePart Picture
        if (_ValidationResultDTO.Result && SparePartDTO.FileDTO?.Data != null)
        {
            SparePartDTO.FileDTO.ID = (int)SparePartDTO.ID;
            SparePartDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.SparePartPictureDirectory;
            _ValidationResultDTO = File_Service.SaveFile(SparePartDTO.FileDTO);
        }
        //save ChangeLog
        if (_ValidationResultDTO.Result)
        {
            ChangeLog_Service.BuildChangeLogActionCreate<SparePartDTO>(SparePartDTO, (int)SparePartDTO.AddedByID, (int)SparePartDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSparePart_Global(SparePartDTO SparePartDTO)
    {
        var _ValidationResultDTO = SparePart_Validator.UpdateSparePart_Validation(SparePartDTO);
        var _previousSparePartDTO = SparePart_Service.GetSparePartList_Global(new SparePartDTO { ID = SparePartDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            SparePartDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SparePart_Repository.UpdateSparePart(SparePartDTO);
        }
        //Save SparePart Picture
        if (_ValidationResultDTO.Result && SparePartDTO.FileDTO?.Data != null)
        {
            SparePartDTO.FileDTO.ID = (int)SparePartDTO.ID;
            SparePartDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.SparePartPictureDirectory;
            _ValidationResultDTO = File_Service.SaveFile(SparePartDTO.FileDTO);
        }
        //save ChangeLog
        if (_ValidationResultDTO.Result)
        {
            ChangeLog_Service.BuildChangeLogActionUpdate<SparePartDTO>(_previousSparePartDTO, SparePartDTO, (int)SparePartDTO.LastUpdateByID, (int)SparePartDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSparePart_Global(SparePartDTO SparePartDTO)
    {
        var _ValidationResultDTO = SparePart_Validator.DeleteSparePart_Validation(SparePartDTO);
        var _previousSparePartDTO = SparePart_Service.GetSparePartList_Global(new SparePartDTO { ID = SparePartDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SparePart_Repository.DeleteSparePart(SparePartDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog_Service.BuildChangeLogActionDelete<SparePartDTO>(_previousSparePartDTO, (int)SparePartDTO.LastUpdateByID, (int)SparePartDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<SparePartDTO> GetSparePartList_Global(SparePartDTO SparePartDTO, PagedResultDTO<SparePartDTO> PagedResultDTO = null)
    {
        var _sparepartglobalList = new List<SparePartDTO>();
        try
        {
            var _sparepartList = SparePart_Repository.GetSparePartList(SparePartDTO, PagedResultDTO);
            // if SparePart is empty, return list
            //_sparepartglobalList = _sparepartList;
            if (SparePartDTO.GetImage)
            {
                foreach (var _sparePartDTO in _sparepartList)
                {
                    var _fileDTO = new FileDTO
                    {
                        ID = (int)_sparePartDTO.ID,
                        FileDirectory = (int)FileDirectory_Enum.SparePartPictureDirectory
                    };
                    _sparePartDTO.SparePartImage = File_Service.GetFile(_fileDTO).URL;
                    _sparepartglobalList.Add(_sparePartDTO);
                }
            }
            else
            {
                _sparepartglobalList = _sparepartList;
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartglobalList;
    }

    public static int GetSparePartTotalCount(PagedResultDTO<SparePartDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SparePart_Repository.GetSparePartCount(PagedResultDTO.Filter, PagedResultDTO);
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
