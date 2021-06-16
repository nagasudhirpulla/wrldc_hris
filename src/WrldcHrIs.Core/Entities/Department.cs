using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrldcHrIs.Core.Common;

namespace WrldcHrIs.Core.Entities
{
    public class Department: AuditableEntity
    {
        [Required]
        public int Name { get; set; }
    }
}
