using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {

        IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            model.Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
            model.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count() };
            model.CurrentCategory = category;
            return View(model);
        }

        //异步action,适用于耗时，低cpu活动（非cup密集型）
        public async Task<ActionResult> AsyncList(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            model.Products = await Task<IEnumerable<Product>>.Factory.StartNew(
                () => {
                    Thread.Sleep(2000);//模拟长时间等待，但并不耗费cup，使用异步action释放工作线程，使其在等待期间去响应其他请求
                    return repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
                });         
            model.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count() };
            model.CurrentCategory = category;
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public PartialViewResult Count()
        {
            return PartialView(repository.Products.Count());
        }

        //
        public HttpStatusCodeResult StatusCode()
        {
            
            return new HttpStatusCodeResult(404, "页面没找到");
            //1.更加便捷的404
            //return HttpNotFound();
            //2.表示未经授权的 HTTP 请求的结果。401
            //return new HttpUnauthorizedResult();
        }
    }
}