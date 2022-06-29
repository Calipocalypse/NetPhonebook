using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class VirtualModelsCustomization
    {
        public Guid Id { get; init; }
        public VirtualModel Model{ get; set; }
        public Guid ModelId { get; set; }
        public sbyte CellId { get; set; }

        public ExtraCategory Category { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderSize { get; set; }
        public string CornerRadius { get; set; }
        public string FontSize { get; set; }
    }
}
