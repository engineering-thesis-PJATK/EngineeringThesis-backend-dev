namespace OneBan_TMS.Models.DTOs.Address
{
    using Models;
    public class AddressDto
    {
        public string AdrTown { get; set; }
        public string AdrStreet { get; set; }
        public string AdrStreetNumber { get; set; }
        public string AdrPostCode { get; set; }
        public string AdrCountry { get; set; }

        public Address GetAddress()
        {
            return new Address()
            {
                AdrTown = this.AdrTown,
                AdrStreet = this.AdrStreet,
                AdrStreetNumber = this.AdrStreetNumber,
                AdrPostCode = this.AdrPostCode,
                AdrCountry = this.AdrCountry
            };
        }

        public Address GetAddressToUpdate(Address addressToUpdate)
        {
            addressToUpdate.AdrTown = this.AdrTown;
            addressToUpdate.AdrStreet = this.AdrStreet;
            addressToUpdate.AdrStreetNumber = this.AdrStreetNumber;
            addressToUpdate.AdrPostCode = this.AdrPostCode;
            addressToUpdate.AdrCountry = this.AdrCountry;
            return addressToUpdate;
        }
    }
}