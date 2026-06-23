using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Payments
{
    public class FakePaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FakePaymentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int AppointmentId { get; set; }

        public decimal Amount { get; set; }

        public IActionResult OnGet(int appointmentId)
        {
            var appointment = _context.Appointments
                .Include(a => a.Service)
                .FirstOrDefault(a => a.Id == appointmentId);

            if (appointment == null)
            {
                return RedirectToPage("/Index");
            }

            AppointmentId = appointmentId;
            Amount = appointment.Service.Price;

            return Page();
        }

        public IActionResult OnPost()
        {
            var appointment = _context.Appointments
                .Include(a => a.Service)
                .FirstOrDefault(a => a.Id == AppointmentId);

            if (appointment == null)
            {
                return RedirectToPage("/Index");
            }

            var payment = new Payment
            {
                AppointmentId = AppointmentId,
                Amount = appointment.Service.Price,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return RedirectToPage("/Payments/Success");
        }
    }
}                           