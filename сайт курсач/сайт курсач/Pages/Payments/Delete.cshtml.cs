using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Payments
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Payment Payment { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Payment = _context.Payments.Find(id);

            if (Payment == null)
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

            var payment = _context.Payments.Find(id);

            if (payment != null)
            {
                _context.Payments.Remove(payment);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}