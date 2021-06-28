using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.FoodItems
{
    public class IndexModel : PageModel
    {
        private readonly IAppDbContext _context;

        public IndexModel(IAppDbContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get;set; }

        public async Task OnGetAsync()
        {
            FoodItem = await _context.FoodItems.ToListAsync();
        }
    }
}
