using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain_DbFrstApprh.Models
{
    public class Employee_DBF
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Manager { get; set; }
        public string Wfm_Manager { get; set; }
        public string Email { get; set; }
        public string LockStatus { get; set; }
        public decimal? Experience { get; set; }
        public int? Profile_ID { get; set; }

    }
}
