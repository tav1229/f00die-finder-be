﻿namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantUpdateImagesDto
    {
        public Guid Id { get; set; }
        public List<IFormFile>? RestaurantImages { get; set; }
        public List<IFormFile>? MenuImages { get; set; }

    }
}
