using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VilpunViinitila.Model;

namespace VilpunViinitila.ViewModel
{
   public  class MainFormViewModel
    {

       private ViinitilaDbContainer _db = new ViinitilaDbContainer();

       public Wine SelectedWine { get; set; }
       public IQueryable<Wine> Wines { get; set; }


       public void LoadData()
       {
           Wines = _db.WineSet;


       }

       public void AddSelectedWine()
       {
           _db.AddToWineSet(SelectedWine);
           _db.SaveChanges();

       }

    }
}
