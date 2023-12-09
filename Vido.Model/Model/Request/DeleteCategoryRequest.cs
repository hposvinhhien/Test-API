using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class DeleteCategoryRequest
    {
        public long CategoryID { get; set; }
        public int RVCNo { get; set; }
    }
}
