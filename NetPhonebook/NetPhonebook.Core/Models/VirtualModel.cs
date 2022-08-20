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

        public string FontSize { get; set; }
        public string CornerRadius { get; set; }
        public string BorderSize { get; set; }

        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string ForegroundColor { get; set; }

        public List<VirtualModelsCustomization> CustomizationCells { get; set; }
    }
}
