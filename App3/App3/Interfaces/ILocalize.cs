using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace App3.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
