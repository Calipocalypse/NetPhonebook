using NetPhonebook.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class VirtualModelsCustomization
    {
        public Guid Id { get; init; }
        public VirtualModel Model { get; set; }
        public Guid ModelId { get; set; }
        public byte CellId { get; set; }


        public CellRecordType CellType { get; set;}

        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ExtraCategory Category { get; set; }

        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderSize { get; set; }
        public string CornerRadius { get; set; }
        public string FontSize { get; set; }
    }
}
