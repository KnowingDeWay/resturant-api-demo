using Web_API_Practice.Interfaces.Models;

namespace Web_API_Practice.Models
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileDescription { get; set; }
        public int UserReputationRating { get; set; } // Reputation level of a user for posting fair and accurate reviews
        public string CityOfResidence { get; set; }
        public string CountryOfResidence { get; set; }
        public byte[] UserProfileImage { get; set; }
        public int TotalReviews { get; set; }
        public int TotalActiveReviews { get; set; } // Reviews that are visible and not deleted
    }
}
