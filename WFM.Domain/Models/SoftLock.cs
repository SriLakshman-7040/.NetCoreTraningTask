using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain.Models
{
    public class SoftLock
    {
        [Key]
        public int LockID { get; set; }
        public int EmployeeID { get; set; }
        public string Manager { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public string RequestMessage { get; set; }
        public string WfmRemark { get; set; }
        public string ManagerStatus { get; set; }
        public string ManagerStatusComment { get; set; }
        public DateTime ManagerLastUpdate { get; set; }
    }
}
