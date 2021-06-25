using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Core.Entities;
using WrldcHrIs.Infra.Persistence;

namespace WrldcHrIs.WebApp.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly IAppDbContext _context;

        public DeleteModel(IAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
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

            Department = await _context.Departments.FindAsync(id);

            if (Department != null)
            {
                _context.Departments.Remove(Department);
                await _context.SaveChangesAsync(new CancellationToken());
            }

            return RedirectToPage("./Index");
        }
    }
}
