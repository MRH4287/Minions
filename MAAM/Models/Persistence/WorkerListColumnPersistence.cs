namespace MAAM.Models.Persistence
{
    public class WorkerListColumnPersistence : PersistenceBase
    {
        public override string PersistenceKey => "WorkerListColumn";

        private bool _name = false;

        public bool Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private bool _image = false;

        public bool Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }


        private bool _age = true;

        public bool Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private bool _race = false;

        public bool Race
        {
            get { return _race; }
            set
            {
                _race = value;
                OnPropertyChanged();
            }
        }

        private bool _sex = false;

        public bool Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                OnPropertyChanged();
            }
        }

        private bool _job = false;

        public bool Job
        {
            get { return _job; }
            set
            {
                _job = value;
                OnPropertyChanged();
            }
        }

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

        private bool _condidtion = true;

        public bool Condition
        {
            get { return _condidtion; }
            set
            {
                _condidtion = value;
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
