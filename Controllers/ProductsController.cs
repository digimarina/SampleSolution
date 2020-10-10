using Newtonsoft.Json;
using SampleProject.ModelManager;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return View(db.Products.Where(x => !x.IsDeleted).ToList());
        }

        public ActionResult Add()
        {
            Products products = new Products();
            return View(products);
        }

        [HttpPost]
        public ActionResult Add(Products products, HttpPostedFileBase image)
        {
            ProductManager productManager = new ProductManager();
            if (image != null)
            {
                //get Unique Image Path
                string strNewPath = GetUniqueImagePath();
                string strDestPath = "Content\\images\\Product\\" + strNewPath;

                //Full path
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\Product\\" + strNewPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                    System.IO.Directory.CreateDirectory(strFullDestPath);

                products.ImagePath = strDestPath.Replace("\\", "/");
                products.ImageName = image.FileName;
                var strFullpath = strFullDestPath + "\\" + image.FileName;
                image.SaveAs(strFullpath);
            }

            int id = productManager.save(products);

            if (id != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = true;
                return View(products);
            }

        }

        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Products products = db.Products.FirstOrDefault(x => x.ProductID == id);
            return View(products);
        }

        [HttpPost]
        public ActionResult Edit(string formData, HttpPostedFileBase image)
        {
            ProductManager productManager = new ProductManager();
            Products products = JsonConvert.DeserializeObject<Products>(formData);

            if (image != null)
            {
                //get Unique Image Path
                string strNewPath = GetUniqueImagePath();
                string strDestPath = "Content\\images\\Product\\" + strNewPath;

                //Full path
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\Product\\" + strNewPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                    System.IO.Directory.CreateDirectory(strFullDestPath);

                products.ImagePath = strDestPath.Replace("\\", "/");
                products.ImageName = image.FileName;
                var strFullpath = strFullDestPath + "\\" + image.FileName;
                image.SaveAs(strFullpath);
            }
            try
            {
                int id = productManager.save(products);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            ProductManager productManager = new ProductManager();

            productManager.remove(id);

            return RedirectToAction("Index");
        }

        private string GetUniqueImagePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\Product";
            int iCount = 1;
            //### First we need to get the path
            while (System.IO.Directory.Exists(path + "\\ProductImages" + iCount) == true)
            {
                iCount++;
            }
            return "ProductImages" + iCount;
        }
    }
}