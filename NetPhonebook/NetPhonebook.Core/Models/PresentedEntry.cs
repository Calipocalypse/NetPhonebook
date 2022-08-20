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
        public ObservableCollection<PresentedEntryCell> PresentedCells { get; set; }
    }
}
