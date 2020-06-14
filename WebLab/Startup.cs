using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebLab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebLab.Models;
using WebLab.Extensions;

namespace WebLab
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<ApplicationUser, IdentityRole>(opt => {
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireDigit = false;
			})
				//.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			//services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			//	.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddDistributedMemoryCache();
			services.AddSession(opt =>
			{
				opt.Cookie.HttpOnly = true;
				opt.Cookie.IsEssential = true;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<Cart>(sp => CartService.GetCart(sp));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ApplicationDbContext context
							  , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseSession();
			app.UseAuthorization();

			app.UseLogging();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
			DbInitializer.Seed(context, userManager, roleManager).GetAwaiter().GetResult();
		}
	}
}
