using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Models
{
    public class PresentedEntryCell
    {
        public int CellId { get; set; }
        public string Text { get; set; }

        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush ForegroundColor { get; set; }
        public SolidColorBrush BorderColor { get; set; }

        public int BorderSize { get; set; }
        public int CornerRadius { get; set; }
        public int FontSize { get; set; }
    }
}
