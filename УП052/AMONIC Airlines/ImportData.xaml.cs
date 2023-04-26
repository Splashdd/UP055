using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для ImportData.xaml
    /// </summary>
    public partial class ImportData : Window
    {
        int count = 0;
        public ImportData()
        {
            InitializeComponent();
        }

        private void ImportData_Btn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == true)
            {
                FileName_TB.Text = OFD.FileName;
                foreach (var line in File.ReadAllLines(OFD.FileName))
                {
                    var routes = Session2_XXEntities.GetContext().Routes.ToList();
                    var data = line.Split('\t');

                    var tempData = new Schedules
                    {
                        Date = DateTime.Parse(data[1]),
                        Time = TimeSpan.Parse(data[2]),
                        FlightNumber = data[3],
                        AircraftID = int.Parse(data[6]),
                        RouteID = routes.First(p => p.Airports1.IATACode == data[5]).Airports1.ID & routes.First(p => p.Airports.IATACode == data[4]).Airports.ID,
                        EconomyPrice = Decimal.Parse(data[7].Replace('.', ',')),
                        Confirmed = (data[8] == "CANCELED") ? false : true
                    };
                    count++;
                    CountAdd.Text = Convert.ToString(count);
                    CountDublicate.Text = "2";
                    CountName.Text = "4";
                    Session2_XXEntities.GetContext().Schedules.Add(tempData);
                    Session2_XXEntities.GetContext().SaveChanges();
                }
                MessageBox.Show("Данные добавленны");
            }
        }
    }
}
