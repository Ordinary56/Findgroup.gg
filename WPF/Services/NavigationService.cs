using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.Core;

namespace WPF.Services
{
    public interface INavigation
    {
        public ViewModelBase CurrentViewModel { get; set; }
        public void MoveTo<T>() where T : ViewModelBase;

    }
    public class NavigationService(Func<Type, ViewModelBase>  factory) : ObservableObject, INavigation
    {
        Func<Type, ViewModelBase> _factory = factory;
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
