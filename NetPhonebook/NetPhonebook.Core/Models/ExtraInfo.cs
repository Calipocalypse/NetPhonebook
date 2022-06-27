using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{   
    [Table("ExtraInfos")]
    public class ExtraInfo
    {
        [Key]
        public Guid Id { get; init; }
        public ExtraCategory ExtraCategory { get; set; }
        public Guid ExtraCategoryId { get; set; }
        public string Name { get; set; }
    }
}
