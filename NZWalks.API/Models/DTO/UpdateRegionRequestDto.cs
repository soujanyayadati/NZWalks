﻿namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        //(? means this col accepts null values)
        public string? RegionImageUrl { get; set; }
    }
}
