using System;
using System.Collections.Generic;
using Web_API_Practice.Interfaces.Models;

namespace Web_API_Practice.Models
{
    public class ResturantReview : IResturantReview
    {
        public int ResturantReviewId { get; set; }
        public string ReviewText { get; set; }
        public float ReviewScore { get; set; }
        public byte[] ReviewImage { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Verified { get; set; }
        public int NumberOfUsersVerfied { get; set; } // Number of users that verified the review
        public User ReviewAuthor { get; set; }
        public List<ResturantReview> Replies { get; set; }
    }
}
