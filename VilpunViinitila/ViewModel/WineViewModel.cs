using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VilpunViinitila.ViewModel {
  public class WineViewModel : INotifyPropertyChanged {
    private Guid _id;
    public Guid Id {
      get { return _id; }
      set { _id = value; if (PropertyChanged != null)OnPropertyChanged("Id"); }
    }

    private string _name;
    public string Name {
      get { return _name; }
      set {
        _name = value;
        if (PropertyChanged != null)
          OnPropertyChanged("Name");
      }
    }

    private string _country;
    public string Country {
      get { return _country; }
      set {
        _country = value; if (PropertyChanged != null)
          OnPropertyChanged("Country");
      }

    }

    private string _grape;
    public string Grape {
      get { return _grape; }
      set { _grape = value; if (PropertyChanged != null)OnPropertyChanged("Grape"); }
    }

    private string _region;
    public string Region {
      get { return _region; }
      set { _region = value; if (PropertyChanged != null)OnPropertyChanged("Region"); }


    }

    private string _sortiment;
    public string Sortiment {
      get { return _sortiment; }
      set { _sortiment = value; if (PropertyChanged != null)OnPropertyChanged("Sortiment"); }

    }


    private double _price;
    public double Price {
      get { return _price; }
      set { _price = value; if (PropertyChanged != null)OnPropertyChanged("Price"); }
    }


    protected void OnPropertyChanged(string name) {


      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) {
        handler(this, new PropertyChangedEventArgs(name));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
