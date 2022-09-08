using System;
using System.Collections.Generic;

namespace WFM_DBFrstApprh.Models
{
    public partial class SkillMap
    {
        public int? Employeeid { get; set; }
        public decimal? Skillid { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
