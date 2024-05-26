using ST10263992.Models;
using Microsoft.AspNetCore.Mvc;

namespace ST10263992.Controllers
{
    public class ProductController : Controller
    {
        public ProductTable productTable = new ProductTable();

        [HttpPost]
        public ActionResult MyWork(ProductTable products)
        {
            var result2 = productTable.InsertProduct();
            return RedirectToAction("Index", "Home");
        }
 
        [HttpGet]
        public ActionResult MyWork()
        {
            ViewData["Products"] = ProductTable.GetAllProducts();
            return View(productTable);
        }
    }
}
