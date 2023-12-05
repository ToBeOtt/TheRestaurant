﻿namespace TheRestaurant.Presentation.Client.Pages.Order.ProductModel
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsFoodItem { get; set; }
        public string Description { get; set; }
        public byte[] MenuPhoto { get; set; }
        public bool IsDeleted { get; set; }
    }

}
