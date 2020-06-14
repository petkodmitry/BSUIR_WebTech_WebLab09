﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLab.DAL.Data;
using WebLab.Extensions;
using WebLab.Models;

namespace WebLab.Controllers
{
    public class CartController : Controller {
        private ApplicationDbContext _context;
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart) {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index() {
            //_cart = HttpContext.Session.Get<Cart>("cart");
            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl) {
            //_cart = HttpContext.Session.Get<Cart>("cart");
            var item = _context.Militaries.Find(id);
            if (item != null) {
                _cart.AddToCart(item);
                //HttpContext.Session.Set<Cart>("cart", _cart);
            }
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int id) {
            //_cart = HttpContext.Session.Get<Cart>("cart"); 
            _cart.RemoveFromCart(id);
            //HttpContext.Session.Set<Cart>("cart", _cart); 
            return RedirectToAction("Index");
        }
    }
}