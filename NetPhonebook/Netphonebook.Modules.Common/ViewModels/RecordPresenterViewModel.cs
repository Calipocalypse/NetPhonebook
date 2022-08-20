using NetPhonebook.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Netphonebook.Modules.Common.ViewModels
{
    public class RecordPresenterViewModel : BindableBase
    {
        private ObservableCollection<PresentedEntry> presentedCollection = new ObservableCollection<PresentedEntry>();
        public ObservableCollection<PresentedEntry> PresentedCollection
        {
            get { return presentedCollection; }
            set { SetProperty(ref presentedCollection, value); }
        }

        public RecordPresenterViewModel()
        {
            PresentedCollection = new ObservableCollection<PresentedEntry>();
        }

        public void CleanCollection()
        {
            PresentedCollection = new ObservableCollection<PresentedEntry>();
        }

        public void AddToCollection(string displayedNumber, string serialNumber, 
            SolidColorBrush BackgroundColor, SolidColorBrush BorderColor, SolidColorBrush ForegroundColor, int FontSize, int CornerRadius, int BorderSize,
            string[] texts, 
            SolidColorBrush[] backgroundColors, SolidColorBrush[] foregroundColors, SolidColorBrush[] borderColors,
            int[] borderSizes, int[] cornerRadius, int[] fontSize)
        {
            /* 1. Creating cells */
            ObservableCollection<PresentedEntryCell> cells = new ObservableCollection<PresentedEntryCell>();

            for (int i = 0; i < texts.Count(); i++)
            {
                var newCell = new PresentedEntryCell
                {
                    CellId = i,
                    Text = texts[i],
                    BackgroundColor = backgroundColors[i],
                    ForegroundColor = foregroundColors[i],
                    BorderColor = borderColors[i],
                    BorderSize = borderSizes[i],
                    CornerRadius = cornerRadius[i],
                    FontSize = fontSize[i]
                };

                cells.Add(newCell);
            }

            /* 2. Creating full entry and attaching cells*/
            var newEntry = new PresentedEntry
            {
                DisplayedNumber = displayedNumber,
                SerialNumber = serialNumber,

                FontSize = FontSize,
                SecondaryFontSize = FontSize*2/3,
                CornerRadius = CornerRadius,
                BorderSize = BorderSize,
                BackgroundColor = BackgroundColor,
                ForegroundColor = ForegroundColor,
                BorderColor = BorderColor,

                PresentedCells = cells
            };

            PresentedCollection.Add(newEntry);
        }
    }
}
