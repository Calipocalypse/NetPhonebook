using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class VirtualModelsData
    {
        public Guid Id { get; set; }
        //public Image Image { get; set; }
        public string DisplayedNumber { get; set; }
        public ICollection<VirtualModelsCellData> CellDatas { get; set; }
        //TODO ADD RELATION TO VIRTUAL MODEL AND GET BACK TO RecordEntriesClickerViewModel TO 28 LINE
    }
}
