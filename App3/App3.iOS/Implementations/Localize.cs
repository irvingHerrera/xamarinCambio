using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using App3.Helper;
using App3.Interfaces;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(App3.iOS.Implementations.Localize))]
namespace App3.iOS.Implementations
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }

            System.Globalization.CultureInfo ci = null;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            throw new NotImplementedException();
        }

        string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;

            switch (iOSLanguage)
            {
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
            }

            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platformCulture)
        {
            var netLanguage = platformCulture.LanguageCode;

            switch (platformCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT";
                    break;
                case "gsw":
                    netLanguage = "de.CH";
                    break;
            }

            return netLanguage;
        }
    }
}