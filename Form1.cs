using System;
using System.Drawing;
using System.Windows.Forms;

namespace LassoGame
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        private readonly Button[] lasso = new Button[12];
        private readonly Button[] horses = new Button[7];
        private bool reversed = false;
        private enum Dir : byte { BottomRight, BottomLeft, TopLeft, TopRight, }
        private readonly Dir[] lassoDir = new Dir[11];
        private Point lassoStartPos = new Point(760, 100);
        private const byte offset = 50, w = 60;

        private void CreateHorses()
        {
            for (byte i = 0; i < horses.Length; ++i)
            {
                horses[i] = new Button();
                horses[i].Width = horses[i].Height = w;
                horses[i].FlatStyle = FlatStyle.Flat;
                horses[i].FlatAppearance.BorderSize = 0;
                horses[i].BackColor = horses[i].FlatAppearance.MouseDownBackColor =
                    horses[i].FlatAppearance.MouseOverBackColor = Color.SandyBrown;
                Controls.Add(horses[i]);
            }
            horses[0].Location = new Point(lassoStartPos.X - offset * 13, lassoStartPos.Y + offset);
            horses[1].Location = new Point(horses[0].Left + offset * 2, horses[0].Top + offset * 2);
            horses[2].Location = new Point(horses[1].Left + offset, horses[1].Top - offset * 3);
            horses[3].Location = new Point(horses[2].Left + offset, horses[1].Top);
            horses[4].Location = new Point(horses[2].Left + offset * 2, horses[2].Top);
            horses[5].Location = new Point(horses[4].Left, horses[3].Top + offset);
            horses[6].Location = new Point(horses[5].Left + offset * 2, horses[0].Top + offset);
        }

        private void CreateLasso()
        {
            for (int i = 0; i < lasso.Length; ++i)
            {
                lasso[i] = new Button();

                lasso[i].FlatStyle = FlatStyle.Flat;
                lasso[i].FlatAppearance.BorderSize = 0;
                lasso[i].Tag = i;
                lasso[i].SetBounds(lassoStartPos.X + offset * i, lassoStartPos.Y + offset * i, w, w);

                if (i < 5 || i == 6)
                    lasso[i].BackColor = lasso[i].FlatAppearance.MouseDownBackColor =
                        lasso[i].FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
                else
                    lasso[i].BackColor = lasso[i].FlatAppearance.MouseDownBackColor =
                        lasso[i].FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;

                Controls.Add(lasso[i]);
                lasso[i].Click += LassoClick;
            }
        }
        public Form1()
        {
            InitializeComponent();
            CreateLasso();
            CreateHorses();
        }

        private void LassoClick(object sender, EventArgs e)
        {
            ++counter;
            score.Text = Convert.ToString(counter);

            Button lassoPart = sender as Button;
            int ind = Convert.ToInt32(lassoPart.Tag);

            if (lassoPart.BackColor == SystemColors.ControlDark)
            {
                if (!reversed)
                {
                    for (int i = ind + 1; i < lasso.Length; i++)
                        switch (lassoDir[i - 1])
                        {
                            case Dir.BottomRight:
                                {
                                    lasso[i].Left = lasso[i - 1].Left - offset;
                                    lasso[i].Top = lasso[i - 1].Top + offset;
                                    lassoDir[i - 1] = Dir.BottomLeft;
                                    break;
                                }
                            case Dir.BottomLeft:
                                {
                                    lasso[i].Left = lasso[i - 1].Left - offset;
                                    lasso[i].Top = lasso[i - 1].Top - offset;
                                    lassoDir[i - 1] = Dir.TopLeft;
                                    break;
                                }
                            case Dir.TopLeft:
                                {
                                    lasso[i].Left = lasso[i - 1].Left + offset;
                                    lasso[i].Top = lasso[i - 1].Top - offset;
                                    lassoDir[i - 1] = Dir.TopRight;
                                    break;
                                }
                            case Dir.TopRight:
                                {
                                    lasso[i].Left = lasso[i - 1].Left + offset;
                                    lasso[i].Top = lasso[i - 1].Top + offset;
                                    lassoDir[i - 1] = Dir.BottomRight;
                                    break;
                                }
                        }
                }
                else
                {
                    for (int i = ind - 1; i >= 0; i--)
                        switch (lassoDir[i])
                        {
                            case Dir.BottomRight:
                                {
                                    lasso[i].Left = lasso[i + 1].Left + offset;
                                    lasso[i].Top = lasso[i + 1].Top - offset;
                                    lassoDir[i] = Dir.BottomLeft;
                                    break;
                                }
                            case Dir.BottomLeft:
                                {
                                    lasso[i].Left = lasso[i + 1].Left + offset;
                                    lasso[i].Top = lasso[i + 1].Top + offset;
                                    lassoDir[i] = Dir.TopLeft;
                                    break;
                                }
                            case Dir.TopLeft:
                                {
                                    lasso[i].Left = lasso[i + 1].Left - offset;
                                    lasso[i].Top = lasso[i + 1].Top + offset;
                                    lassoDir[i] = Dir.TopRight;
                                    break;
                                }
                            case Dir.TopRight:
                                {
                                    lasso[i].Left = lasso[i + 1].Left - offset;
                                    lasso[i].Top = lasso[i + 1].Top - offset;
                                    lassoDir[i] = Dir.BottomRight;
                                    break;
                                }

                        }
                }
            }
            else
            {
                if (!reversed)
                {
                    for (int i = ind + 1; i < lasso.Length; i++)
                        switch (lassoDir[i - 1])
                        {
                            case Dir.BottomRight:
                                {
                                    lasso[i].Left = lasso[i - 1].Left + offset;
                                    lasso[i].Top = lasso[i - 1].Top - offset;
                                    lassoDir[i - 1] = Dir.TopRight;
                                    break;
                                }
                            case Dir.BottomLeft:
                                {
                                    lasso[i].Left = lasso[i - 1].Left + offset;
                                    lasso[i].Top = lasso[i - 1].Top + offset;
                                    lassoDir[i - 1] = Dir.BottomRight;
                                    break;
                                }
                            case Dir.TopLeft:
                                {
                                    lasso[i].Left = lasso[i - 1].Left - offset;
                                    lasso[i].Top = lasso[i - 1].Top + offset;
                                    lassoDir[i - 1] = Dir.BottomLeft;
                                    break;
                                }
                            case Dir.TopRight:
                                {
                                    lasso[i].Left = lasso[i - 1].Left - offset;
                                    lasso[i].Top = lasso[i - 1].Top - offset;
                                    lassoDir[i - 1] = Dir.TopLeft;
                                    break;
                                }
                        }
                }
                else
                {
                    for (int i = ind - 1; i >= 0; i--)
                        switch (lassoDir[i])
                        {
                            case Dir.BottomRight:
                                {
                                    lasso[i].Left = lasso[i + 1].Left - offset;
                                    lasso[i].Top = lasso[i + 1].Top + offset;
                                    lassoDir[i] = Dir.TopRight;
                                    break;
                                }
                            case Dir.BottomLeft:
                                {
                                    lasso[i].Left = lasso[i + 1].Left - offset;
                                    lasso[i].Top = lasso[i + 1].Top - offset;
                                    lassoDir[i] = Dir.BottomRight;
                                    break;
                                }
                            case Dir.TopLeft:
                                {
                                    lasso[i].Left = lasso[i + 1].Left + offset;
                                    lasso[i].Top = lasso[i + 1].Top - offset;
                                    lassoDir[i] = Dir.BottomLeft;
                                    break;
                                }
                            case Dir.TopRight:
                                {
                                    lasso[i].Left = lasso[i + 1].Left + offset;
                                    lasso[i].Top = lasso[i + 1].Top + offset;
                                    lassoDir[i] = Dir.TopLeft;
                                    break;
                                }

                        }
                }
            }

        }

        private void RestartGame(object sender, EventArgs e)
        {
            reversed = false;
            score.Text = Convert.ToString(0);
            counter = 0;
            for (int i = 0; i < lassoDir.Length; ++i)
            {
                lassoDir[i] = Dir.BottomRight;
            }
            for (int i = 0; i < lasso.Length; i++)
            {
                lasso[i].BringToFront();
            }
            for (int i = 0; i < lasso.Length; i++)
            {
                lasso[i].Left = lassoStartPos.X + offset * i;
                lasso[i].Top = lassoStartPos.Y + offset * i;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (!reversed)
                for (int i = lasso.Length - 1; i >= 0; --i)
                {
                    lasso[i].BringToFront();
                }
            else
                for (int i = 0; i < lasso.Length; ++i)
                {
                    lasso[i].BringToFront();
                }
            reversed = !reversed;
        }
    }
}
