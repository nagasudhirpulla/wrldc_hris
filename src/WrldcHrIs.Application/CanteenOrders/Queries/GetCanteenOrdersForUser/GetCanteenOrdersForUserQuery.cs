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

namespace WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrdersForUserForUser
{
    public class GetCanteenOrdersForUserQuery : IRequest<List<CanteenOrder>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }

        public class GetCanteenOrdersForUserQueryHandler : IRequestHandler<GetCanteenOrdersForUserQuery, List<CanteenOrder>>
        {
            private readonly IAppDbContext _context;

            public GetCanteenOrdersForUserQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<CanteenOrder>> Handle(GetCanteenOrdersForUserQuery request, CancellationToken cancellationToken)
            {
                List<CanteenOrder> res = await _context.CanteenOrders
                                                .Where(co => co.OrderDate >= request.StartDate
                                                && co.OrderDate <= request.EndDate
                                                && co.CustomerId == request.UserId)
                                                .ToListAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
