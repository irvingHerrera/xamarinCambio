using App3.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Helper
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "App3.Resources.Resource";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) { return string.Empty; }

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
                throw new ArgumentException(String.Format(
                    "Key '{0}' was not found in resources '{1}' for culture '{2}'.",
                    Text, ResourceId, ci.Name ));
            }

            return translation;
        }
    }
}
