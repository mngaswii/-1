using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Payments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Payment Payment { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToPage("/Index");
            }

            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == Payment.AppointmentId);

            if (appointment == null)
            {
                ModelState.AddModelError("", "Запись с таким ID не найдена.");

                return Page();
            }

            Payment.PaymentDate = DateTime.Now;

            _context.Payments.Add(Payment);

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}