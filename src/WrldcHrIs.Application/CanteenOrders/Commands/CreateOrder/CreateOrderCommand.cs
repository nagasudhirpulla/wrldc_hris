using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrldcHrIs.Application.CanteenOrders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<List<string>>
    {
        public DateTime OrderDate { get; set; }

        public int OrderQuantity { get; set; }

        public string FoodItemName { get; set; }

    }
}
