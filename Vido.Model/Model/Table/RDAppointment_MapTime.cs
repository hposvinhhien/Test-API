using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public class RDAppointment_MapTime
    {
        public decimal AppointmentID { get; set; }
        public decimal CheckNo { get; set; }
        public DateTime? CreateTimeUTC { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? CheckInTimeUTC { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? StartTimeUTC { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? DoneTimeUTC { get; set; }
        public DateTime? DoneTime { get; set; }
        public DateTime? CloseTimeUTC { get; set; }
        public DateTime? CloseTime { get; set; }
        public int RVCno { get; set; }
    }
}
