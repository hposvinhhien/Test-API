using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class EditCategoryRequest
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public int RVCNo { get; set; }
        public string ImageData { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public string Color { get; set; }
        public string ImageBase64 { get; set; }
    }
}
