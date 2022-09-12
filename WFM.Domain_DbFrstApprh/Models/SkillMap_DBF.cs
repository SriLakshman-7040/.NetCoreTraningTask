using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain_DbFrstApprh.Models
{ 
    public class SkillMap_DBF
    {
        public int? Employeeid { get; set; }
        public decimal? Skillid { get; set; }

        public Employee_DBF Employee { get; set; }
        public virtual Skill_DBF Skill { get; set; }
    }
}
