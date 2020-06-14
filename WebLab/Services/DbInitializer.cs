using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Services
{
	public class DbInitializer {
		public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
			// создать БД, если она еще не создана
			context.Database.EnsureCreated();
			// проверка наличия ролей
			if(!context.Roles.Any()) {
				var roleAdmin = new IdentityRole {
					Name = "admin", NormalizedName = "admin"
				};
				// создать роль manager
				var result = await roleManager.CreateAsync(roleAdmin);
			}
			
			// проверка наличия пользователей
			if(!context.Users.Any()) {
				// создать пользователя user@mail.ru
				var user = new ApplicationUser {
					Email = "user@mail.ru", UserName = "user@mail.ru"
				};
				await userManager.CreateAsync(user, "123456");
				// создать пользователя admin@mail.ru
				var admin = new ApplicationUser {
					Email = "admin@mail.ru", UserName = "admin@mail.ru"
				};
				await userManager.CreateAsync(admin, "123456");
				// назначить роль admin
				admin = await userManager.FindByEmailAsync("admin@mail.ru");
				await userManager.AddToRoleAsync(admin, "admin");
			}

			//проверка наличия групп объектов
			if (!context.MilitaryGroups.Any()) {
				context.MilitaryGroups.AddRange(
				new List<MilitaryGroup> {
					new MilitaryGroup {GroupName="Артиллерия"},
					new MilitaryGroup {GroupName="Самолёты"},
					new MilitaryGroup {GroupName="Танки"}
				});
				await context.SaveChangesAsync();
			}

			// проверка наличия объектов 
			if (!context.Militaries.Any()) {
				context.Militaries.AddRange(
				new List<Military>{
					new Military { MilitaryName="Долговязый Том", Description="Американская пушка", Force=3
						, MilitaryGroupId=1, Image="art_longTom.jpg" },
					new Military { MilitaryName="Нора", Description="Бронированная машинка", Force=120
						, MilitaryGroupId=1, Image="art_nora.jpg" },
					new Military { MilitaryName="Пион", Description="Крутая гаубица", Force=250
						, MilitaryGroupId=1, Image="art_pion.jpg" },
					new Military { MilitaryName="Смерч", Description="Катюша new", Force=500
						, MilitaryGroupId=1, Image="art_smerch.jpg" },

					new Military { MilitaryName="Су-27", Description="Выше всяких похвал", Force=10500
						, MilitaryGroupId=2, Image="air_su27.jpg" },
					new Military { MilitaryName="Hawker Siddeley Harrier", Description="Английский, вертикальный, 1-й в своём роде", Force=770
						, MilitaryGroupId=2, Image="air_harrier.jpg" },
					new Military { MilitaryName="F-15", Description="Стабильность", Force=330
						, MilitaryGroupId=2, Image="air_f15.jpg" },
					new Military { MilitaryName="F-22", Description="Мишура", Force=13
						, MilitaryGroupId=2, Image="air_f22.jpg" },

					new Military { MilitaryName="Армата", Description="4-е поколение. Лучше не попадаться на глаза", Force=1000
						, MilitaryGroupId=3, Image="tank_armata.jpg" },
					new Military { MilitaryName="Меркава", Description="Против арам-зам-зам", Force=500
						, MilitaryGroupId=3, Image="tank_merkava.jpg" },
					new Military { MilitaryName="Леопард-2", Description="Немецкое качество", Force=800
						, MilitaryGroupId=3, Image="tank_leopard2.jpg" },
					new Military { MilitaryName="Абрамс", Description="Старьё", Force=33
						, MilitaryGroupId=3, Image="tank_abrams.jpg" }
				});
				await context.SaveChangesAsync();
			}
		}
	}
}
