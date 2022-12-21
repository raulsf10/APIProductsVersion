﻿namespace APIProductsVersion.DTO
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public Rating rating { get; set; }


    }

    public class Rating
    {
        public float rate { get; set; }
        public int count { get; set; }
    }

    public class Productv2
    {
        public Guid InternalId { get; set; } = Guid.NewGuid();
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
    }

}
