using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using AMONIC_Airlines.Windows;

namespace AMONIC_Airlines.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddRoleWindow.xaml
    /// </summary>
    public partial class AddRoleWindow : Window
    {
        private Users _currentUsers = new Users();
        public AddRoleWindow()
        {
            InitializeComponent();
            DataContext = _currentUsers;
            CB_Office.ItemsSource = Session1_XXEntities.GetContext().Offices.ToList();
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Btn(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (Email_TB.Text == "")
                Errors.AppendLine("Введите почту");
            if (FirstName_TB.Text == "")
                Errors.AppendLine("Введите Имя");
            if (Lastname_TB.Text == "")
                Errors.AppendLine("Введите фамилию");
            if (Birthday_TB.Text == "")
                Errors.AppendLine("Введите день рождения");
            if (Password_TB.Text == "")
                Errors.AppendLine("Введите пароль");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString());
                return;
            }

            if (_currentUsers.ID == 0)
            {
                Session1_XXEntities.GetContext().Users.Add(_currentUsers);
            }

            try
            {
                Session1_XXEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь сохранен");
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
