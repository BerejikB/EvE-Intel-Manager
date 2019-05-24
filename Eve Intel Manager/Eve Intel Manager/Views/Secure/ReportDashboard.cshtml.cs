using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eve_Intel_Manager.Views.Secure
{
    public class ReportDashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return new RedirectToPageResult("ReportDashboard");
        }
    }
}