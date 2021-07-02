using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrldcHrIs.Core.Common;

namespace WrldcHrIs.Core.Entities
{
    public class FoodItem : AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
