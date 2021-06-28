using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.FoodItems
{
    [Authorize(Roles = SecurityConstants.AdminRoleString + "," + SecurityConstants.CanteenMgrRoleString)]
    public class CreateModel : PageModel
    {
        private readonly IAppDbContext _context;

        public CreateModel(IAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync(new CancellationToken());

            return RedirectToPage("./Index");
        }
    }
}
