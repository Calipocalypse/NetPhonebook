using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class VirtualModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        //public Image BaseImage { get; set; }
        //public Guid BaseImageId { get; set; }
        //Image color???
        public ICollection<VirtualModelsCustomization> CustomizationCells { get; set; }
    }
}
