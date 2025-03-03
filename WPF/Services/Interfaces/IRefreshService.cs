using WPF.MVVM.Model;

namespace WPF.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for refreshing the admin's token
    /// <para>It should only be called when access token expires</para>
    /// </summary>
    public interface IRefreshService
    {
        public Task<bool> Refresh(AdminUser user);
    }
}
