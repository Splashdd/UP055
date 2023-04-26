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
using AMONIC_Airlines.Windows;

namespace AMONIC_Airlines.Windows
{
    /// <summary>
    /// Логика взаимодействия для Sys.xaml
    /// </summary>
    public partial class Sys : Window
    {
        Session1_XXEntities db = new Session1_XXEntities();
        public Sys()
        {
            InitializeComponent();
            var alloffice = Session1_XXEntities.GetContext().Offices.ToList();
            alloffice.Insert(0, new Offices
            {
                Title = "All offices"
            });
            CB_Offices.SelectedIndex = 0;
            CB_Offices.ItemsSource = alloffice;
            UpdateTable();
            if (MainWindow.Globals.RoleID == 2)
            {
                AddUser_MI.IsEnabled = false;
            }
        }

        public void UpdateTable()
        {
            var curUsers = db.Users.ToList();
            if (CB_Offices.SelectedIndex > 0 && CB_Offices.SelectedIndex != 1)
            {
                curUsers = curUsers.Where(p => p.OfficeID == CB_Offices.SelectedIndex + 1).ToList();
            }
            else if (CB_Offices.SelectedIndex == 1)
            {
                curUsers = curUsers.Where(p => p.Offices.Title == "Abu dhabi").ToList();
            }

            DGrid.ItemsSource = curUsers;
        }
        private void Exit_btn(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Close();
        }

        private void Add_user_Btn(object sender, RoutedEventArgs e)
        {
            AddRoleWindow ARW = new AddRoleWindow();
            ARW.Show();

        }

        private void Cel_Cha(object sender, SelectionChangedEventArgs e)
        {
            UpdateTable();
        }

        private void ChangeActive_Btn(object sender, RoutedEventArgs e)
        {
            var choose = DGrid.SelectedItems.Cast<Users>().ToList();
            Users users = db.Users.Find(choose.FirstOrDefault().ID);
            if (users.Active == true)
            {
                users.Active = false;
            }
            else
            {
                users.Active = true;
            }
            db.SaveChanges();
            MessageBox.Show("Доступ изменён");
            UpdateTable();
        }

        private void ChangeRole_Btn(object sender, RoutedEventArgs e)
        {
            var choose = DGrid.SelectedItems.Cast<Users>().ToList();
            Users users = db.Users.Find(choose.FirstOrDefault().ID);
            if (users.RoleID == 1)
            {
                users.RoleID = 2;
            }
            else
            {
                users.RoleID = 1;
            }
            db.SaveChanges();
            MessageBox.Show("Роль изменена");
            UpdateTable();
        }
    }
}
