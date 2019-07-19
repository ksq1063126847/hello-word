using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class NavController : Controller
    {
        IProductRepository repository;
        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public PartialViewResult Menu(string category=null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products.Select(p => p.Category).Distinct().OrderBy(p => p);
            return PartialView(categories);
        }
    }
}