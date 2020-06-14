using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public EditModel(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        [BindProperty]
        public Military Military { get; set; }
        [BindProperty]
        public IFormFile image { get; set; }

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
           ViewData["MilitaryGroupId"] = new SelectList(_context.MilitaryGroups, "MilitaryGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string path = "";
            // предыдущее изображение
            string previousImage = String.IsNullOrEmpty(Military.Image) ? "" : Military.Image;
            if (image != null) {
                // новый файл изображения
                Military.Image = Military.MilitaryId + Path.GetExtension(image.FileName);
                // путь для нового файла изображения
                path = Path.Combine(_environment.WebRootPath, "images", Military.Image);
            }

            _context.Attach(Military).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                if (image != null) {
                    // если было предыдущее изображение 
                    if (!String.IsNullOrEmpty(previousImage)) {
                        // если файл существует
                        var fileInfo = _environment.WebRootFileProvider.GetFileInfo("/Images/" + previousImage);
                        if (fileInfo.Exists) {
                            var oldPath = Path.Combine(_environment.WebRootPath, "images", previousImage);
                            // удалить предыдущее изображение
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var stream = new FileStream(path, FileMode.Create)) {
                        // сохранить новое изображение
                        await image.CopyToAsync(stream);
                    };
                }
            } catch (DbUpdateConcurrencyException) {
                if (!MilitaryExists(Military.MilitaryId)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MilitaryExists(int id)
        {
            return _context.Militaries.Any(e => e.MilitaryId == id);
        }
    }
}
