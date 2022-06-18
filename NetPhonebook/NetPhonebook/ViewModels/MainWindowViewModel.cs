using Prism.Events;
using Prism.Mvvm;

namespace NetPhonebook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        public MainWindowViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
        }

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
