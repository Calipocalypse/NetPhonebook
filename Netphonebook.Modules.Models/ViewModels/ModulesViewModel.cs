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
        private readonly IRegionManager _regionManager;
        private readonly IDataProvider _dataProvider;

        public DelegateCommand<string> NavigateToModelCreator { get; set; }
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
            NavigateToModelCreator = new DelegateCommand<string>(Navigate);
            DeleteCommand = new DelegateCommand(DeleteModel, CanDeleteModel).ObservesProperty(() => SelectedModel);
        }

        private bool CanDeleteModel()
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

        private void Navigate(string uri)
        {
            switch (uri)
            {
                case "Add": SwitchView(new ModelCreatorView(), "AddModelCreatorView");
                    break;
                case "Edit": SwitchView(new ModelCreatorView(),"EditModelCreatorView");
                    break;
                default:
                    break;
            }
        }

        private void SwitchView(object obj, string uri)
        {
            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(obj, uri);
            var view = _regionManager.Regions["ContentRegion"].GetView("uri");
        }
    }
}
