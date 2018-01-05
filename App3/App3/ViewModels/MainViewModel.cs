
namespace App3.ViewModels
{
    using App3.Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.ComponentModel;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using System;

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool _isRunning;
        bool _isEnable;
        string _result;
        ObservableCollection<Rate> _rates;

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
        public Rate SourceRate { get; set; }
        public Rate TargetRate { get; set; }
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_result)));
                }
            }
        }

        #region Constructors
        public MainViewModel()
        {
            LoadRate();
        }
        #endregion

        #region Methods
        void LoadRate()
        {
            IsRunning = true;
            Result = "Loading rate...";

            try
            {
                var list = new List<Rate>{
                         new Rate { RateId = 3, Code="ALL", Name="Albanian Lek", TaxRate=112.78 },
                         new Rate { RateId = 4, Code = "AMD", Name = "Armenian Dram", TaxRate = 477.815313 },
                         new Rate { RateId = 5, Code = "ANG", Name = "Netherlands Antillean Guilder", TaxRate = 1.778253 },
                         new Rate { RateId = 6, Code = "AOA", Name = "Angolan", TaxRate = 165.9205 }};

                Rates = new ObservableCollection<Rate>(list);
                IsRunning = false;
                Result = "Ready to convert!";
                IsEnabled = true;
            }
            catch (System.Exception ex)
            {
                IsRunning = false;
                Result = ex.Message;
            }
        }
        #endregion

        #region Commands

        public ICommand ConConvertCommand
        {
            get
            {
                return new RelayCommand(Conver);
            }
        }

        private void Conver()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
