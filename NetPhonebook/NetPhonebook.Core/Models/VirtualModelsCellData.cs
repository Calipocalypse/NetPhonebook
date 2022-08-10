using NetPhonebook.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class VirtualModelsCellData
    {
        public Guid Id { get; set; }
        public VirtualModelsData MainData { get; set;}
        public Guid MainDataId { get; set;}
        public sbyte CellId { get; set; }
        public CellRecordType CellType { get; set; }

        public string FirstText { get; set; }
        public string SecondText { get; set; }

        public ExtraInfo extraInfo { get; set; }
        public Guid? extraInfoId { get ; set; }

        /*
        public bool IsUsingPrefix { get; set; }
        public ExtraInfo Prefix { get; set; }
        public bool IsUsingSuffix { get; set; }
        public ExtraInfo Suffix { get; set; }
        */
        }
}
