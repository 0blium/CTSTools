using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class Document_DXFilter
{
    public static GroupOperator GetDocument_DXFilter(DocumentDTO DocumentDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DocumentDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Oid), DocumentDTO.ID));
            }
            if (DocumentDTO.DocumentIDArray != null && DocumentDTO.DocumentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentXPO.Oid), DocumentDTO.DocumentIDArray));
            }
            if (DocumentDTO.OwnerID != null || DocumentDTO.OwnerID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Owner), DocumentDTO.OwnerID));
            }
            if (DocumentDTO.DepartmentID != null || DocumentDTO.DepartmentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Department), DocumentDTO.DepartmentID));
            }
            if (DocumentDTO.DepartmentIDArray != null && DocumentDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentXPO.Department), DocumentDTO.DepartmentIDArray));
            }
            if (DocumentDTO.TypeID != null || DocumentDTO.TypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Type), DocumentDTO.TypeID));
            }
            if (DocumentDTO.TypeIDArray != null && DocumentDTO.TypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentXPO.Type), DocumentDTO.TypeIDArray));
            }
            if (DocumentDTO.CustomerID != null || DocumentDTO.CustomerID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Customer), DocumentDTO.CustomerID));
            }
            if (DocumentDTO.CustomerIDArray != null && DocumentDTO.CustomerIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentXPO.Customer), DocumentDTO.CustomerIDArray));
            }
            if (DocumentDTO.StatusID != null || DocumentDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.Status), DocumentDTO.StatusID));
            }
            if (DocumentDTO.StatusIDArray != null && DocumentDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DocumentXPO.Status), DocumentDTO.StatusIDArray));
            }
            if (DocumentDTO.AddedByID != null && DocumentDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.AddedBy), DocumentDTO.AddedByID));
            }
            if (DocumentDTO.LastUpdateByID != null && DocumentDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DocumentXPO.LastUpdateBy), DocumentDTO.LastUpdateByID));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
