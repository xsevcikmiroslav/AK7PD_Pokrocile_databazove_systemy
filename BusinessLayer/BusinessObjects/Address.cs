using System.Text.Json.Serialization;

namespace BusinessLayer.BusinessObjects
{
    public class Address : IBusinessObjectValidation
    {
        public string Street { get; set; } = string.Empty;

        public string DescriptiveNumber { get; set; } = string.Empty;

        public string OrientationNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Zip { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsValid =>
            !string.IsNullOrEmpty(City)
            && !string.IsNullOrEmpty(Zip)
            && Zip.All(c => char.IsDigit(c))
            && !string.IsNullOrEmpty(Street)
            && !string.IsNullOrEmpty(DescriptiveNumber);
    }
}
