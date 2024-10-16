namespace MAAM.Models.Persistence
{
    public abstract class WorkerListColumnPersistenceBase : PersistenceBase
    {
        private bool _serviceStarted = true;

        public bool ServiceStarted
        {
            get { return _serviceStarted; }
            set
            {
                _serviceStarted = value;
                OnPropertyChanged();
            }
        }

        private bool _serviceEnded = true;

        public bool ServiceEnded
        {
            get { return _serviceEnded; }
            set
            {
                _serviceEnded = value;
                OnPropertyChanged();
            }
        }

        private bool _timeOnBoard = true;

        public bool TimeOnBoard
        {
            get { return _timeOnBoard; }
            set
            {
                _timeOnBoard = value;
                OnPropertyChanged();
            }
        }

        private bool _payment = false;

        public bool Payment
        {
            get { return _payment; }
            set
            {
                _payment = value;
                OnPropertyChanged();
            }
        }

        private bool _daysWithoutPay = false;

        public bool DaysWithoutPay
        {
            get { return _daysWithoutPay; }
            set
            {
                _daysWithoutPay = value;
                OnPropertyChanged();
            }
        }

        private bool _currentPayment = true;

        public bool CurrentPayment
        {
            get { return _currentPayment; }
            set
            {
                _currentPayment = value;
                OnPropertyChanged();
            }
        }

        private bool _menu = false;

        public bool Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                OnPropertyChanged();
            }
        }

    }
}
