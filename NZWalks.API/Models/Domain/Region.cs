﻿namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        //(? means this col accepts null values)
        public string? RegionImageUrl { get; set;}

    }
}
