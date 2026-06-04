using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Appointments
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList Clients { get; set; }

        public SelectList Masters { get; set; }

        public SelectList Services { get; set; }

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

            Clients = new SelectList(
                _context.Clients.ToList(),
                "Id",
                "FirstName");

            Masters = new SelectList(
                _context.Masters.ToList(),
                "Id",
                "FirstName");

            Services = new SelectList(
                _context.Services.ToList(),
                "Id",
                "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Appointments.Update(Appointment);

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}