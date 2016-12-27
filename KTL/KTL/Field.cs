using System;
using System.Drawing;

namespace KTL
{
    public class Field
    {
        private readonly Color EmptyColor = Color.White;
        public Color Color { get; private set; }
        public bool Enabled { get; set; }
        public Field()
        {
            Color = EmptyColor;
            Enabled = true;
        }

        internal void Select(Color color)
        {
            Color = color;
            Enabled = false;
        }
        public void Enable()
        {
            Color = EmptyColor;
            Enabled = true;
        }

        internal bool IsEmpty()
        {
            return Color == EmptyColor;
        }

        internal void Disable(Color _actualColor)
        {
            Color = _actualColor;
            Enabled = false;
        }
    }
}