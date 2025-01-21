using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevision_DXFilter
{
    public static GroupOperator GetDocumentRevision_DXFilter(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DocumentRevisionDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentRevisionXPO.Oid), DocumentRevisionDTO.ID));
            }
            if (DocumentRevisionDTO.DocumentRevisionIDArray != null && DocumentRevisionDTO.DocumentRevisionIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentRevisionXPO.Oid), DocumentRevisionDTO.DocumentRevisionIDArray));
            }
            if (DocumentRevisionDTO.DocumentID != null || DocumentRevisionDTO.DocumentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentRevisionXPO.Document), DocumentRevisionDTO.DocumentID));
            }
            if (DocumentRevisionDTO.DocumentIDArray != null && DocumentRevisionDTO.DocumentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentRevisionXPO.Document), DocumentRevisionDTO.DocumentIDArray));
            }
            if (DocumentRevisionDTO.StatusID != null || DocumentRevisionDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentRevisionXPO.Status), DocumentRevisionDTO.StatusID));
            }
            if (DocumentRevisionDTO.StatusIDArray != null && DocumentRevisionDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentRevisionXPO.Status), DocumentRevisionDTO.StatusIDArray));
            }
            if (DocumentRevisionDTO.AddedByID != null && DocumentRevisionDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentRevisionXPO.AddedBy), DocumentRevisionDTO.AddedByID));
            }
            if (DocumentRevisionDTO.LastUpdateByID != null && DocumentRevisionDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentRevisionXPO.LastUpdateBy), DocumentRevisionDTO.LastUpdateByID));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
