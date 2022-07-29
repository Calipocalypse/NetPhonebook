using NetPhonebook.Core;
using NetPhonebook.Core.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Netphonebook.Modules.Models.ViewModels
{
    internal class CellCreatorTextViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;

        internal CellCreatorTextViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        //private sbyte[] fontSize = new sbyte[6];
        //public sbyte FontSize
        //{
        //    get { return fontSize[SelectedCellNumber - 1]; }
        //    set { SetProperty(ref fontSize[SelectedCellNumber - 1], value); }
        //}
        
        private sbyte[] fontSize = new sbyte[6];
        public sbyte[] FontSize
        {
            get { return fontSize; }
            set { SetProperty(ref fontSize, value); }
        }

        private sbyte[] borderSize = new sbyte[6];
        public sbyte[] BorderSize
        {
            get { return borderSize; }
            set { SetProperty(ref borderSize, value); }
        }

        private sbyte[] cornerRadius = new sbyte[6];
        public sbyte[] CornerRadius
        {
            get { return cornerRadius; }
            set { SetProperty(ref cornerRadius, value); }
        }

        private SolidColorBrush[] fontColor = new SolidColorBrush[6];
        public SolidColorBrush[] FontColor
        {
            get { return fontColor; }
            set { SetProperty(ref fontColor, value); }
        }
        private SolidColorBrush[] backgroundColor = new SolidColorBrush[6];
        public SolidColorBrush[] BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }

        private SolidColorBrush[] borderColor = new SolidColorBrush[6];
        public SolidColorBrush[] BorderColor
        {
            get { return borderColor; }
            set { SetProperty(ref borderColor, value); }
        }
    }
}
