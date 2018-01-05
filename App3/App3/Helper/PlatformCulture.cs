using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Helper
{
    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString));
            }

            PlatformString = platformCultureString.Replace("_", "-");

            var dashIndex = platformCultureString.IndexOf("", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = string.Empty;
            }
        }


        public string PlatformString { get; set; }
        public string LanguageCode { get; set; }
        public string LocaleCode { get; set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
