using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Windows.Data;
using VilpunViinitila.Model;

namespace VilpunViinitila.ViewModel {
 public class WineListViewModel {
    public  ObservableCollection<Wine> WineList { get;  set; }


   public WineListViewModel()
   {
      WineList = new  ObservableCollection<Wine>();
   }





  }
}
