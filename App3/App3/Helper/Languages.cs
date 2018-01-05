using App3.Interfaces;
using App3.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App3.Helper
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Tile
        {
            get { return Resource.Tile; }
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string AmountValidation
        {
            get { return Resource.AmountValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }
    }
}
