using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Practice.Enums;
using Web_API_Practice.Interfaces.Models;
using Web_API_Practice.Models;

namespace Web_API_Practice.Util
{
    public class UtilMethods
    {
        public UtilMethods()
        {

        }

        public IResturant ConvertJsonToResturant(dynamic json)
        {
            List<byte> imageBytes = new List<byte>();
            foreach(var singlebyte in json.ResturantImage)
            {
                byte currentByte = JsonConvert.DeserializeObject<byte>(singlebyte);
                imageBytes.Add(currentByte);
            }
            List<ResturantReview> reviews = new List<ResturantReview>();
            foreach(var review in json.ResturantReviews)
            {
                ResturantReview resReview = JsonConvert.DeserializeObject<ResturantReview>(review);
                reviews.Add(resReview);
            }
            IResturant resturant = new Resturant()
            {
                ResturantId = json.ResturantId,
                ResturantAddress = JsonConvert.DeserializeObject<FormalAddress>(json.FormalAddress),
                ResturantDescription = json.ResturantDescription,
                ResturantImage = imageBytes.ToArray(),
                ResturantName = json.ResturantName,
                ResturantOwnerName = json.ResturantOwnerName,
                ResturantReviews = reviews,
                ResturantStarRating = json.ResturantStarRating,
                ResturantType = Enum.Parse(typeof(ResturantType), json.ResturantType),
                DateAdded = DateTime.Parse(json.DateAdded)
            };
            return resturant;
        }
    }
}
