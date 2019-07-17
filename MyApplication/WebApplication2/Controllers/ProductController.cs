﻿using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {

        IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository repository )
        {
            this.repository = repository;
        }

        public ViewResult List(int page=1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            model.Products = repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
            model.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = repository.Products.Count() };
            return View(model);
        }

    }
}