using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("RDPara")]
    public class RDPara
    {
        [Key, Column(Order = 0)]
        public string ParaName { get; set; }

        [Key, Column(Order = 1)]
        public int RVCNo { get; set; }

        public string ParaStr { get; set; }
        public Nullable<int> ParaType { get; set; }
        public string ParaDesc { get; set; }
        public long nSeq { get; set; }
        public Nullable<long> Status { get; set; }
        public string GroupName { get; set; }
        public int? OrderNum { get; set; }
        public string TabName { get; set; }

        public string ParentName { get; set; }
        public bool? IsHide { get; set; }
    }

}
