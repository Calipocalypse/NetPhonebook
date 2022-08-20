using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Models
{
    public class VirtualModelsData
    {
        public Guid Id { get; set; }
        //public Image Image { get; set; }


        public string DisplayedNumber { get; set; }
        public string SerialNumber { get; set; }

        public VirtualModel ModelBase { get; set; }
        public Guid ModelBaseId { get; set; }


        public ICollection<VirtualModelsCellData> CellDatas { get; set; }
    }
}
