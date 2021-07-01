using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrderById
{
    public class GetCanteenOrderByIdQuery : IRequest<CanteenOrder>
    {
        public int Id { get; set; }

        public class GetCanteenOrderByIdQueryHandler : IRequestHandler<GetCanteenOrderByIdQuery, CanteenOrder>
        {
            private readonly IAppDbContext _context;

            public GetCanteenOrderByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<CanteenOrder> Handle(GetCanteenOrderByIdQuery request, CancellationToken cancellationToken)
            {
                CanteenOrder res = await _context.CanteenOrders.Where(co => co.Id == request.Id)
                                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
