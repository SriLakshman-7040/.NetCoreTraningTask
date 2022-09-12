using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain_DbFrstApprh.Models
{
    public class SoftLock_DBF
    {
        public int? EmployeeId { get; set; }
        public string Manager { get; set; }
        public DateTime? ReqDate { get; set; }
        public string Status { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Key]
        public int LockId { get; set; }
        public string RequestMessage { get; set; }
        public string WfmRemark { get; set; }
        public string ManagerStatus { get; set; }
        public string ManagerStatusComment { get; set; }
        public DateTime? MgrLastUpdate { get; set; }
    }
}
