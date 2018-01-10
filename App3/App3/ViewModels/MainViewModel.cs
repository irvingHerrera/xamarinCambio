
namespace App3.ViewModels
{
    using App3.Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.ComponentModel;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Xamarin.Forms;
    using App3.Helper;
    using App3.Services;
    using System.Threading.Tasks;

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool _isRunning;
        bool _isEnable;
        string _result;
        ObservableCollection<Rate> _rates;
        Rate _sourceRate;
        Rate _targeRate;
        string _status;
        List<Rate> rates;

        #region Service
        ApiService service;
        DataService dataService;
        DialogService dialogService;
        #endregion

        public string Amount { get; set; }
        public ObservableCollection<Rate> Rates
        {
            get
            {
                return _rates;
            }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rates)));
                }
            }
        }
        public Rate SourceRate
        {
            get
            {
                return _sourceRate;
            }
            set
            {
                if (_sourceRate != value)
                {
                    _sourceRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceRate)));
                }
            }
        }
        public Rate TargeRate
        {
            get
            {
                return _targeRate;
            }
            set
            {
                if (_targeRate != value)
                {
                    _targeRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargeRate)));
                }
            }
        }
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnable;
            }
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        #region Constructors
        public MainViewModel()
        {
            service = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            LoadRate();
        }
        #endregion

        #region Methods
        async void LoadRate()
        {
            IsRunning = true;
            Result = "Loading rate...";

            var connection = await service.CheckConnection();

            if (!connection.IsSuccess)
            {
                LoadLocalData();
            }
            else
            {
                await LoadDataFromAPI();

            }

            if (rates.Count == 0)
            {
                IsRunning = false;
                IsEnabled = false;
                Result = "There are not intenet connection and not load previously rates. " +
                         "Please try again with internet connection.";

                return;
            }
            
            Rates = new ObservableCollection<Rate>(rates);

            IsRunning = false;
            IsEnabled = true;
            Result = "Ready to convert!";
        }

        private async Task LoadDataFromAPI()
        {
            var url = Application.Current.Resources["URLAPI"].ToString();

            var response = await service.GetList<Rate>("http://apiexchangerates.azurewebsites.net", "/api/Rates");

            if (!response.IsSuccess)
            {
                LoadLocalData();
                return;
            }

            rates = (List<Rate>)response.Result;

            dataService.DeleteAll<Rate>();
            dataService.Save(rates);

            Status = "Rates loader from Internet";
        }

        private void LoadLocalData()
        {
            rates = dataService.Get<Rate>(false);

            Status = "Rates loader from local date";
        }
        #endregion

        #region Commands

        public ICommand ConvertCommand
        {
            get
            {
                return new RelayCommand(Conver);
            }
        }

        public ICommand SwithCommand
        {
            get
            {
                return new RelayCommand(Swith);
            }
        }

        private void Swith()
        {
            var aux = SourceRate;
            SourceRate = TargeRate;
            TargeRate = aux;
            Conver();
        }

        private async void Conver()
        {
            if (string.IsNullOrEmpty(Amount))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.AmountValidation, Languages.Accept);
                return;
            }

            decimal amount = 0;

            if (!decimal.TryParse(Amount, out amount))
            {
                await dialogService.ShowMessage(Languages.Error, "You must enter a numeric value in amount");
            }

            if (SourceRate == null)
            {
                await dialogService.ShowMessage(Languages.Error, "You must select a source rate");
                return;
            }

            if (TargeRate == null)
            {
                await dialogService.ShowMessage(Languages.Error, "You must select a source TargetRate");
                return;
            }

            var amountConverted = amount / (decimal)SourceRate.TaxRate * (decimal)TargeRate.TaxRate;

            Result = string.Format("{0} {1} = {2} {3:C2}", SourceRate.Code, amount, TargeRate.Code, amountConverted);
        }

        #endregion
    }
}
