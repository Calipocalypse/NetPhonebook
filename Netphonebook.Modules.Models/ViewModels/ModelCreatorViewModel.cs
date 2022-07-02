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
using System.Windows;
using NetPhonebook.Core;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class ModelCreatorViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IDataProvider _dataProvider;

        public DelegateCommand ClickBack { get; set; }
        public DelegateCommand ClickAdd { get; set; }
        public DelegateCommand<string> SetColor { get; set; }
        public DelegateCommand<string> FavouriteColorCommand { get; set; }
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
            RaisePropertyChanged(nameof(FontColor));
            RaisePropertyChanged(nameof(BackgroundColor));
            RaisePropertyChanged(nameof(BorderColor));
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
        }

        private string outcomingColor;
        public SolidColorBrush OutcomingColor
        {
            get { return HexColorConverter.ToSolidColor(outcomingColor); }
            set 
            { 
                SetProperty(ref outcomingColor, HexColorConverter.ToHex(value));
            }
        }

        private byte[] colorPicker = new byte[3];
        public byte[] ColorPicker
        {
            get
            {
                OutcomingColor = new SolidColorBrush(Color.FromRgb(colorPicker[0], colorPicker[1], colorPicker[2]));
                return colorPicker;
            }
            set
            {
                SetProperty(ref colorPicker, value);
                OutcomingColor = new SolidColorBrush(Color.FromRgb(colorPicker[0], colorPicker[1], colorPicker[2]));
            }
        }

        public ObservableCollection<SolidColorBrush> FavouriteColors { get; set; }

        private SolidColorBrush selectedFavouriteColor;
        public SolidColorBrush SelectedFavouriteColor
        {
            get { return selectedFavouriteColor; }
            set 
            { 
                SetProperty(ref selectedFavouriteColor, value);
                if (selectedFavouriteColor != null) ColorPicker = new byte[3] { selectedFavouriteColor.Color.R, selectedFavouriteColor.Color.G, selectedFavouriteColor.Color.B};
            }
        }

        private string[] fontColor = new string[6];
        public SolidColorBrush FontColor
        {
            get { return HexColorConverter.ToSolidColor(fontColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref fontColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        private string[] backgroundColor = new string[6];
        public SolidColorBrush BackgroundColor
        {
            get { return HexColorConverter.ToSolidColor(backgroundColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref backgroundColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        private string[] borderColor = new string[6];
        public SolidColorBrush BorderColor
        {
            get { return HexColorConverter.ToSolidColor(borderColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref borderColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        public ModelCreatorViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            ClickBack = new DelegateCommand(NavigateBack);
            ClickAdd = new DelegateCommand(ClickedAdd);
            FavouriteColorCommand = new DelegateCommand<string>(ClickedFavColorButton);
            SetColor = new DelegateCommand<string>(ClickedSetColor);
            FavouriteColors = _dataProvider.GetFavouriteColors();
        }

        private void ClickedFavColorButton(string parameter)
        {
            switch (parameter)
            {
                case "Add": AddFavouriteColor(); 
                    break;
                case "Delete": RemoveFavouriteColor();
                    break;
            }
        }

        private void AddFavouriteColor()
        {
            FavouriteColors.Add(OutcomingColor);
            _dataProvider.AddFavouriteColor(new FavouriteColor { Id = Guid.NewGuid(), HexColor = HexColorConverter.ToHex(OutcomingColor) });
        }

        private void RemoveFavouriteColor()
        {
            _dataProvider.DestroyFavouriteColor(SelectedFavouriteColor);
            FavouriteColors.Remove(SelectedFavouriteColor);
        }

        private void ClickedSetColor(string parameter)
        {
            switch (parameter)
            {
                case "fontColor":
                    FontColor = OutcomingColor;
                    break;
                case "backgroundColor":
                    BackgroundColor = OutcomingColor;
                    break;
                case "borderColor":
                    BorderColor = OutcomingColor;
                    break;
            }
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
