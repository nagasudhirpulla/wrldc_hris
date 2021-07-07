using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrldcHrIs.Application.CanteenOrders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<List<string>>
    {
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public int OrderQuantity { get; set; }

        public string FoodItemName { get; set; }

        public string FoodItemDescription { get; set; }

        public float FoodItemUnitPrice { get; set; }
    }
}
