using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KTL
{
    public partial class Form2 : Form
    {
        private Game _game;
        private Color _actualColor;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Game _game)
        {
            this._game = _game;
            InitializeComponent();
            InitializeColorPanel();
            InitializeNumberPanel();
        }

        private void InitializeColorPanel()
        {
            colorsPanel.Controls.Clear();
            int colors = _game.Colors.Count;
            label_c.Text = colors.ToString();
            if (colors > 0)
            {
                int width = colorsPanel.Width;
                int height = colorsPanel.Height;
                int startX = 0;
                int startY = 0;
                int colorSize = (int)Math.Floor(Math.Sqrt((width * height / colors)));
                while ((int)(width / colorSize) * (int)(height / colorSize) < colors)
                {
                    colorSize--;
                }
                Random r = new Random();
                if (_actualColor.A == 0)
                {
                    _actualColor = _game.Colors[0];
                    actualColorPanel.BackColor = _actualColor;
                }
                for (int i = 0; i < colors; i++)
                {
                    Button control = new Button();
                    control.BackColor = _game.Colors[i];
                    control.Size = new Size(colorSize, colorSize);
                    if (startX + colorSize >= width)
                    {
                        startX = 0;
                        startY = startY + colorSize;
                    }
                    control.Click += (s, args) =>
                    {
                        _actualColor = control.BackColor; actualColorPanel.BackColor = _actualColor;
                    };
                    control.ForeColor = Color.FromArgb(255 - control.BackColor.R, 255 - control.BackColor.G, 255 - control.BackColor.B);
                    control.Location = new Point(startX, startY);
                    colorsPanel.Controls.Add(control);
                    startX = (startX + colorSize);
                }
            }
        }

        private void InitializeNumberPanel()
        {
            panelNumbers.Controls.Clear();
            int numbers = _game.Fields.Count;
            label_n.Text = numbers.ToString();
            label_k.Text = _game.K.ToString();
            if (numbers > 0)
            {
                int width = panelNumbers.Width;
                int height = panelNumbers.Height;
                int startX = 0;
                int startY = 0;
                int colorSize = (int)Math.Floor(Math.Sqrt((width * height / numbers)));
                while ((int)(width / colorSize) * (int)(height / colorSize) < numbers)
                {
                    colorSize--;
                }
                Random r = new Random();
                for (int i = 0; i < numbers; i++)
                {
                    Button control = new Button();
                    control.BackColor = _game.Fields[i].Item1;
                    control.Size = new Size(colorSize, colorSize);
                    if (startX + colorSize >= width)
                    {
                        startX = 0;
                        startY = startY + colorSize;
                    }
                    control.Enabled = _game.Fields[i].Item2;
                    control.Text = (i + 1).ToString();
                    control.Font = new Font(new FontFamily(GenericFontFamilies.Monospace), colorSize / 4);
                    control.ForeColor = Color.FromArgb(255 - control.BackColor.R, 255 - control.BackColor.G, 255 - control.BackColor.B);
                    control.Location = new Point(startX, startY);
                    control.Click += (s, args) =>
                    {
                        _game.Fields[panelNumbers.Controls.GetChildIndex(control)] = Tuple.Create(_actualColor, false);
                        control.BackColor = _actualColor;
                        control.Enabled = false;
                        control.ForeColor = Color.FromArgb(255 - control.BackColor.R, 255 - control.BackColor.G, 255 - control.BackColor.B);
                    };
                    panelNumbers.Controls.Add(control);
                    startX = (startX + colorSize);
                }
            }
        }

        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
            InitializeColorPanel();
            InitializeNumberPanel();
        }
    }
}
