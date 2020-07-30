using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Practice.Interfaces.Models
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ProfileDescription { get; set; }
        int UserReputationRating { get; set; } // Reputation level of a user for posting fair and accurate reviews
        string CityOfResidence { get; set; }
        string CountryOfResidence { get; set; }
        byte[] UserProfileImage { get; set; }
        int TotalReviews { get; set; }
        int TotalActiveReviews { get; set; } // Reviews that are visible and not deleted
    }
}
