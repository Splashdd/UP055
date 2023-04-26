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

namespace AMONIC_Airlines.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        Sys S = new Sys();
        private Users _currentUsers = new Users();
        public EditUserWindow(Users selectedItem)
        {
            InitializeComponent();
            Office_CB.ItemsSource = Session1_XXEntities.GetContext().Offices.ToList();
            //var choose = S.DGrid.SelectedItems.Cast<Users>().ToList();
            //DataContext = choose;
            if (selectedItem != null)
                _currentUsers = selectedItem;
            DataContext = _currentUsers;
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditUser_Btn(object sender, RoutedEventArgs e)
        {
        }

    }
}
