using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MAAM.Models.Persistence
{
    public abstract class PersistenceBase
    {
        [JsonIgnore]
        public abstract string PersistenceKey { get; }

        [JsonIgnore]
        public bool IgnoreChanged = true;

        [JsonIgnore]
        private readonly Subject<string> _propertyChanged = new Subject<string>();
        [JsonIgnore]
        public IObservable<string> PropertyChanged => _propertyChanged.AsObservable();

        protected void OnPropertyChanged([CallerMemberName] string memberName = "")
        {
            if (IgnoreChanged)
            {
                return;
            }

            _propertyChanged.OnNext(memberName);
        }
    }
}
