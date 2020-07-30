using Web_API_Practice.Interfaces.Models;

namespace Web_API_Practice.Models
{
    public class FormalAddress : IFormalAddress
    {
        public string AddressApartmentNumber { get; set; }
        public string AddressBuildingNumber { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressSuburbName { get; set; }
        public string AddressCityName { get; set; }
        public string AddressStateName { get; set; }
        public string AddressCountryName { get; set; }
        public string AddressPostcode { get; set; }

        public override string ToString()
        {
            var apartmentNumber = AddressApartmentNumber ?? "";
            var buildingNumber = AddressBuildingNumber ?? "";
            var streetName = AddressStreetName ?? "";
            var suburbName = AddressSuburbName ?? "";
            var cityName = AddressCityName ?? "";
            var stateName = AddressStateName ?? "";
            var countryName = AddressCountryName ?? "";
            var postcode = AddressPostcode ?? "";
            return $"{apartmentNumber}/{buildingNumber} {streetName} {suburbName} {cityName} {stateName} {countryName} {postcode}";
        }

        public bool Equals(FormalAddress other)
        {
            return ToString().Equals(other.ToString());
        }
    }
}
