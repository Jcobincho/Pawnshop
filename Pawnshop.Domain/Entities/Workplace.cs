namespace Pawnshop.Domain.Entities
{
    public class Workplace : BaseEntity
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string StreetAndBuildingNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
