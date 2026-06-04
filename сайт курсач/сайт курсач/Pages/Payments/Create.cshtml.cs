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

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Payment.PaymentDate = DateTime.Now;

            _context.Payments.Add(Payment);

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}