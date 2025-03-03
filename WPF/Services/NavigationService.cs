using CommunityToolkit.Mvvm.ComponentModel;
using WPF.Core;
using WPF.Services.Interfaces;

namespace WPF.Services
{
    
    public class NavigationService(Func<Type, ViewModelBase>  factory) : ObservableObject, INavigationService
    {
        readonly Func<Type, ViewModelBase> _factory = factory;
        private ViewModelBase _viewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public void MoveTo<T>() where T : ViewModelBase
        {
            ViewModelBase viewmodel = _factory.Invoke(typeof(T));
            CurrentViewModel = viewmodel;
        }
    }
}
