using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KTL
{
    public partial class Form2 : Form
    {
        private Game game;
        private Color _actualColor;
        private bool? victory;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Game _game)
        {
            game = _game;
            game.NewGame();
            InitializeComponent();
            InitializeColorPanel();
            InitializeNumberPanel();
        }

        private void InitializeColorPanel()
        {
            colorsPanel.Controls.Clear();
            int colors = game.Colors.Count;
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
                    _actualColor = game.Colors[0];
                    actualColorPanel.BackColor = _actualColor;
                }
                for (int i = 0; i < colors; i++)
                {
                    Button control = new Button();
                    control.BackColor = game.Colors[i];
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
                    control.Text = i.ToString();
                    colorsPanel.Controls.Add(control);
                    startX = (startX + colorSize);
                }
            }
        }

        private void InitializeNumberPanel()
        {
            panelNumbers.Controls.Clear();
            int numbers = game.Fields.Count;
            label_n.Text = numbers.ToString();
            label_k.Text = game.K.ToString();
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
                    control.BackColor = game.Fields[i].Color;
                    control.Size = new Size(colorSize, colorSize);
                    if (startX + colorSize >= width)
                    {
                        startX = 0;
                        startY = startY + colorSize;
                    }
                    control.Enabled = game.Fields[i].Enabled;
                    control.Text = (i + 1).ToString();
                    control.Font = new Font(new FontFamily(GenericFontFamilies.Monospace), colorSize / 4);
                    control.ForeColor = Color.FromArgb(255 - control.BackColor.R, 255 - control.BackColor.G, 255 - control.BackColor.B);
                    control.Location = new Point(startX, startY);
                    control.Click += FieldClick;
                    panelNumbers.Controls.Add(control);
                    startX = (startX + colorSize);
                }
            }
        }

        private void FieldClick(object sender, EventArgs e)
        {
            if (victory.HasValue) return;
            var control = (Button)sender;
            game.Fields[panelNumbers.Controls.GetChildIndex(control)] = new Field
            {
                Color = _actualColor,
                Enabled = false
            };
            control.BackColor = _actualColor;
            control.Enabled = false;
            control.ForeColor = Color.FromArgb(255 - control.BackColor.R, 255 - control.BackColor.G, 255 - control.BackColor.B);
            victory = game.VerifyVictory();
            game.ComputerStep();
            InitializeNumberPanel();
        }





        private void Form2_Resize(object sender, EventArgs e)
        {
            InitializeColorPanel();
            InitializeNumberPanel();
        }

        private void hintButton_Click(object sender, EventArgs e)
        {
            game.ShowHint();
        }
    }
}
