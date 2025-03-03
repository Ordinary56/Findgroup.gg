using WPF.Core;

namespace WPF.Services.Interfaces
{
    public interface INavigationService
    {
        public ViewModelBase CurrentViewModel { get; set; }
        public void MoveTo<T>() where T : ViewModelBase;

    }
}
