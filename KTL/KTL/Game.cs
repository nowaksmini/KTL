using System;
using System.Collections.Generic;
using System.Drawing;

namespace KTL
{
    public class Game
    {
        public List<Color> Colors { get; set; }
        public List<Tuple<Tuple<Color, bool>, bool>> Fields { get; set; }
        public int K { get; set; }
        public int HumanLevel { get; set; }
        public int ComputerLevel { get; set; }
    }
}
