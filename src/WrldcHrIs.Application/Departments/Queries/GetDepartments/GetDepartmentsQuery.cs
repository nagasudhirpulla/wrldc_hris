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

namespace WrldcHrIs.Application.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
        public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<Department>>
        {
            private readonly IAppDbContext _context;

            public GetDepartmentsQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
            {
                List<Department> res = await _context.Departments.ToListAsync();
                return res;
            }
        }
    }
}
