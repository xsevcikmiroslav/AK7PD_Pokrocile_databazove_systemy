using BusinessLayer.BusinessObjects;

namespace BusinessLayer
{
    public static class ApplicationState
    {
        public static User ActiveUser { get; set; } = new User();
    }
}
