using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL
{
    public class Game
    {
        public List<Color> Colors { get; set; }
        public List<Tuple<Color, Boolean>> Fields { get; set; }
        public int K { get; set; }
        public Boolean HumanStarts { get; set; }
        public Int32 Move { get; set; }
    }
}
