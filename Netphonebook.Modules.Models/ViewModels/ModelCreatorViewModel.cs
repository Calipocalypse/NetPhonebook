using Netphonebook.Modules.Models.Views;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class ModelCreatorViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IDataProvider _dataProvider;

        public DelegateCommand ClickBack { get; set; }
        public DelegateCommand ClickAdd { get; set; }

        private string modelName;
        public string ModelName
        {
            get { return modelName; }
            set { SetProperty(ref modelName, value); }
        }

        private sbyte numberOfCells = 1;
        public sbyte NumberOfCells
        {
            get { return numberOfCells; }
            set
            {
                if (selectedCellNumber > value) SelectedCellNumber = value; 
                SetProperty(ref numberOfCells, value); 
            }
        }

        private sbyte selectedCellNumber = 1;
        public sbyte SelectedCellNumber
        {
            get { return selectedCellNumber; }
            set
            {
                SetProperty(ref selectedCellNumber, value);
                OnCellChange();
            }
        }

        private void OnCellChange()
        {
            RaisePropertyChanged(nameof(FontSize));
        }

        private sbyte[] fontSize = new sbyte[6];
        public sbyte FontSize
        {
            get { return fontSize[SelectedCellNumber-1]; }
            set { SetProperty(ref fontSize[SelectedCellNumber-1], value); }
        }

        private sbyte borderSize;
        public sbyte BorderSize
        {
            get { return borderSize; }
            set { SetProperty(ref borderSize, value); }
        }

        private sbyte cornerRadius;
        public sbyte CornerRadius
        {
            get { return cornerRadius; }
            set { SetProperty(ref cornerRadius, value); }
        }

        public ModelCreatorViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            ClickBack = new DelegateCommand(NavigateBack);
            ClickAdd = new DelegateCommand(ClickedAdd);
        }

        private void ClickedAdd()
        {
            var ModelId = Guid.NewGuid();
            VirtualModel toAdd = new VirtualModel
            {
                Id = ModelId,
                Name = ModelName,
                CustomizationCells =
                new List<VirtualModelsCustomization>
                {
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 0 },
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 1 },
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 2 },
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 3 },
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 4 },
                    new VirtualModelsCustomization { Id = Guid.NewGuid(), ModelId = ModelId, CellId = 5 }
                }
            };
            _dataProvider.AddVirtualModel(toAdd);
        }

        private void NavigateBack()
        {
            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(new ModulesView(), "ModelsViewer");
            var view = _regionManager.Regions["ContentRegion"].GetView("ModelsViewer");
        }
    }
}
