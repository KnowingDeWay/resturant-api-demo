using System;
using System.Collections.Generic;
using Web_API_Practice.Enums;
using Web_API_Practice.Interfaces.Models;

namespace Web_API_Practice.Models
{
    public class Resturant : IResturant
    {
        public int ResturantId { get; set; }
        public string ResturantName { get; set; }
        public FormalAddress ResturantAddress { get; set; }
        public float ResturantStarRating { get; set; }
        public byte[] ResturantImage { get; set; }
        public string ResturantOwnerName { get; set; }
        public string ResturantDescription { get; set; }
        public ResturantType ResturantType { get; set; }
        public List<ResturantReview> ResturantReviews { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Equals(IResturant other)
        {
            return ResturantId == other.ResturantId && ResturantName.Equals(other.ResturantName)
                && ResturantAddress.Equals(other.ResturantAddress) && ResturantStarRating == other.ResturantStarRating
                && ResturantOwnerName.Equals(other.ResturantOwnerName) && ResturantDescription.Equals(other.ResturantDescription)
                && ResturantType == other.ResturantType && DateAdded.Equals(other.DateAdded);
        }

        public bool DuplicateEquals(IResturant other)
        {
            var resturantName = ResturantName ?? "";
            var resturantDesc = ResturantDescription ?? "";
            var address = ResturantAddress ?? new FormalAddress()
            {
                AddressApartmentNumber = null,
                AddressBuildingNumber = null,
                AddressCityName = null,
                AddressStreetName = null,
                AddressCountryName = null,
                AddressPostcode = null,
                AddressStateName = null,
                AddressSuburbName = null
            };
            var otherName = other.ResturantName ?? "";
            var otherDesc = other.ResturantDescription ?? "";
            var otherAddress = other.ResturantAddress ?? new FormalAddress() {
                AddressApartmentNumber = null,
                AddressBuildingNumber = null,
                AddressCityName = null,
                AddressStreetName = null,
                AddressCountryName = null,
                AddressPostcode = null,
                AddressStateName = null,
                AddressSuburbName = null
            };
            return resturantName.Equals(otherName) && address.Equals(otherAddress)
                && resturantDesc.Equals(otherDesc);
        }

        public static Resturant ConvertToEntity(IResturant resturant)
        {
            if(resturant != null)
            {
                Resturant newResturant = new Resturant()
                {
                    ResturantId = resturant.ResturantId,
                    ResturantName = resturant.ResturantName,
                    ResturantAddress = resturant.ResturantAddress,
                    ResturantDescription = resturant.ResturantDescription,
                    ResturantImage = resturant.ResturantImage,
                    ResturantOwnerName = resturant.ResturantOwnerName,
                    ResturantReviews = resturant.ResturantReviews,
                    ResturantStarRating = resturant.ResturantStarRating,
                    ResturantType = resturant.ResturantType
                };
                return newResturant;
            }
            else
            {
                return null;
            }
        }
    }
}
