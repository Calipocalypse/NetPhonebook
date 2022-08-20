using Netphonebook.Modules.Common.ViewModels;
using NetPhonebook.Core;
using NetPhonebook.Core.Enums;
using NetPhonebook.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class CellCreatorTextViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;
        private ModelCreatorViewModel parentView;
        private ColorPickerViewModel colorPickerInstance;

        public DelegateCommand<string> SetColor { get; set; }
        public CellCreatorTextViewModel(IDataProvider dataProvider, ModelCreatorViewModel ParentView, ColorPickerViewModel ColorPickerInstance)
        {
            _dataProvider = dataProvider;
            parentView = ParentView;
            SetColor = new DelegateCommand<string>(ClickedSetColor);
            colorPickerInstance = ColorPickerInstance;
        }

        public sbyte RealSelectedCellNumber => (sbyte)(parentView.SelectedCellNumber - 1);

        #region Bindings

        //private sbyte[] fontSize = new sbyte[6];
        //public sbyte FontSize
        //{
        //    get { return fontSize[SelectedCellNumber - 1]; }
        //    set { SetProperty(ref fontSize[SelectedCellNumber - 1], value); }
        //}

        private Visibility cellCreatorTextVisibility = Visibility.Visible;
        public Visibility CellCreatorTextVisibility
        {
            get { return cellCreatorTextVisibility; }
            set { SetProperty(ref cellCreatorTextVisibility, value); }
        }


        /* 1. FontSize */
        private sbyte[] fontSize = new sbyte[6] {1,2,3,4,5,6};
        public sbyte[] FontSize
        {
            get { return fontSize; }
            set 
            { 
                SetProperty(ref fontSize, value);
                parentView.UpdatePresenter();
            }
        }
        public sbyte FontSizeCell //for XAML
        { 
            get { return fontSize[RealSelectedCellNumber]; }
            set 
            {
                SetProperty(ref fontSize[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        /* 2. BorderSize */
        private sbyte[] borderSize = new sbyte[6];
        public sbyte[] BorderSize
        {
            get { return borderSize; }
            set 
            { 
                SetProperty(ref borderSize, value);
                parentView.UpdatePresenter();
            }
        }
        public sbyte BorderSizeCell //for XAML
        {
            get { return borderSize[RealSelectedCellNumber]; }
            set 
            { 
                SetProperty(ref borderSize[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        /* 3. CornerRadius */

        private sbyte[] cornerRadius = new sbyte[6];
        public sbyte[] CornerRadius
        {
            get { return cornerRadius; }
            set 
            {
                SetProperty(ref cornerRadius, value);
                parentView.UpdatePresenter();
            }
        }
        public sbyte CornerRadiusCell //for XAML
        {
            get { return cornerRadius[RealSelectedCellNumber]; }
            set 
            { 
                SetProperty(ref cornerRadius[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        /* 4. FontColor */
        private SolidColorBrush[] fontColor = new SolidColorBrush[6];
        public SolidColorBrush[] FontColor
        {
            get { return fontColor; }
            set 
            { 
                SetProperty(ref fontColor, value);
                parentView.UpdatePresenter();
            }
        }
        public SolidColorBrush FontColorCell
        {
            get { return fontColor[RealSelectedCellNumber]; }
            set 
            { 
                SetProperty(ref fontColor[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        /* 5. BackgroundColor */
        private SolidColorBrush[] backgroundColor = new SolidColorBrush[6];
        public SolidColorBrush[] BackgroundColor
        {
            get { return backgroundColor; }
            set 
            { 
                SetProperty(ref backgroundColor, value);
                parentView.UpdatePresenter();
            }
        }
        public SolidColorBrush BackgroundColorCell
        {
            get { return backgroundColor[RealSelectedCellNumber]; }
            set 
            { 
                SetProperty(ref backgroundColor[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        /* 6. BorderColor */
        private SolidColorBrush[] borderColor = new SolidColorBrush[6];
        public SolidColorBrush[] BorderColor
        {
            get { return borderColor; }
            set 
            { 
                SetProperty(ref borderColor, value);
                parentView.UpdatePresenter();
            }
        }
        public SolidColorBrush BorderColorCell
        {
            get { return borderColor[RealSelectedCellNumber]; }
            set 
            { 
                SetProperty (ref borderColor[RealSelectedCellNumber], value);
                parentView.UpdatePresenter();
            }
        }

        #endregion


        public void OnCellChange()
        {
            RaisePropertyChanged(nameof(FontSizeCell));
            RaisePropertyChanged(nameof(BorderSizeCell));
            RaisePropertyChanged(nameof(CornerRadiusCell));
            RaisePropertyChanged(nameof(FontColorCell));
            RaisePropertyChanged(nameof(BackgroundColorCell));
            RaisePropertyChanged(nameof(BorderColorCell));
        }

        private void ClickedSetColor(string parameter)
        {
            switch (parameter)
            {
                case "fontColor":
                    FontColorCell = colorPickerInstance.OutcomingColor;
                    break;
                case "backgroundColor":
                    BackgroundColorCell = colorPickerInstance.OutcomingColor;
                    break;
                case "borderColor":
                    BorderColorCell = colorPickerInstance.OutcomingColor;
                    break;
                default: throw new NotImplementedException();
            }
        }

        public bool IsCellModelValid()
        {
            if (ColorsNotNull()) return true;
            else return false;
        }

        private bool ColorsNotNull()
        {
            for (int i = 0; i< parentView.NumberOfCells; i++ )
            {
                if (parentView.CellRecordTypeArray[i] != CellRecordType.Text) continue;
                if (BackgroundColor[i] == null) return false;
                if (FontColor[i] == null) return false;
                if (BorderColor[i] == null) return false;
            }
            return true;
        }
    }
}
