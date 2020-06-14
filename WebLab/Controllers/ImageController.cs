using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading.Tasks;
using WebLab.DAL.Entities;

namespace WebLab.Controllers
{
    public class ImageController : Controller {
        UserManager<ApplicationUser> _userManager;
        IHostingEnvironment _env;

        public ImageController(UserManager<ApplicationUser> mngr, IHostingEnvironment env) {
            _userManager = mngr;
            _env = env;
        }

        public async Task<IActionResult> GetAvatar() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return null;
            if (user.AvatarImage != null)
                return File(user.AvatarImage, user.ImageMimeType);
            else {
                var avatarPath = "/Images/avatar.jpg";
                var extProvider = new FileExtensionContentTypeProvider();
                var mimeType = extProvider.Mappings[".png"];
                return File(_env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), mimeType);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}