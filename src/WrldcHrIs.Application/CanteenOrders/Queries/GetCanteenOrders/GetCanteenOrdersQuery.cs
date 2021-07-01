using FluentValidation;
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

namespace WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrders
{
    public class GetCanteenOrdersQuery : IRequest<List<CanteenOrder>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetCanteenOrdersQueryHandler : IRequestHandler<GetCanteenOrdersQuery, List<CanteenOrder>>
        {
            private readonly IAppDbContext _context;

            public GetCanteenOrdersQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<CanteenOrder>> Handle(GetCanteenOrdersQuery request, CancellationToken cancellationToken)
            {
                List<CanteenOrder> res = await _context.CanteenOrders
                                                .Where(co => co.OrderDate >= request.StartDate && co.OrderDate <= request.EndDate)
                                                .ToListAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
