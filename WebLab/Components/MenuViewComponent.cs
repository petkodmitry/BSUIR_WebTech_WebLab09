using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebLab.Models;

namespace WebLab.Components
{
	public class MenuViewComponent : ViewComponent
	{
		// Инициализация списка элементов меню 
		private List<MenuItem> _menuItems = new List<MenuItem> {
			new MenuItem{ Controller="Home", Action="Index", Text="Lab 9"},
			new MenuItem{ Controller="Product", Action="Index", Text="Каталог"},
			new MenuItem{ IsPage=true, Area="Admin", Page="/Index", Text="Администрирование"},
			new MenuItem{ IsPage=true, Area="ApiDemo", Page="/Index", Text="API-promo"}
		};

		public IViewComponentResult Invoke() {
			//Получение значений сегментов маршрута
			var controller = ViewContext.RouteData.Values["controller"];
			var page = ViewContext.RouteData.Values["page"];
			var area = ViewContext.RouteData.Values["area"];

			foreach (var item in _menuItems) {
				// Название контроллера совпадает? 
				var _matchController = controller?.Equals(item.Controller) ?? false;
				// Название зоны совпадает? 
				var _matchArea = area?.Equals(item.Area) ?? false;
				// Если есть совпадение, то сделать элемент меню активным  
				// (применить соответствующий класс CSS) 
				if (_matchController || _matchArea) {
					item.Active = "active";
				}
			}
			return View(_menuItems);
		}
	}
}
