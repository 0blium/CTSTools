using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentType;

public class DocumentType_DXFilter
{
    public static GroupOperator GetDocumentType_DXFilter(DocumentTypeDTO DocumentTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DocumentTypeDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentTypeXPO.Oid), DocumentTypeDTO.ID));
            if (DocumentTypeDTO.DocumentTypeIDArray != null && DocumentTypeDTO.DocumentTypeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentTypeXPO.Oid), DocumentTypeDTO.DocumentTypeIDArray));
            if (DocumentTypeDTO.AddedByID != null && DocumentTypeDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentTypeXPO.AddedBy), DocumentTypeDTO.AddedByID));
            if (DocumentTypeDTO.LastUpdateByID != null && DocumentTypeDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentTypeXPO.LastUpdateBy), DocumentTypeDTO.LastUpdateByID));
            if (DocumentTypeDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentTypeXPO.IsActive), DocumentTypeDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
