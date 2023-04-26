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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Session2_XXEntities db = new Session2_XXEntities();
        public MainWindow()
        {
            InitializeComponent();
            From_CB.ItemsSource = Session2_XXEntities.GetContext().Airports.ToList();
            To_CB.ItemsSource = Session2_XXEntities.GetContext().Airports.ToList();
            FLightNumber_CB.ItemsSource = Session2_XXEntities.GetContext().Schedules.ToList();
            UpdateTable();
        }

        public void UpdateTable()
        {
            DGrid.ItemsSource = db.Schedules.ToList();
        }

        private void Cancel_Flight_Btn(object sender, RoutedEventArgs e)
        {
            var choose = DGrid.SelectedItems.Cast<Schedules>().ToList();
            Schedules shedules = db.Schedules.Find(choose.FirstOrDefault().ID);
            if (shedules.Confirmed == true)
            {
                shedules.Confirmed = false;
            }
            else
            {
                shedules.Confirmed = true;
            }
            db.SaveChanges();
            MessageBox.Show("Статус полёта изменён");
            UpdateTable();
        }

        private void ApplyFilter_Btn(object sender, RoutedEventArgs e)
        {
            var searchFlight = db.Schedules.ToList();
            if (Outbound_DP.SelectedDate != null)
                searchFlight = searchFlight.Where(p => p.Date == Outbound_DP.SelectedDate).ToList();
            if (From_CB.SelectedIndex > 0)
                searchFlight = searchFlight.Where(p => p.Routes.DepartureAirportID == (From_CB.SelectedItem as Airports).ID).ToList();
            if (To_CB.SelectedIndex > 0)
                searchFlight = searchFlight.Where(p => p.Routes.ArrivalAirportID == (To_CB.SelectedItem as Airports).ID).ToList();
            if (SortBy_CB.SelectedIndex == 0)
                searchFlight = searchFlight.OrderBy(p => p.Date).ToList();
            else if (SortBy_CB.SelectedIndex == 1)
                searchFlight = searchFlight.OrderBy(p => p.EconomyPrice).ToList();
            else
                searchFlight = searchFlight.OrderBy(p => p.Confirmed).ToList();
            if (FLightNumber_CB.SelectedIndex > 0)
                searchFlight = searchFlight.Where(p => p.FlightNumber == (FLightNumber_CB.SelectedItem as Schedules).FlightNumber).ToList();
            DGrid.ItemsSource = searchFlight;

        }

        private void EditFlight_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                Edit edit = new Edit(DGrid.SelectedItem as Schedules);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Выберите рейс");
            }
            Session2_XXEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        private void Import_Btn(object sender, RoutedEventArgs e)
        {
            ImportData ID = new ImportData();
            ID.Show();
        }
    }
}
