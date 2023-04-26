using AMONIC_Airlines.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int check = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public static class Globals
        {
            public static int RoleID;
        }

        private void Exit_Btn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Login_Btn(object sender, RoutedEventArgs e)
        {
            
            var db = new Session1_XXEntities();
            var auth = db.Users.AsNoTracking().FirstOrDefault(l => l.Email == Username_TB.Text & l.Password == Password_P.Password);
            if (auth != null)
            {
                if (auth.Active == false)
                {
                    MessageBox.Show("Доступ запрещён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Globals.RoleID = auth.RoleID;
                    Sys NW = new Sys();
                    NW.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                if (check == 3)
                {
                    timer.Interval = TimeSpan.FromSeconds(10);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                    Username_TB.IsEnabled = false;
                    Password_P.IsEnabled = false;
                }
                else
                {
                    check++;
                }
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Вы снова можете попробывать войти в программу");
            Username_TB.IsEnabled = true;
            Password_P.IsEnabled = true;
            timer.Stop();
        }
    }
}
