using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmVorm : Form
    {
        List<string> actions = new List<string> { "+", "-", "*", "/" };
        Label lbl, plusLeftLabel, plusRightLabel, minusLeftLabel, minusRightLabel,
            multiplicationLeft, multiplicationRight, divisionLeft, divisionRight,
            equals, signs;
        TableLayoutPanel tlp;
        Button close, start;
        System.Timers.Timer timer;
        NumericUpDown numeric1, numeric2, numeric3, numeric4;
        FlowLayoutPanel flp;

        Random random = new Random();

        //Для появления рандомных чисел, вместо вопросительных знаков
        // These integer variables store the numbers 
        // for the addition problem. 
        int addend1;
        int addend2;

        // These integer variables store the numbers 
        // for the subtraction problem. 
        int minuend;
        int subtrahend;

        // These integer variables store the numbers 
        // for the multiplication problem. 
        int multiplicand;
        int multiplier;

        // These integer variables store the numbers 
        // for the division problem. 
        int dividend;
        int divisor;


        public KolmVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";
            this.BackColor = Color.LightSkyBlue;

            //TableLayoutPanel
            tlp = new TableLayoutPanel();
            tlp.BorderStyle = BorderStyle.FixedSingle;
            tlp.AutoSize = true;
            tlp.Location = new Point(0, 100);
            tlp.ColumnCount = 5;
            tlp.RowCount = 4;

            for (int i = 0; i < 5; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            }
            for (int i = 0; i < 4; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            }

            //FlowLayoutPanel
            flp= new FlowLayoutPanel();
            flp.FlowDirection = FlowDirection.RightToLeft;
            flp.Dock = DockStyle.Top;
            flp.Controls.Add(tlp);

            //NumericUpDown
            //numeric1
            numeric1 = new NumericUpDown();
            numeric1.Font = new Font("Calibri", 18, FontStyle.Regular);
            numeric1.Width = 100;
            numeric1.Text = "sum";
            numeric1.Enter += Numeric1_Enter;

            //numeric2
            numeric2 = new NumericUpDown();
            numeric2.Font = new Font("Calibri", 18, FontStyle.Regular);
            numeric2.Width = 100;
            numeric2.Text = "min";
            numeric2.Enter += Numeric2_Enter;

            //numeric3
            numeric3 = new NumericUpDown();
            numeric3.Font = new Font("Calibri", 18, FontStyle.Regular);
            numeric3.Width = 100;
            numeric3.Text = "umn";
            numeric3.Enter += Numeric3_Enter;

            //numeric4
            numeric4 = new NumericUpDown();
            numeric4.Font = new Font("Calibri", 18, FontStyle.Regular);
            numeric4.Width = 100;
            numeric4.Text = "del";
            numeric4.Enter += Numeric4_Enter;

            tlp.Controls.Add(numeric1);
            tlp.SetCellPosition(numeric1, new TableLayoutPanelCellPosition(4, 0));
            tlp.Controls.Add(numeric2);
            tlp.SetCellPosition(numeric2, new TableLayoutPanelCellPosition(4, 1));
            tlp.Controls.Add(numeric3);
            tlp.SetCellPosition(numeric3, new TableLayoutPanelCellPosition(4, 2));
            tlp.Controls.Add(numeric4);
            tlp.SetCellPosition(numeric4, new TableLayoutPanelCellPosition(4, 3));

            //Time Left - lbl
            lbl = new Label();
            lbl.Name = "Time Left";
            lbl.AutoSize = false;
            lbl.Size = new Size(60, 50);
            lbl.Font = new Font("Arial", 18, FontStyle.Italic);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Location = new Point(50, 75);

            //Plus Left
            plusLeftLabel = new Label();
            plusLeftLabel.AutoSize = false;
            plusLeftLabel.Dock = DockStyle.Fill;
            plusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusLeftLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            plusLeftLabel.Text = "?";

            //Plus Right
            plusRightLabel = new Label();
            plusRightLabel.AutoSize = false;
            plusRightLabel.Dock = DockStyle.Fill;
            plusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusRightLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            plusRightLabel.Text = "?";

            //Minus Left
            minusLeftLabel = new Label();
            minusLeftLabel.AutoSize = false;
            minusLeftLabel.Dock = DockStyle.Fill;
            minusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            minusLeftLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            minusLeftLabel.Text = "?";

            //Minus Right
            minusRightLabel = new Label();
            minusRightLabel.AutoSize = false;
            minusRightLabel.Dock = DockStyle.Fill;
            minusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            minusRightLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            minusRightLabel.Text = "?";

            //Multiplication Left
            multiplicationLeft = new Label();
            multiplicationLeft.AutoSize = false;
            multiplicationLeft.Dock = DockStyle.Fill;
            multiplicationLeft.TextAlign = ContentAlignment.MiddleCenter;
            multiplicationLeft.Font = new Font("Arial", 18, FontStyle.Italic);
            multiplicationLeft.Text = "?";

            //Multiplication Right
            multiplicationRight = new Label();
            multiplicationRight.AutoSize = false;
            multiplicationRight.Dock = DockStyle.Fill;
            multiplicationRight.TextAlign = ContentAlignment.MiddleCenter;
            multiplicationRight.Font = new Font("Arial", 18, FontStyle.Italic);
            multiplicationRight.Text = "?";

            //Division Left
            divisionLeft = new Label();
            divisionLeft.AutoSize = false;
            divisionLeft.Dock = DockStyle.Fill;
            divisionLeft.TextAlign = ContentAlignment.MiddleCenter;
            divisionLeft.Font = new Font("Arial", 18, FontStyle.Italic);
            divisionLeft.Text = "?";

            //Division Right
            divisionRight = new Label();
            divisionRight.AutoSize = false;
            divisionRight.Dock = DockStyle.Fill;
            divisionRight.TextAlign = ContentAlignment.MiddleCenter;
            divisionRight.Font = new Font("Arial", 18, FontStyle.Italic);
            divisionRight.Text = "?";

            tlp.Controls.Add(plusLeftLabel);
            tlp.SetCellPosition(plusLeftLabel, new TableLayoutPanelCellPosition(0, 0));
            tlp.Controls.Add(plusRightLabel);
            tlp.SetCellPosition(plusRightLabel, new TableLayoutPanelCellPosition(2, 0));

            tlp.Controls.Add(minusLeftLabel);
            tlp.SetCellPosition(minusLeftLabel, new TableLayoutPanelCellPosition(0, 1));
            tlp.Controls.Add(minusRightLabel);
            tlp.SetCellPosition(minusRightLabel, new TableLayoutPanelCellPosition(2, 1));

            tlp.Controls.Add(multiplicationLeft);
            tlp.SetCellPosition(multiplicationLeft, new TableLayoutPanelCellPosition(0, 2));
            tlp.Controls.Add(multiplicationRight);
            tlp.SetCellPosition(multiplicationRight, new TableLayoutPanelCellPosition(2, 2));

            tlp.Controls.Add(divisionLeft);
            tlp.SetCellPosition(divisionLeft, new TableLayoutPanelCellPosition(0, 3));
            tlp.Controls.Add(divisionRight);
            tlp.SetCellPosition(divisionRight, new TableLayoutPanelCellPosition(2, 3));

            //Button start the quiz
            start = new Button();
            start.Text = "Start the quiz";
            start.Font = new Font("Arial", 18, FontStyle.Italic);
            start.Height = 60;
            start.Width = 90;
            start.AutoSize = true;
            start.Location = new Point(150, 290);
            start.BackColor = Color.AliceBlue;
            start.Click += Start_Click;

            //Button - Close
            close = new Button();
            close.Text = "Close";
            close.Font = new Font("Arial", 18, FontStyle.Italic);
            close.Height = 60;
            close.Width = 90;
            close.AutoSize = true;
            close.Location = new Point(150, 340);
            close.BackColor = Color.AliceBlue;
            close.Click += Close_Click; 

            //Timer
            timer = new System.Timers.Timer();
            timer.Interval = 1000; 
        }

        private void Numeric4_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Numeric3_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Numeric2_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Numeric1_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Close_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void Start_Click(object? sender, EventArgs e)
        {
            StartTheQuiz();
            start.Enabled = false;
        }

        
        // Start the quiz by filling in all of the problem 
        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = random.Next(51);
            addend2 = random.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = random.Next(1, 101);
            subtrahend = random.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            divisionLeft.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = random.Next(2, 11);
            multiplier = random.Next(2, 11);
            multiplicationLeft.Text = multiplicand.ToString();
            multiplicationRight.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = random.Next(2, 11);
            int temporaryQuotient = random.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            divisionLeft.Text = dividend.ToString();
            divisionRight.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timer = 30;
            lbl.Text = "30 seconds";
            timer.Start();
        }
        // Check the answers to see if the user got everything right.
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }



    }
}
