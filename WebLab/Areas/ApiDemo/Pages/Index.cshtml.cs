using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Areas.ApiDemo.Pages
{
    public class IndexModel : PageModel
    {
        /*public void OnGet() {
            int k = 1;
        }*/



        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) {
            _context = context;
        }
        public IList<Military> Military { get; set; }
        public async Task OnGetAsync() {
            Military = await _context.Militaries.Include(m => m.Group).ToListAsync();
        }
    }
}