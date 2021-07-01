using System;
using WrldcHrIs.Core.Common;

namespace WrldcHrIs.Core.Entities
{
    public class CanteenOrder : AuditableEntity
    {
        public DateTime OrderDate { get; set; }
        
        public int OrderQuantity { get; set; }
        
        public string FoodItemName { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
