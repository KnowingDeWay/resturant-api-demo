using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Practice.Interfaces.Models;

namespace Web_API_Practice.Interfaces.Controllers
{
    public interface IResturantController
    {
        bool AddResturant(string resturantName, string unitNumber, string buildingNumber,
            string streetName, string suburbName, string cityName, string stateName,
            string countryName, string postcode, string ownerName, string description,
            int resturantType, byte[] image = null);
        List<IResturant> GetResturants(int resultLimit = 30);
        List<IResturant> SearchResturant(string resturantName, int resultLimit = 30);
        bool UpdateResturantDetails(dynamic resturant);
        bool DeleteResturant(int resturantId);
    }
}
