using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KTL
{
    public partial class Form1 : Form
    {
        private Game _game;
        private Button[] _computerLevelButtons;
        private Button[] _humanLevelButtons;

        public Form1()
        {
            InitializeComponent();
            _game = new Game();
            _computerLevelButtons = new Button[5] { button7, button8, button9, button10, button11 };
            _humanLevelButtons = new Button[5] { button2, button3, button4, button5, button6 };
            _game.ComputerLevel = 2;
            _game.HumanLevel = 2;
            UpdateComputerLevel();
            UpdateHumanLevel();
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
            CheckButtonStart();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.Length != 0)
            {
                _game.Fields = new List<Field>();
                int n = Int32.Parse(textBox1.Text);
                for (int i = 0; i < n; i++)
                {
                    _game.Fields.Add(new Field());
                }
            }
            else
            {
                _game.Fields = new List<Field>();
            }
            CheckButtonStart();
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
            CheckButtonStart();
        }

        private void CheckButtonStart()
        {
            if (_game == null)
            {
                button1.Enabled = false;
            }
            else
            {
                if (_game.K > 0 && _game.Colors != null && _game.Colors.Count > 0 && _game.Fields != null
                    && _game.Fields.Count > 0)
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            colorsPanel.Controls.Clear();
            int colors = _game.Colors != null ? _game.Colors.Count : 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            _game.HumanLevel = 1;
            UpdateHumanLevel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _game.HumanLevel = 2;
            UpdateHumanLevel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _game.HumanLevel = 3;
            UpdateHumanLevel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _game.HumanLevel = 4;
            UpdateHumanLevel();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _game.HumanLevel = 5;
            UpdateHumanLevel();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _game.ComputerLevel = 1;
            UpdateComputerLevel();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _game.ComputerLevel = 2;
            UpdateComputerLevel();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _game.ComputerLevel = 3;
            UpdateComputerLevel();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _game.ComputerLevel = 4;
            UpdateComputerLevel();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _game.ComputerLevel = 5;
            UpdateComputerLevel();
        }

        private void UpdateComputerLevel()
        {
            for (int i = 0; i < _computerLevelButtons.Length; i++)
            {
                _computerLevelButtons[i].BackColor = i + 1 == _game.ComputerLevel ? Color.CadetBlue : Color.WhiteSmoke;
            }
        }

        private void UpdateHumanLevel()
        {
            for (int i = 0; i < _humanLevelButtons.Length; i++)
            {
                _humanLevelButtons[i].BackColor = i + 1 == _game.HumanLevel ? Color.CadetBlue : Color.WhiteSmoke;
            }
        }
    }

}
