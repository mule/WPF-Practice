using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace VilpunViinitila {
  public class RequiredTextValidationRule : ValidationRule {
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {

      string txt = value as String;

      if(String.IsNullOrWhiteSpace(txt))
        return  new ValidationResult(false,"Field is mandatory");
      else
      {
        return  new ValidationResult(true,null);
      }

    }
  }
}
