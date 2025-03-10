using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateNewUserDialog.xaml
    /// </summary>
    public partial class CreateNewUserDialog : Window
    {
        public CreateNewUserDialog()
        {
            InitializeComponent();
        }
        

        private void CloseModal(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
