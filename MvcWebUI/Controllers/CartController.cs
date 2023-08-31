using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Helpers;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartservice;
        private ICartSessionHelper _cartSessionhelper;
        private IProductService _productservice;

        public CartController(ICartService cartservice, ICartSessionHelper cartSessionhelper, IProductService productservice)
        {
            _cartservice = cartservice;
            _cartSessionhelper = cartSessionhelper;
            _productservice = productservice;
        }
        public IActionResult AddToCart(int productId) 
        {
            //ürünü çek
            Product product = _productservice.GetById(productId);

            var cart = _cartSessionhelper.GetCart("cart");

            _cartservice.AddToCart(cart,product);

            _cartSessionhelper.SetCart("cart",cart);

            TempData.Add("message",product.ProductName+ " Added your cart");

            return RedirectToAction("Index","Product");
        }

        public IActionResult Index()
        {
            var model = new CartListViewModel
            {
                Cart = _cartSessionhelper.GetCart("cart"),
            };
            return View(model);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            //ürünü çek
            Product product = _productservice.GetById(productId);
            var cart = _cartSessionhelper.GetCart("cart");
            _cartservice.RemoverFromCart(cart, productId);
             _cartSessionhelper.SetCart("cart",cart);
            TempData.Add("message", product.ProductName + " Deleted from your cart");
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Complete()
        {
            var model = new ShippingDetailsViewModel
            {
                ShippingDetail = new ShippingDetail(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetail shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", "Your Order is Completed Successfully");
            _cartSessionhelper.Clear();
            return RedirectToAction("Index", "Cart");
        }

    }
}
