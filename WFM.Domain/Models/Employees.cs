using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string status { get; set; }
        public string Manager { get; set; }
        public string Wfm_Manager { get; set; }
        public string Email { get; set; }
        public string LockStatus { get; set; }
        public decimal Experience { get; set; }
        public int ProfileID { get; set; }
    }
}
