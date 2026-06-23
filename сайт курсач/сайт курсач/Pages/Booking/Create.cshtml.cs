using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Booking
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public int ServiceId { get; set; }

        [BindProperty]
        public int MasterId { get; set; }

        [BindProperty]
        public DateTime AppointmentDate { get; set; }

        public SelectList Services { get; set; }

        public SelectList Masters { get; set; }

        public void OnGet()
        {
            LoadLists();
        }

        public IActionResult OnPost()
        {
            LoadLists();

            // Проверка занятости мастера
            var existingAppointment = _context.Appointments
                .FirstOrDefault(a =>
                    a.MasterId == MasterId &&
                    a.AppointmentDate == AppointmentDate);

            if (existingAppointment != null)
            {
                TempData["Error"] =
                    "На это время мастер уже занят. Выберите другое время.";

                return Page();
            }

            var client = new Client
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            var appointment = new Appointment
            {
                ClientId = client.Id,
                MasterId = MasterId,
                ServiceId = ServiceId,
                AppointmentDate = AppointmentDate,
                Status = "Запланирована"
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToPage(
                "/Payments/FakePayment",
                new
                {
                    appointmentId = appointment.Id
                });
        }

        private void LoadLists()
        {
            Services = new SelectList(
                _context.Services.ToList(),
                "Id",
                "Name");

            Masters = new SelectList(
                _context.Masters.ToList(), //
                "Id",
                "FirstName");
        }
    }
}