﻿using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repository, IOrderProcessor orderProcessor)
        {
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }
        public RedirectToRouteResult AddToCard(Cart cart, int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails )
        {
            if (cart.Lines().Count() == 0)
            {
                ModelState.AddModelError("", "你的购物车是空的");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}