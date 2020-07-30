using System;
using System.Collections.Generic;
using Web_API_Practice.Models;

namespace Web_API_Practice.Interfaces.Models
{
    public interface IResturantReview
    {
        int ResturantReviewId { get; set; }
        string ReviewText { get; set; }
        float ReviewScore { get; set; }
        byte[] ReviewImage { get; set; }
        DateTime ReviewDate { get; set; }
        bool Verified { get; set; }
        int NumberOfUsersVerfied { get; set; } // Number of users that verified the review
        User ReviewAuthor { get; set; }
        List<ResturantReview> Replies { get; set; }
    }
}
