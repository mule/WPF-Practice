using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
      protected ViinitilaDbContainer _db;
        public MainWindow()
        {

          _db = new ViinitilaDbContainer();
            InitializeComponent();
         

        }

        private void btnAddWine_Click(object sender, RoutedEventArgs e)
        {
            var winevm = pnlWineDataFields.DataContext as WineViewModel;


            if (winevm != null)
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

                    _db.WineSet.AddObject(wine);
                    _db.SaveChanges();
                
            }




        }

        private void pnlWinesView_Initialized(object sender, EventArgs e)
        {


          var wineListVM = new WineListViewModel();

            wineListVM.WineList = new ObservableCollection<Wine>(_db.WineSet);

          grdWineList.DataContext = wineListVM;



        }
    }
}
