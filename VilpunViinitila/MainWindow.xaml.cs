using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace VilpunViinitila {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    protected ViinitilaDbContainer _db;
    public MainWindow() {

      _db = new ViinitilaDbContainer();
      InitializeComponent();
      pnlEditWineControls.Visibility = Visibility.Collapsed;
      refreshValueStats();
      refreshCountryStats();


    }

    private void btnAddWine_Click(object sender, RoutedEventArgs e) {
      var winevm = pnlWineDataFields.DataContext as WineViewModel;


      if (winevm != null) {

        var wine = new Wine() {
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



      refreshWineList();
      refreshValueStats();
      refreshCountryStats();
      pnlWineDataFields.DataContext = new WineViewModel();


    }

    private void pnlWinesView_Initialized(object sender, EventArgs e) {

      refreshWineList();




    }

    private void WineListRowSelected(object sender, RoutedEventArgs e) {

      var row = sender as DataGridRow;

      if (row != null) {
   
        var wine = row.Item as Wine;
        if (wine != null) {
          pnlWineDataFields.DataContext = new WineViewModel() {
            Id = wine.Id,
            Country = wine.Country,
            Grape = wine.Grape,
            Name = wine.WineName,
            Price = wine.Price,
            Region = wine.Region,
            Sortiment = wine.Sortiment
          };
          pnlAddWineControls.Visibility = Visibility.Collapsed;
          pnlEditWineControls.Visibility = Visibility.Visible;
          

        }
        else
        {
          pnlWineDataFields.DataContext = new WineViewModel();
          pnlEditWineControls.Visibility = Visibility.Collapsed;
          pnlAddWineControls.Visibility = Visibility.Visible;
          

        }
      }
    }

    private void refreshValueStats()
    {
      if (_db.WineSet.Count() != 0)
      {
        var totalValue = _db.WineSet.Sum(w => w.Price);
        var wineCount = _db.WineSet.Count();

        pnlValueStats.DataContext = new VineYardValueViewModel() {TotalValue = totalValue, WineCount = wineCount};
      }
      else
      {
        pnlValueStats.DataContext = new VineYardValueViewModel() {TotalValue = 0, WineCount = 0};
      }





    }


    private void refreshCountryStats()
    {

      if (_db.WineSet.Count() != 0)
      {


        var winesByCountry = from wine in _db.WineSet
                             group wine by wine.Country
                             into g
                             where g.Count() > 0
                             select new WineByCountryViewModel() {Country = g.Key, WineCount = g.Count()};





        grdWinesByCountry.Columns.Clear();


        foreach (var country in winesByCountry)
        {
          grdWinesByCountry.Columns.Add(new DataGridTextColumn()
                                          {Header = country.Country, Binding = new Binding("WineCount")});

        }

        grdWinesByCountry.ItemsSource = new List<WineByCountryViewModel>() {winesByCountry.FirstOrDefault()};

        lblWinesByCountry.Visibility = Visibility.Visible;
        grdWinesByCountry.Visibility = Visibility.Visible;
      }
      else
      {
        lblWinesByCountry.Visibility = Visibility.Collapsed;
        grdWinesByCountry.Visibility = Visibility.Collapsed;
      }

    }


  

    private void refreshWineList()
    {

      var wineListVM = new WineListViewModel();

      wineListVM.WineList = new ObservableCollection<Wine>(_db.WineSet);

      grdWineList.DataContext = wineListVM;
      

    }

    private void btnDeleteWine_Click(object sender, RoutedEventArgs e) {
      var winevm = pnlWineDataFields.DataContext as WineViewModel;

      if(winevm!=null)
      {
        var wine = _db.WineSet.Where(w => w.Id == winevm.Id).SingleOrDefault();

        if (wine == null)
          throw new InvalidOperationException(String.Format("Tried to delete wine with id {0}, but was not found in db.",
                                                        winevm.Id));

        _db.WineSet.DeleteObject(wine);
        _db.SaveChanges();

        pnlEditWineControls.Visibility = Visibility.Collapsed;
        pnlAddWineControls.Visibility = Visibility.Visible;
        
        refreshWineList();
        refreshValueStats();
        refreshCountryStats();

      }

     

    }

    private void btnSaveWine_Click(object sender, RoutedEventArgs e) {

      
      var winevm = pnlWineDataFields.DataContext as WineViewModel;

      if (winevm != null)
      {
        var wine = _db.WineSet.Where(w => w.Id == winevm.Id).SingleOrDefault();

        if (wine == null)
          throw new InvalidOperationException(String.Format("Tried to edit wine with id {0}, but was not found in db.",
                                                            winevm.Id));


        wine.Price = winevm.Price;
        wine.Region = winevm.Region;
        wine.Sortiment = winevm.Sortiment;
        wine.Country = winevm.Country;
        wine.WineName = winevm.Name;
        _db.SaveChanges();

        refreshWineList();

      }
    }

    private void btnCancelAddWine_Click(object sender, RoutedEventArgs e)
    {
      pnlWineDataFields.DataContext = new WineViewModel();


    }

    private void btnCancelEditWine_Click(object sender, RoutedEventArgs e) {
      pnlEditWineControls.Visibility = Visibility.Collapsed;
      pnlAddWineControls.Visibility = Visibility.Visible;
      pnlWineDataFields.DataContext = new WineViewModel();

      grdWineList.SelectedItem = null;









    }
  }
}

