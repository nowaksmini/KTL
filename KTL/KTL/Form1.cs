using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTL
{
    public partial class Form1 : Form
    {
        private Game _game;

        public Form1()
        {
            InitializeComponent();
            _game = new Game();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            colorsPanel.Controls.Clear();
            List<Color> colorList = new List<Color>();
            if (textBox3.Text != null && textBox3.Text.Length != 0)
            {
                int colors = Int32.Parse(textBox3.Text);
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
                    for (int i = 0; i < colors; i++)
                    {
                        Control control = new Control();
                        control.BackColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                        colorList.Add(control.BackColor);
                        control.Size = new Size(colorSize, colorSize);
                        if (startX + colorSize >= width)
                        {
                            startX = 0;
                            startY = startY + colorSize;
                        }
                        control.Location = new Point(startX, startY);
                        colorsPanel.Controls.Add(control);
                        startX = (startX + colorSize);
                    }
                }
            }
            _game.Colors = colorList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.Length != 0)
            {
                _game.Fields = new List<Tuple<Color, bool>>();
                int n = Int32.Parse(textBox1.Text);
                for (int i = 0; i < n; i++)
                {
                    _game.Fields.Add(Tuple.Create(Color.White, true));
                }
            }
            else
            {
                _game.Fields = new List<Tuple<Color, bool>>();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != null && textBox2.Text.Length != 0)
            {
                _game.K = Int32.Parse(textBox2.Text);
            }
            else
            {
                _game.K = 0;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            colorsPanel.Controls.Clear();
            int colors = _game.Colors.Count;
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
                for (int i = 0; i < colors; i++)
                {
                    Control control = new Control();
                    control.BackColor = _game.Colors[i];
                    control.Size = new Size(colorSize, colorSize);
                    if (startX + colorSize >= width)
                    {
                        startX = 0;
                        startY = startY + colorSize;
                    }
                    control.Location = new Point(startX, startY);
                    colorsPanel.Controls.Add(control);
                    startX = (startX + colorSize);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2(_game);
            form2.Closed += (s, args) => this.Show();
            form2.Show();
            this.Hide();
        }
    }

}
