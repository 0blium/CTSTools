using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevisionMap
{
    public static DocumentRevisionDTO XPOToDTO(DocumentRevisionXPO DocumentRevisionXPO)
    {
        var _documentRevisionDTO = new DocumentRevisionDTO();
        try
        {
            _documentRevisionDTO.ID = DocumentRevisionXPO.Oid;
            _documentRevisionDTO.Revision = DocumentRevisionXPO.Revision;
            _documentRevisionDTO.ChangeReason = DocumentRevisionXPO.ChangeReason;
            _documentRevisionDTO.DocumentID = (DocumentRevisionXPO.Document != null) ? DocumentRevisionXPO.Document.Oid : 0;
            _documentRevisionDTO.DocumentName = (DocumentRevisionXPO.Document != null) ? DocumentRevisionXPO.Document.Name : "Unnassigned";
            _documentRevisionDTO.StatusID = (DocumentRevisionXPO.Status != null) ? DocumentRevisionXPO.Status.Oid : 0;
            _documentRevisionDTO.StatusName = (DocumentRevisionXPO.Status != null) ? DocumentRevisionXPO.Status.Name : "Unnassigned";
            _documentRevisionDTO.AddedDate = (DocumentRevisionXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DocumentRevisionXPO.AddedDate : (DateTime?)null;
            _documentRevisionDTO.AddedByID = (DocumentRevisionXPO.AddedBy != null) ? DocumentRevisionXPO.AddedBy.Oid : 0;
            _documentRevisionDTO.AddedByName = (DocumentRevisionXPO.AddedBy != null) ? DocumentRevisionXPO.AddedBy.Name : "Unnassigned";
            _documentRevisionDTO.LastUpdate = (DocumentRevisionXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DocumentRevisionXPO.LastUpdate : (DateTime?)null;
            _documentRevisionDTO.LastUpdateByID = (DocumentRevisionXPO.LastUpdateBy != null) ? DocumentRevisionXPO.LastUpdateBy.Oid : 0;
            _documentRevisionDTO.LastUpdateByName = (DocumentRevisionXPO.LastUpdateBy != null) ? DocumentRevisionXPO.LastUpdateBy.Name : "Unnassigned";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentRevisionDTO;
    }

    public static DocumentRevisionXPO DTOtoXPO(DocumentRevisionDTO DocumentRevisionDTO, UnitOfWork UnitOfWork)
    {
        DocumentRevisionXPO _documentRevisionXPO;
        try
        {
            _documentRevisionXPO = DocumentRevisionDTO.ID == null || DocumentRevisionDTO.ID == 0 ? new DocumentRevisionXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DocumentRevisionXPO>(DocumentRevisionDTO.ID);
            _documentRevisionXPO.Revision = _documentRevisionXPO.Revision == DocumentRevisionDTO.Revision ? _documentRevisionXPO.Revision : DocumentRevisionDTO.Revision;
            _documentRevisionXPO.ChangeReason = _documentRevisionXPO.ChangeReason == DocumentRevisionDTO.ChangeReason ? _documentRevisionXPO.ChangeReason : DocumentRevisionDTO.ChangeReason;
            _documentRevisionXPO.Document = (_documentRevisionXPO.Document != null && _documentRevisionXPO.Document.Oid == DocumentRevisionDTO.DocumentID) ? _documentRevisionXPO.Document : UnitOfWork.GetObjectByKey<DocumentXPO>(DocumentRevisionDTO.DocumentID);
            _documentRevisionXPO.Status = (_documentRevisionXPO.Status != null && _documentRevisionXPO.Status.Oid == DocumentRevisionDTO.StatusID) ? _documentRevisionXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(DocumentRevisionDTO.StatusID);
            _documentRevisionXPO.AddedDate = _documentRevisionXPO.AddedDate != null ? _documentRevisionXPO.AddedDate : DocumentRevisionDTO.AddedDate;
            _documentRevisionXPO.AddedBy = (_documentRevisionXPO.AddedBy != null) ? _documentRevisionXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentRevisionDTO.AddedByID);
            _documentRevisionXPO.LastUpdate = _documentRevisionXPO.LastUpdate == DocumentRevisionDTO.LastUpdate ? _documentRevisionXPO.LastUpdate : DocumentRevisionDTO.LastUpdate;
            _documentRevisionXPO.LastUpdateBy = (_documentRevisionXPO.LastUpdateBy != null && _documentRevisionXPO.LastUpdateBy.Oid == DocumentRevisionDTO.LastUpdateByID) ? _documentRevisionXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentRevisionDTO.LastUpdateByID);            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentRevisionXPO;
    }
    public static List<DocumentRevisionDTO> XPCollectionToList(XPCollection<DocumentRevisionXPO> DecoderStructureXPCollection)
    {
        var _documentRevisionList = new List<DocumentRevisionDTO>();
        try
        {
            foreach (var _decoderStructureXPO in DecoderStructureXPCollection)
            {
                _documentRevisionList.Add(XPOToDTO(_decoderStructureXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentRevisionList;
    }
    public static List<DocumentRevisionXPO> DTOListToXPOList(List<DocumentRevisionDTO> DecoderStructureList, UnitOfWork UnitOfWork)
    {
        var _documentRevisionXPOList = new List<DocumentRevisionXPO>();
        try
        {
            foreach (var _decoderStructureDTO in DecoderStructureList)
            {
                _documentRevisionXPOList.Add(DTOtoXPO(_decoderStructureDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentRevisionXPOList;
    }
}
