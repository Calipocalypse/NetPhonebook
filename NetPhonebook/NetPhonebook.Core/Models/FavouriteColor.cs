using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Models
{
    public class FavouriteColor
    {
        public Guid Id { get; init; }
        public string HexColor { get; set; }
        public SolidColorBrush SolidColorBrush  =>  HexColorConverter.ToSolidColor(HexColor);
    }
}
