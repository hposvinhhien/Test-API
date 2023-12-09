using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Proc
{
    public class LoadEmpRsp
    {
        public decimal? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Phone { get; set; }
        public string ImageFileName { get; set; }
        public string EmployeeImage { get; set; }
        public int? RVCNo { get; set; }

    }

    public class ResultNoShow
    {
        public int Status { get; set; }
        public string Message { get; set; }

    }
}
