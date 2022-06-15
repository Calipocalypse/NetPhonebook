using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    [Table("ExtraCategories")]
    public class ExtraCategory
    {
        public ExtraCategory(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
