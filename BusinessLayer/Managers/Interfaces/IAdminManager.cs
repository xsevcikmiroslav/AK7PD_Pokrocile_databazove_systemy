using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IAdminManager
    {
        User ApproveUser(string userId);

        User BanUser(string userId);
    }
}
