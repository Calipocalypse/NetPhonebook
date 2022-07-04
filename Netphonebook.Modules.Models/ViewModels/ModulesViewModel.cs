using Netphonebook.Modules.Models.Views;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class ModulesViewModel : BindableBase
    {
        private readonly INavigateAsync _navigator;
        private readonly IRegionManager _regionManager;
        private readonly IDataProvider _dataProvider;

        public DelegateCommand AddModelCreator { get; set; }
        public DelegateCommand EditModelCreator { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public ObservableCollection<VirtualModel> VirtualModels { get; set; }

        private VirtualModel selectedModel;
        public VirtualModel SelectedModel
        {
            get { return selectedModel; }
            set { SetProperty(ref selectedModel, value); }
        }

        public ModulesViewModel(IDataProvider dataProvider, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            VirtualModels = _dataProvider.GetVirtualModels();
            AddModelCreator = new DelegateCommand(OpenAddModelCreator);
            EditModelCreator = new DelegateCommand(OpenEditModelCreator, CanDeleteOrEditModel).ObservesProperty(() => SelectedModel);
            DeleteCommand = new DelegateCommand(DeleteModel, CanDeleteOrEditModel).ObservesProperty(() => SelectedModel);
        }
        private void OpenAddModelCreator()
        {
            SwitchView(new ModelCreatorView(), "AddModelCreatorView", "add");
        }

        private void OpenEditModelCreator()
        {
            SwitchView(new ModelCreatorView(), "EditModelCreatorView", "edit");
        }

        private bool CanDeleteOrEditModel()
        {
            if (SelectedModel == null) return false;
            else return true;
        }

        private void DeleteModel()
        {
            var toDelete = SelectedModel;
            VirtualModels.Remove(toDelete);
            _dataProvider.DestroyModel(toDelete);
        }

        private void SwitchView(object obj, string uri, string mode)
        {
            NavigationParameters navPar = new NavigationParameters()
            {
                { "model", SelectedModel },
                { "mode", mode }
            };

            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(obj, uri);
            _regionManager.Regions["ContentRegion"].Activate(obj);
            _regionManager.Regions["ContentRegion"].RequestNavigate(uri, navPar);
        }
    }
}
