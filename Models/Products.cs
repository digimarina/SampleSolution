using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockAvailable { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
    }
}