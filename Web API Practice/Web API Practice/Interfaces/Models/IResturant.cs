using System;
using System.Collections.Generic;
using Web_API_Practice.Enums;
using Web_API_Practice.Models;

namespace Web_API_Practice.Interfaces.Models
{
    public interface IResturant
    {
        int ResturantId { get; set; }
        string ResturantName { get; set; }
        FormalAddress ResturantAddress { get; set; }
        float ResturantStarRating { get; set; }
        byte[] ResturantImage { get; set; }
        string ResturantOwnerName { get; set; }
        string ResturantDescription { get; set; }
        ResturantType ResturantType { get; set; }
        List<ResturantReview> ResturantReviews { get; set; }
        DateTime DateAdded { get; set; }
    }
}
