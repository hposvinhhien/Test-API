using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    [Table("EMP_EMPLOYEE")]
    public class Employee
    {
        [Key]
        public decimal EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Note { get; set; }

        public DateTime? StartDate { get; set; }
        public bool? IsWorking { get; set; }
        public string TouchID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool? IsTechnician { get; set; }
        public string ColorCode { get; set; }
        public string Title { get; set; }

        public string ImageFileName { get; set; }
        public string SizeImage { get; set; }
        public string AvatarFull { get; set; }
        public string AvatarCrop { get; set; }
        public string EmployeeName { get; set; }
        public bool? IsRating { get; set; }
        public bool? IsNotifyTech { get; set; }
        public bool? TakeAppointment { get; set; }
        public int? TechAppStatus { get; set; }
        public bool? AllowTechApp { get; set; }
    }
}
