using System;
using System.Drawing;
using System.Windows.Forms;

namespace LassoGame
{
    public partial class Form1 : Form
    {
        int counter = 0;
        Button[] lasso;
        bool reversed = false;
        private enum Dir : byte { BottomRight, BottomLeft, TopLeft, TopRight, }
        private Dir[] lassoDir = new Dir[12];
        public Point lassoStartPos = new Point(760, 100);

        public void CreateLasso()
        {
            lasso = new Button[12];
            for (int i = 0; i < lasso.Length; ++i)
            {
                Button lassoPart = new Button();

                lassoPart.FlatStyle = FlatStyle.Flat;
                lassoPart.FlatAppearance.BorderSize = 0;
                lassoPart.Tag = i;
                lassoPart.SetBounds(lassoStartPos.X + 50 * i, lassoStartPos.Y + 50 * i, 60, 60);

                if (i < 5 || i == 6)
                    lassoPart.BackColor = lassoPart.FlatAppearance.MouseDownBackColor =
                        lassoPart.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
                else
                    lassoPart.BackColor = lassoPart.FlatAppearance.MouseDownBackColor =
                        lassoPart.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;

                lasso[i] = lassoPart;
                Controls.Add(lasso[i]);
                lasso[i].BringToFront();
                lasso[i].Click += LassoClick;
            }
        }
        public Form1()
        {
            InitializeComponent();
            CreateLasso();
        }

        private void LassoClick(object sender, EventArgs e)
        {
            ++counter;
            score.Text = Convert.ToString(counter);

            Button lassoPart = sender as Button;
            int ind = Convert.ToInt32(lassoPart.Tag);
            int colour = lassoPart.BackColor == SystemColors.ControlDark ? 1 : -1;
            int offset = Math.Abs(lasso[1].Top - lasso[0].Top);

            if (!reversed)
                for (int i = ind + 1; i < lasso.Length; i++)
                    switch (lassoDir[i - 1])
                    {
                        case Dir.BottomRight:
                            {
                                lasso[i].Left = lasso[i - 1].Left - colour * offset;
                                lasso[i].Top = lasso[i - 1].Top + colour * offset;
                                lassoDir[i - 1] = colour == 1 ? Dir.BottomLeft : Dir.TopRight;
                                break;
                            }
                        case Dir.BottomLeft:
                            {
                                lasso[i].Left = lasso[i - 1].Left - colour * offset;
                                lasso[i].Top = lasso[i - 1].Top - colour * offset;
                                lassoDir[i - 1] = colour == 1 ? Dir.TopLeft : Dir.BottomRight;
                                break;
                            }
                        case Dir.TopLeft:
                            {
                                lasso[i].Left = lasso[i - 1].Left + colour * offset;
                                lasso[i].Top = lasso[i - 1].Top - colour * offset;
                                lassoDir[i - 1] = colour == 1 ? Dir.TopRight : Dir.BottomLeft;
                                break;
                            }
                        case Dir.TopRight:
                            {
                                lasso[i].Left = lasso[i - 1].Left + colour * offset;
                                lasso[i].Top = lasso[i - 1].Top + colour * offset;
                                lassoDir[i - 1] = colour == 1 ? Dir.BottomRight : Dir.TopLeft;
                                break;
                            }
                    }
            else
            {
                colour *= -1;
                for (int i = ind - 1; i >= 0; i--)
                    switch ((Dir)lassoDir[i])
                    {
                        case Dir.BottomRight:
                            {
                                lasso[i].Left = lasso[i + 1].Left - colour * offset;
                                lasso[i].Top = lasso[i + 1].Top + colour * offset;
                                lassoDir[i] = colour == 1 ? Dir.TopRight : Dir.BottomLeft;
                                break;
                            }
                        case Dir.BottomLeft:
                            {
                                lasso[i].Left = lasso[i + 1].Left - colour * offset;
                                lasso[i].Top = lasso[i + 1].Top - colour * offset;
                                lassoDir[i] = colour == 1 ? Dir.BottomRight : Dir.TopLeft;
                                break;
                            }
                        case Dir.TopLeft:
                            {
                                lasso[i].Left = lasso[i + 1].Left + colour * offset;
                                lasso[i].Top = lasso[i + 1].Top - colour * offset;
                                lassoDir[i] = colour == 1 ? Dir.BottomLeft : Dir.TopRight;
                                break;
                            }
                        case Dir.TopRight:
                            {
                                lasso[i].Left = lasso[i + 1].Left + colour * offset;
                                lasso[i].Top = lasso[i + 1].Top + colour * offset;
                                lassoDir[i] = colour == 1 ? Dir.TopLeft : Dir.BottomRight;
                                break;
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
                lasso[i].Left = lassoStartPos.X + 50 * i;
                lasso[i].Top = lassoStartPos.Y + 50 * i;
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
