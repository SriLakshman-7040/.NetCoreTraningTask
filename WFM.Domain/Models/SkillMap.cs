using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain.Models
{
    public class SkillMap
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public decimal SkillID { get; set; }
    }
}
