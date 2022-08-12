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
        public string SerialNumber { get; set; }
        public ICollection<VirtualModelsCellData> CellDatas { get; set; }
        public VirtualModel ModelBase { get; set; }
        public Guid ModelBaseId { get; set; }
    }
}
