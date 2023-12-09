using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Request
{
    public class FindAndRegCustomerModel
    {
        public FindAndRegCustomerModel()
        {
            phone = "";
            firstName = "";
            lastName = "";
        }
        public string date { get; set; }
        public decimal empId { get; set; }
        public bool gender { get; set; }
        [Required(ErrorMessage = "Firs Name is required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }
        //[Required(ErrorMessage = "Phone is required")]
        public string phone { get; set; }
        public string sex { get; set; }
        public string portalCode { get; set; }
        public bool isKid { get; set; }
        public int? rvcNo { get; set; }
    }
}
