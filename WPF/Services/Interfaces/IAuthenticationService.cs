using WPF.MVVM.Model;

namespace WPF.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> Authenticate(AdminUser user);
    }
}
