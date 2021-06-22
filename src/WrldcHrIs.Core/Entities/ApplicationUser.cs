using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrldcHrIs.Core.Common;

namespace WrldcHrIs.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string OfficeId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
