using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<FoodItem> FoodItems { get; set; }
        DbSet<CanteenOrder> CanteenOrders { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry Attach([NotNullAttribute] object entity);
    }
}
