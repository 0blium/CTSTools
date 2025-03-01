using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.TransactionOrigin
{   
    public enum TransactionOrigin_Enum : int
    {
        Purchase_Order = 1,
        RMA = 2,
        NA = 3,
    }
}
