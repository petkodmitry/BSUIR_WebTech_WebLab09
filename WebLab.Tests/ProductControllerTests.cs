using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebLab.Controllers;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;
using WebLab.Models;
using Xunit;

namespace WebLab.Tests
{
	public class ProductControllerTests {
		[Theory]
		[MemberData(nameof(Data))]
		public void ControllerGetsProperPage(int page, int qty, int id) {
			// Arrange  
			// объекта класса ControllerContext
			var controllerContex = new ControllerContext();
			// объект для HttpContext 
			var httpContext = new DefaultHttpContext();
			httpContext.Request.Headers.Add("x-requested-with", "");
			// поместить HttpContext в ControllerContext
			controllerContex.HttpContext = httpContext;

			// настройка для сонтекста базы данных
			var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
			// создать контекст
			using (var context = new ApplicationDbContext(options)) {
				// заполнить контекст данными
				context.Militaries.AddRange(
				new List<Military> {
					new Military{ MilitaryId=71 },
					new Military{ MilitaryId=72 },
					new Military{ MilitaryId=73 },
					new Military{ MilitaryId=74 },
					new Military{ MilitaryId=75 },
					new Military{ MilitaryId=76 }
				});
				context.MilitaryGroups.Add(new MilitaryGroup { GroupName = "fake group" });
				context.SaveChanges();
				// создать объект контроллера
				var controller = new ProductController(context/*, null*/) {
					ControllerContext = controllerContex
				};

				// Act
				var result = controller.Index(pageNo: page, group: null) as ViewResult;
				var model = result?.Model as List<Military>;

				// Assert
				Assert.NotNull(model);
				Assert.Equal(qty, model.Count);
				Assert.Equal(70 + id, model[0].MilitaryId);
			}
			using (var context = new ApplicationDbContext(options)) {
				context.Database.EnsureDeleted();
			}
		}

        /// <summary> 
        /// Исходные данные для теста 
        /// номер страницы, кол.объектов на выбранной странице и 
        /// id первого объекта на странице 
        /// </summary> 
        /// <returns></returns> 
        public static IEnumerable<object[]> Data() {
            yield return new object[] { 1, 3, 1 };  // 1-я страница, кол. объектов 3, id первого объекта 1
			yield return new object[] { 2, 3, 4 };  // 2-я страница, кол. объектов 3, id первого объекта 4
		}

        /// <summary> 
        /// Получение тестового списка объектов 
        /// </summary> 
        /// <returns></returns> 
        private List<Military> GetMilitariesList() {
            return new List<Military> {
				new Military{ MilitaryId=1, MilitaryGroupId=1 },
				new Military{ MilitaryId=2, MilitaryGroupId=1 },
				new Military{ MilitaryId=3, MilitaryGroupId=1 },
				new Military{ MilitaryId=4, MilitaryGroupId=1 },
				new Military{ MilitaryId=5, MilitaryGroupId=2 },
				new Military{ MilitaryId=6, MilitaryGroupId=2 },
				new Military{ MilitaryId=7, MilitaryGroupId=2 },
				new Military{ MilitaryId=8, MilitaryGroupId=2 },
				new Military{ MilitaryId=9, MilitaryGroupId=3 },
				new Military{ MilitaryId=10, MilitaryGroupId=3 },
				new Military{ MilitaryId=11, MilitaryGroupId=3 },
				new Military{ MilitaryId=12, MilitaryGroupId=3 }
			};
        }

		[Theory]
		[MemberData(memberName: nameof(Data))]
		public void ListViewModelCountsPages(int page, int qty, int id) {
			// Act 
			var model = ListViewModel<Military>.GetModel(GetMilitariesList(), page,
			3);

			// Assert
			Assert.Equal(4, model.TotalPages);
		}

		[Theory]
		[MemberData(memberName: nameof(Data))]
		public void ListViewModelSelectsCorrectQty(int page, int qty, int id) {
			// Act 
			var model = ListViewModel<Military>.GetModel(GetMilitariesList(), page,
			3);

			// Assert 
			Assert.Equal(qty, model.Count);
		}

		[Theory]
		[MemberData(memberName: nameof(Data))]
		public void ListViewModelHasCorrectData(int page, int qty, int id) {
			// Act 
			var model = ListViewModel<Military>.GetModel(GetMilitariesList(), page,
			3);

			// Assert 
			Assert.Equal(id, model[0].MilitaryId);
		}

		[Fact]
		public void ControllerSelectsGroup() {
			/*var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
			var context = new ApplicationDbContext(options);
			// arrange
			var controller = new ProductController(context);
			controller._militaries = GetMilitariesList();
			// act
			var result = controller.Index(1) as ViewResult;
			var model = result.Model as List<Military>;
			// assert
			Assert.Equal(3, model.Count);
			Assert.Equal(GetMilitariesList()[0], model[0], Comparer<Military>
				.GetComparer((d1, d2) => {
					return d1.MilitaryId == d2.MilitaryId;
				})
				);*/
		}
	}
}
