using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrldcHrIs.Core.Entities;
using WrldcHrIs.Infra.Persistence;

namespace WrldcHrIs.WebApp.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly WrldcHrIs.Infra.Persistence.AppDbContext _context;

        public CreateModel(WrldcHrIs.Infra.Persistence.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
