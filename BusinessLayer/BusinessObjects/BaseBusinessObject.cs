using System.Text.Json.Serialization;

namespace BusinessLayer.BusinessObjects
{
    public abstract class BaseBusinessObject : IBusinessObjectValidation
    {
        public string _id { get; set; } = string.Empty;

        [JsonIgnore]
        public abstract bool IsValid { get; }
    }
}
