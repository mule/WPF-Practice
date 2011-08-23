using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace VilpunViinitila
{
    public class CurrencyValidationRule : ValidationRule 
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string priceStr = value as String;
            double price;

            if (Double.TryParse(priceStr, NumberStyles.Currency, cultureInfo, out price))
                return new ValidationResult(true, null);
            else
            {
                return  new ValidationResult(false, "Not a valid number for currency");
            }



        }
    }
}
