using Microsoft.AspNetCore.Mvc;
using WebLab.Extensions;
using WebLab.Models;

namespace WebLab.Components
{
	public class CartViewComponent : ViewComponent {
		private Cart _cart;
		public CartViewComponent(Cart cart) {
			_cart = cart;
		}

		public IViewComponentResult Invoke() {
			//var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
			return View(_cart);
		}
	}
}
