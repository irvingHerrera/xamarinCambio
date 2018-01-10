using Xamarin.Forms;

[assembly: Dependency(typeof(App3.iOS.Implementations.Config))]
namespace App3.iOS.Implementations
{
    using System;
    using SQLite.Net.Interop;
    using App3.Interfaces;

    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = System.Environment.GetFolderPath(
                        Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(
                        directory,
                        "..",
                        "Library");
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return platform;
            }
        }
    }
}