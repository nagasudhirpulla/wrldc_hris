using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WrldcHrIs.Core.Entities;
using WrldcHrIs.Infra.Persistence;

namespace WrldcHrIs.WebApp.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly WrldcHrIs.Infra.Persistence.AppDbContext _context;

        public DetailsModel(WrldcHrIs.Infra.Persistence.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
