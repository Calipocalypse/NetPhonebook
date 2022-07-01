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
<<<<<<< HEAD
using System.Windows.Media;
=======
using System.Windows;
>>>>>>> 3e878456859b4f025eced232d0eeb5888154f932

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
            RaisePropertyChanged(nameof(BorderSize));
            RaisePropertyChanged(nameof(CornerRadius));
        }

        private sbyte[] fontSize = new sbyte[6];
        public sbyte FontSize
        {
            get { return fontSize[SelectedCellNumber-1]; }
            set { SetProperty(ref fontSize[SelectedCellNumber-1], value); }
        }

        private sbyte[] borderSize = new sbyte[6];
        public sbyte BorderSize
        {
            get { return borderSize[SelectedCellNumber-1]; }
            set { SetProperty(ref borderSize[SelectedCellNumber-1], value); }
        }

        private sbyte[] cornerRadius = new sbyte[6];
        public sbyte CornerRadius
        {
            get { return cornerRadius[SelectedCellNumber-1]; }
            set { SetProperty(ref cornerRadius[SelectedCellNumber-1], value); }
<<<<<<< HEAD
=======
        }

        private byte colorX;
        public byte ColorX 
        {
            get { return colorX; }
            set { SetProperty(ref colorX, value); }
        }
        
        private byte colorY;
        public byte ColorY 
        {
            get { return colorY; }
            set { SetProperty(ref colorY, value); }
        }

        public void OnLeftMouseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

>>>>>>> 3e878456859b4f025eced232d0eeb5888154f932
        }

        public ObservableCollection<SolidColorBrush> ColorList { get; set; }

        public ModelCreatorViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            ColorList = GetSolidColorBrushes();
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

        private ObservableCollection<SolidColorBrush> GetSolidColorBrushes()
        {
            var brushes = new ObservableCollection<SolidColorBrush>();
            for (int i = 0; i <= 255; i+=40)
            {
                for (int j = 0; j <= 255; j+=40)
                {
                    for(int k = 0; k <= 255; k+=40)
                    {
                        var color = Color.FromRgb((byte)i, (byte)j, (byte)k);
                        brushes.Add(new SolidColorBrush(color));
                    }
                }
            }
            return brushes;
        }
    }
}
