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
    public class DetailsModel : PageModel
    {
        private readonly IAppDbContext _context;

        public DetailsModel(IAppDbContext context)
        {
            _context = context;
        }

        public FoodItem FoodItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FoodItem = await _context.FoodItems.FirstOrDefaultAsync(m => m.Id == id);

            if (FoodItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
