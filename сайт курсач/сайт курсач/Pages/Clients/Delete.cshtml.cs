using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = _context.Clients.Find(id);

            if (Client == null)
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

            var client = _context.Clients.Find(id);

            if (client != null)
            {
                _context.Clients.Remove(client);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}