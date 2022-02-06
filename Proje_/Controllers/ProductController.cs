using Proje_.Dal;
using Proje_.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Proje_.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductDal _productDal = new ProductDal();

    
        public ActionResult Index()
        {
            return View();
        }

      
        public JsonResult UpdateData(Product_ product)
        {
            bool IsInserted;

            IsInserted = _productDal.UpdateProduct(product);
            return Json(IsInserted, JsonRequestBehavior.AllowGet);
        }
   

        public JsonResult AddProduct(Product_ product)

        {
            bool isInserted = _productDal.InsertProduct(product);
            return Json(isInserted, JsonRequestBehavior.AllowGet);
        }

       

        public JsonResult GetAllData()

        {
            var listrs = _productDal.GetAll();

            return Json(listrs, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetDataById(int id)

        {

            var product = _productDal.GetById(id);

            return Json(product, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SearchData(string name)

        {
            List<Product_> productList = _productDal.SearchProduct(name);
            return Json(productList, JsonRequestBehavior.AllowGet);
        }



    }
}