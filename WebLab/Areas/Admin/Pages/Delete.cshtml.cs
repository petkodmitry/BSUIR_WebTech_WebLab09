using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebLab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Military Military { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Military = await _context.Militaries
                .Include(m => m.Group).FirstOrDefaultAsync(m => m.MilitaryId == id);

            if (Military == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Military = await _context.Militaries.FindAsync(id);

            if (Military != null)
            {
                _context.Militaries.Remove(Military);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
