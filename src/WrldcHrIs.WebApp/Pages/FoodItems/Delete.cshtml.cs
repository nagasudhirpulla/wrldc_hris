using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.FoodItems
{
    [Authorize(Roles = SecurityConstants.AdminRoleString + "," + SecurityConstants.CanteenMgrRoleString)]
    public class DeleteModel : PageModel
    {
        private readonly IAppDbContext _context;

        public DeleteModel(IAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FoodItem = await _context.FoodItems.FindAsync(id);

            if (FoodItem != null)
            {
                _context.FoodItems.Remove(FoodItem);
                await _context.SaveChangesAsync(new CancellationToken());
            }

            return RedirectToPage("./Index");
        }
    }
}
