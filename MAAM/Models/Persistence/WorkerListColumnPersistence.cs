namespace MAAM.Models.Persistence
{
    public class WorkerListColumnPersistence : WorkerListColumnPersistenceBase
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

    }
}
