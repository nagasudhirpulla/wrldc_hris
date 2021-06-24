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

namespace WrldcHrIs.Application.Departments.Commands.SeedDepartments
{
    public class SeedDepartmentsCommand : IRequest<bool>
    {
        public class SeedDepartmentsCommandHandler : IRequestHandler<SeedDepartmentsCommand, bool>
        {
            private readonly IAppDbContext _context;

            public SeedDepartmentsCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(SeedDepartmentsCommand request, CancellationToken cancellationToken)
            {
                List<string> seedDepts = new() { "NA" };
                foreach (var dept in seedDepts)
                {
                    bool isDeptPres = await _context.Departments.AnyAsync(d => d.Name.ToLower().Equals(dept.ToLower()), cancellationToken: cancellationToken);
                    if (!isDeptPres)
                    {
                        _context.Departments.Add(new Department() { Name = dept });
                        _ = await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                return true;
            }
        }
    }
}
