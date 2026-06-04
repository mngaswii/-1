using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Payment> Payments { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            Payments = _context.Payments
                .Include(p => p.Appointment)
                .ToList();

            return Page();
        }
    }
}