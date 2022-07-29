using NetPhonebook.Core;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Netphonebook.Modules.Common.ViewModels
{
    public class ColorPickerViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;
        public ObservableCollection<SolidColorBrush> FavouriteColors { get; set; }
        public DelegateCommand<string> FavouriteColorCommand { get; set; }

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

        private SolidColorBrush outcomingColor;
        public SolidColorBrush OutcomingColor
        {
            get { return outcomingColor; }
            set
            {
                SetProperty(ref outcomingColor, value);
            }
        }


        private SolidColorBrush selectedFavouriteColor;
        public SolidColorBrush SelectedFavouriteColor
        {
            get { return selectedFavouriteColor; }
            set
            {
                SetProperty(ref selectedFavouriteColor, value);
                if (selectedFavouriteColor != null) ColorPicker = new byte[3]
                {
                    selectedFavouriteColor.Color.R,
                    selectedFavouriteColor.Color.G,
                    selectedFavouriteColor.Color.B
                };
            }
        }

        public ColorPickerViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            FavouriteColorCommand = new DelegateCommand<string>(ClickedFavColorButton);
            FavouriteColors = _dataProvider.GetFavouriteColors();
        }

        private void ClickedFavColorButton(string parameter)
        {
            switch (parameter)
            {
                case "Add":
                    AddFavouriteColor();
                    break;
                case "Delete":
                    RemoveFavouriteColor();
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
    }
}
