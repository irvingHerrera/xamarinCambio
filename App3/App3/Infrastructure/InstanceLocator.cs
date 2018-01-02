namespace App3.Infrastructure
{
    using App3.ViewModels;

    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
