using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment = _context.Appointments.Find(id);

            if (Appointment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _context.Appointments.Find(id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}