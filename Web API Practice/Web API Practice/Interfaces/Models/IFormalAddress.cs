namespace Web_API_Practice.Interfaces.Models
{
    public interface IFormalAddress
    {
        string AddressApartmentNumber { get; set; }
        string AddressBuildingNumber { get; set; }
        string AddressStreetName { get; set; }
        string AddressSuburbName { get; set; }
        string AddressCityName { get; set; }
        string AddressStateName { get; set; }
        string AddressCountryName { get; set; }
        string AddressPostcode { get; set; }
        string ToString();
    }
}
