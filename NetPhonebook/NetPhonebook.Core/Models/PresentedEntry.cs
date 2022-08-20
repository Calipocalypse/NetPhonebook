using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Models
{
    public class PresentedEntry
    {
        public string DisplayedNumber { get; set; }
        public string SerialNumber { get; set; }
        
        public int FontSize { get; set; }
        public int SecondaryFontSize { get; set; }
        public int CornerRadius { get; set; }
        public int BorderSize { get; set; }

        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush BorderColor { get; set; }
        public SolidColorBrush ForegroundColor { get; set; }

        
        public ObservableCollection<PresentedEntryCell> PresentedCells { get; set; }
    }
}
