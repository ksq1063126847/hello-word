using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved",product.Name);              
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int ProductID)
        {
            var deleteProduct = repository.DeleteProduct(ProductID);
            if (deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} 已删除", deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}