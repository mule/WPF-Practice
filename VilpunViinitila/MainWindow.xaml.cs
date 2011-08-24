using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VilpunViinitila.Model;
using VilpunViinitila.ViewModel;

namespace VilpunViinitila
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


        }

        private void btnAddWine_Click(object sender, RoutedEventArgs e)
        {
            var winevm = pnlWineDataFields.DataContext as WineViewModel;


            if (winevm != null)
            {
                using (var db = new ViinitilaDbContainer())
                {
                    var wine = new Wine()
                                   {
                                       Id = Guid.NewGuid(),
                                       WineName = winevm.Name,
                                       Country = winevm.Country,
                                       Grape = winevm.Grape,
                                       Region = winevm.Region,
                                       Sortiment = winevm.Sortiment,
                                       Price = winevm.Price
                                   };

                    db.WineSet.AddObject(wine);
                    db.SaveChanges();
                }
            }




        }
    }
}
