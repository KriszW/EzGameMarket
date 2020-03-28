﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.Abstraction;

namespace WebMVC.ViewModels.Products
{
    public class ProductItem : IProduct
    {
        public string ImageUrl { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public string Category { get; set; }

    }
}