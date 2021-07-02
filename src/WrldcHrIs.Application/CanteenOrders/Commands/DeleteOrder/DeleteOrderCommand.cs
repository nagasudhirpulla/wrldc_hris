using MediatR;
using System.Linq;
using System.Text;

namespace WrldcHrIs.Application.CanteenOrders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
    }
}
