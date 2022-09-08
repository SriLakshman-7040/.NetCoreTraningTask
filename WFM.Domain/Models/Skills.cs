using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain.Models
{
    public class Skills
    {
        [Key]
        public decimal SkillID { get; set; }
        public string SkillName { get; set; }
    }
}
