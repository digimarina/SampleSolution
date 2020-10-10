using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SampleProject.ModelManager
{
    public class ProductManager
    {
        public int save(Products products)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //Add
            if (products.ProductID == 0)
            {
                db.Products.Add(products);
                db.SaveChanges();
            }
            //Update
            else
            {
                db.Set<Products>().AddOrUpdate(products);
                db.SaveChanges();
            }
            //}
            return products.ProductID;
        }

        public void remove(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Products products = new Products();
            if (products != null)
            {
                products.IsDeleted = true;
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}