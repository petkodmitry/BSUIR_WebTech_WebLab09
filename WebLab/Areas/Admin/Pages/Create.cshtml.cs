using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public CreateModel(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["MilitaryGroupId"] = new SelectList(_context.MilitaryGroups, "MilitaryGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Military Military { get; set; }
        [BindProperty]
        public IFormFile image { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Militaries.Add(Military);
            await _context.SaveChangesAsync();

            if (image != null) {
                Military.Image = Military.MilitaryId + Path.GetExtension(image.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", Military.Image);
                using (var stream = new FileStream(path, FileMode.Create)) {
                    await image.CopyToAsync(stream);
                };
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
