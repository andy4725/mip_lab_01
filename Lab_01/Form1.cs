using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WebBrowser
{
    public partial class Form1 : Form
    {
        private readonly System.Windows.Forms.Timer colorTimer =
        new System.Windows.Forms.Timer();
        private readonly Random random = new Random();
        private bool showShapes;

        public Form1()
        {
            InitializeComponent();

            colorTimer.Interval = 1000;
            colorTimer.Tick += ColorTimer_Tick;
            ButtonLabel.MouseEnter += ButtonLabel_MouseEnter;
            UpButton.MouseEnter += DirectionButton_MouseEnter;
            DownButton.MouseEnter += DirectionButton_MouseEnter;
            LeftButton.MouseEnter += DirectionButton_MouseEnter;
            RightButton.MouseEnter += DirectionButton_MouseEnter;
            ChangeButton.Click += ChangeButton_Click;
            ChangeButton2.Click += ChangeButton2_Click;
            SmallTextButton.Click += SmallTextButton_Click;
            LargeTextButton.Click += LargeTextButton_Click;
            this.Paint += Form1_Paint;
            RandomShapesButton.Click += RandomShapesButton_Click;
            shapesTimer.Tick += ShapesTimer_Tick;
        }

        private readonly System.Windows.Forms.Timer shapesTimer =
    new System.Windows.Forms.Timer();

        private readonly List<RandomShape> randomShapes = new List<RandomShape>();

        // Reuse your existing Random field if you already have one.

        private class RandomShape
        {
            public Rectangle Bounds;
            public Color Color;
            public int Type; // 0 = rectangle, 1 = ellipse, 2 = triangle
        }

        private void RandomShapesButton_Click(object sender, EventArgs e)
        {
            shapesTimer.Start();
        }

        private void ShapesTimer_Tick(object sender, EventArgs e)
        {
            int width = random.Next(30, 81);
            int height = random.Next(30, 81);

            randomShapes.Add(new RandomShape
            {
                Type = random.Next(3),
                Color = Color.Red, // Use a visible color while checking.
                Bounds = new Rectangle(
                    random.Next(Math.Max(1, ClientSize.Width - width)),
                    random.Next(Math.Max(1, ClientSize.Height - height)),
                    width,
                    height)
            });

            Invalidate();
        }

        private void CreateShapesButton_Click(object sender, EventArgs e)
        {
            showShapes = true;
            Invalidate(); // Requests that the form repaint
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Draw the original fixed shapes.
            if (showShapes)
            {
                using (Brush brush = new SolidBrush(Color.CornflowerBlue))
                {
                    graphics.FillRectangle(brush, 50, 50, 120, 80);

                    Point[] triangle =
                    {
                new Point(280, 50),
                new Point(220, 130),
                new Point(340, 130)
            };

                    graphics.FillPolygon(brush, triangle);
                }
            }

            // Draw every random shape currently stored.
            foreach (RandomShape shape in randomShapes)
            {
                using (Brush brush = new SolidBrush(shape.Color))
                {
                    switch (shape.Type)
                    {
                        case 0:
                            graphics.FillRectangle(brush, shape.Bounds);
                            break;

                        case 1:
                            graphics.FillEllipse(brush, shape.Bounds);
                            break;

                        case 2:
                            Point[] triangle =
                            {
                        new Point(
                            shape.Bounds.Left + shape.Bounds.Width / 2,
                            shape.Bounds.Top),
                        new Point(shape.Bounds.Left, shape.Bounds.Bottom),
                        new Point(shape.Bounds.Right, shape.Bounds.Bottom)
                    };

                            graphics.FillPolygon(brush, triangle);
                            break;
                    }
                }
            }
        }

        private void TextUpButton_Click(object sender, EventArgs e)
        {
            UpDownText.Top = 0;
        }

        private void TextDownButton_Click(object sender, EventArgs e)
        {
            UpDownText.Top = 100;
        }

        private void SmallTextButton_Click(object sender, EventArgs e)
        {
            UpDownText.Font = new Font(UpDownText.Font.FontFamily, 8);
        }

        private void LargeTextButton_Click(object sender, EventArgs e)
        {
            UpDownText.Font = new Font(UpDownText.Font.FontFamily, 18);
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            ChangeButton2.Text = "First button was clicked";
            ChangeButton2.BackColor = Color.LightGreen;
        }

        private void ChangeButton2_Click(object sender, EventArgs e)
        {
            ChangeButton.Text = "Second button was clicked";
            ChangeButton.BackColor = Color.LightBlue;
        }

        private void DirectionButton_MouseEnter(object sender, EventArgs e)
        {
            var button = (System.Windows.Forms.Button)sender;
            int step = 15;
            int x = Left;
            int y = Top;

            switch (button.Text.Trim().ToLower())
            {
                case "up": y -= step; break;
                case "down": y += step; break;
                case "left": x -= step; break;
                case "right": x += step; break;
            }

            Rectangle area = Screen.FromControl(this).WorkingArea;
            x = Math.Max(area.Left, Math.Min(x, area.Right - Width));
            y = Math.Max(area.Top, Math.Min(y, area.Bottom - Height));

            Location = new Point(x, y);
        }

        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256)
             );
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            ButtonLabel.Text = $"{UpButton.Text}";

            DialogResult result = MessageBox.Show(
                "What would you like to do?",
                "Choose an option",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Handle Yes
            }
            else if (result == DialogResult.No)
            {
                // Handle No
            }
            else
            {
                // Handle Cancel or closing the dialog
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {

        }

        private void LeftButton_Click(object sender, EventArgs e)
        {

        }

        private void RightButton_Click(object sender, EventArgs e)
        {

        }

        private void ButtonLabel_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            colorTimer.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            colorTimer.Stop();
        }

        private void ButtonLabel_MouseEnter(object sender, EventArgs e)
        {
            ButtonLabel.Left += 20;
        }

        private void DirectionButton_Click(object sender, EventArgs e)
        {
            Rectangle area = Screen.FromControl(this).WorkingArea;
            System.Windows.Forms.Button button =
                (System.Windows.Forms.Button)sender;

            ButtonLabel.Text = $"{button.Text}";
            int step = 20;

            switch (button.Name)
            {

                case "UpButton":
                    this.Location = new Point(area.Left, area.Top);
                    break;

                case "RightButton":
                    this.Location = new Point(area.Right - this.Width, area.Top);
                    break;

                case "DownButton":
                    this.Location = new Point(area.Right - this.Width, area.Top);
                    break;

                case "LeftButton":
                    this.Location = new Point(area.Left, area.Bottom - this.Height);
                    break;
            }

            switch (button.Text.Trim().ToLower())
            {
                case "up":
                    ButtonLabel.Top -= step;
                    break;

                case "down":
                    ButtonLabel.Top += step;
                    break;

                case "left":
                    ButtonLabel.Left -= step;
                    break;

                case "right":
                    ButtonLabel.Left += step;
                    break;
            }

        }
    }
}
