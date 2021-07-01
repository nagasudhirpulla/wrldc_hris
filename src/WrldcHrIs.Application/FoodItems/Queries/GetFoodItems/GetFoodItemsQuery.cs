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

namespace WrldcHrIs.Application.FoodItems.Queries.GetFoodItems
{
    public class GetFoodItemsQuery : IRequest<List<FoodItem>>
    {
        public class GetFoodItemsQueryHandler : IRequestHandler<GetFoodItemsQuery, List<FoodItem>>
        {
            private readonly IAppDbContext _context;

            public GetFoodItemsQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<FoodItem>> Handle(GetFoodItemsQuery request, CancellationToken cancellationToken)
            {
                List<FoodItem> res = await _context.FoodItems.ToListAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
