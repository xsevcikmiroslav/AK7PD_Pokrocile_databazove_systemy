namespace BusinessLayer.BusinessObjects
{
    public abstract class BaseBusinessObject : IBusinessObjectValidation
    {
        public string _id { get; set; } = string.Empty;

        public abstract bool IsValid { get; }
    }
}
