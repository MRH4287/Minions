namespace MAAM.Models.Persistence
{
    public class GenericWorkerListColumnPersistence : WorkerListColumnPersistenceBase
    {
        public override string PersistenceKey => "GenericWorkerListColumn";

        private bool _workerType = false;

        public bool WorkerType
        {
            get { return _workerType; }
            set
            {
                _workerType = value;
                OnPropertyChanged();
            }
        }

        private bool _amount = false;

        public bool Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
    }
}
