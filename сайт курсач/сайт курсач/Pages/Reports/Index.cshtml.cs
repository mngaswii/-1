using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;

namespace сайт_курсач.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int ClientsCount { get; set; }

        public int MastersCount { get; set; }

        public int AppointmentsCount { get; set; }

        public int PaymentsCount { get; set; }

        public decimal TotalIncome { get; set; }

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

            ClientsCount = _context.Clients.Count();

            MastersCount = _context.Masters.Count();

            AppointmentsCount = _context.Appointments.Count();

            PaymentsCount = _context.Payments.Count();

            TotalIncome = _context.Payments.Sum(p => p.Amount);

            return Page();
        }
    }
}