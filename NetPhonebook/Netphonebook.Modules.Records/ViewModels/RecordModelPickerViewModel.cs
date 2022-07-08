using Netphonebook.Modules.Records.Views;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class RecordModelPickerViewModel : BindableBase
    {
        public ObservableCollection<VirtualModel> VirtualModels { get; set; }

        public DelegateCommand Choose { get; set; }

        public IRegionManager _regionManager { get; set; }
        public IDataProvider _dataProvider { get; set; }

        private VirtualModel selectedModel;
        public VirtualModel SelectedModel
        {
            get { return selectedModel; }
            set { SetProperty(ref selectedModel, value); }
        }

        public RecordModelPickerViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            VirtualModels = _dataProvider.GetVirtualModels();
            Choose = new DelegateCommand(ChooseClicked);
        }

        private void ChooseClicked()
        {
            SwitchView(new RecordEditor(), "RecordEditorView");
        }

        private void SwitchView(object obj, string uri)
        {
            NavigationParameters navPar = new NavigationParameters()
            {
                { "model", SelectedModel }
            };

            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(obj, uri);
            _regionManager.Regions["ContentRegion"].Activate(obj);
            _regionManager.Regions["ContentRegion"].RequestNavigate(uri, navPar);
        }
    }
}
