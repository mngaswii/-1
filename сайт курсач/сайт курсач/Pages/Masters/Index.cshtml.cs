using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;

namespace сайт_курсач.Pages.Masters
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Master> Masters { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Masters = await _context.Masters.ToListAsync();
        }
    }
}