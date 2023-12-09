using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class CancelTicketReq
    {
        public string reasons { get; set; }
        public decimal AppointmentID { get; set; }
        public decimal CheckNo { get; set; }
        public decimal CancelBy { get; set; }
    }
}
