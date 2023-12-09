using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.ViewModel
{
    public class JsonReceipt
    {
        public string HeadInfo { get; set; }
        public string ContentInfo { get; set; }
        public string TotalInfo { get; set; }
        public string PaymmentInfo { get; set; }
        public string PointInfo { get; set; }
    }

    public class GetBillPreviewV2
    {
        public string BillPreview { get; set; }
        public string Trn { get; set; }
        public string Spos { get; set; }
    }
}
