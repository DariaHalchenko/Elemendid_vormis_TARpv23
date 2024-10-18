using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmVorm : Form
    {
        List<string> tausta_varv = new List<string> { "Aquamarine", "Coral", "DarkSalmon", "Khaki", "LavenderBlush", "LightGreen" };
        List<string> actions = new List<string> { "+", "-", "*", "/" };
        Label time, plusLeftLabel, plusRightLabel, minusLeftLabel, minusRightLabel,
            multiplicationLeft, multiplicationRight, divisionLeft, divisionRight,
            equals, signs;
        TableLayoutPanel tlp;
        Button close, start, alusta_otsast, varv, taustavarv, giveUp;
        System.Windows.Forms.Timer timer;
        NumericUpDown sum, difference, product, quotient;
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

        int timeLeft;
        
        public KolmVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";
            this.BackColor = Color.Thistle;

            //TableLayoutPanel
            tlp = new TableLayoutPanel();
            tlp.BorderStyle = BorderStyle.FixedSingle;
            tlp.AutoSize = true;
            tlp.BackColor = Color.LightPink;
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
            flp.AutoSize = true;
            flp.Controls.Add(tlp);

            //NumericUpDown
            //sum
            sum = new NumericUpDown();
            sum.Font = new Font("Calibri", 18, FontStyle.Regular);
            sum.Width = 100;
            sum.Text = "sum";
            sum.BackColor = Color.PapayaWhip;
            sum.Enter += Sum_Enter;

            //difference
            difference = new NumericUpDown();
            difference.Font = new Font("Calibri", 18, FontStyle.Regular);
            difference.Width = 100;
            difference.Text = "min";
            difference.BackColor = Color.PapayaWhip;
            difference.Enter += Difference_Enter;

            //product
            product = new NumericUpDown();
            product.Font = new Font("Calibri", 18, FontStyle.Regular);
            product.Width = 100;
            product.Text = "umn";
            product.BackColor = Color.PapayaWhip;
            product.Enter += Product_Enter;

            //quotient
            quotient = new NumericUpDown();
            quotient.Font = new Font("Calibri", 18, FontStyle.Regular);
            quotient.Width = 100;
            quotient.Text = "del";
            quotient.BackColor = Color.PapayaWhip;
            quotient.Enter += Quotient_Enter;


            tlp.Controls.Add(sum, 4, 0);
            tlp.Controls.Add(difference, 4, 1);
            tlp.Controls.Add(product, 4, 2);
            tlp.Controls.Add(quotient, 4, 3);

            //Time Left - lbl
            time = new Label();
            time.AutoSize = true;
            time.Text = "Time Left: 40 seconds";
            time.Font = new Font("Harlow Solid Italic", 18, FontStyle.Italic);
            time.TextAlign = ContentAlignment.MiddleCenter;
            time.Dock = DockStyle.Top;
            

            //Plus Left
            plusLeftLabel = new Label();
            plusLeftLabel.Text = "?";
            plusLeftLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            plusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            

            //Plus Right
            plusRightLabel = new Label();
            plusRightLabel.Text = "?";
            plusRightLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            plusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            
            
            //Minus Left
            minusLeftLabel = new Label();
            minusLeftLabel.Text = "?";
            minusLeftLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            minusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            

            //Minus Right
            minusRightLabel = new Label();
            minusRightLabel.Text = "?";
            minusRightLabel.Font = new Font("Arial", 18, FontStyle.Italic);
            minusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
 

            //Multiplication Left
            multiplicationLeft = new Label();
            multiplicationLeft.Text = "?";
            multiplicationLeft.Font = new Font("Arial", 18, FontStyle.Italic);
            multiplicationLeft.TextAlign = ContentAlignment.MiddleCenter;

            //Multiplication Right
            multiplicationRight = new Label();
            multiplicationRight.Text = "?";
            multiplicationRight.Font = new Font("Arial", 18, FontStyle.Italic);
            multiplicationRight.TextAlign = ContentAlignment.MiddleCenter;

            //Division Left
            divisionLeft = new Label();
            divisionLeft.Text = "?";
            divisionLeft.Font = new Font("Arial", 18, FontStyle.Italic);
            divisionLeft.TextAlign = ContentAlignment.MiddleCenter;

            //Division Right
            divisionRight = new Label();
            divisionRight.Text = "?";
            divisionRight.Font = new Font("Arial", 18, FontStyle.Italic);
            divisionRight.TextAlign = ContentAlignment.MiddleCenter;

            //Добавление "равно" в Label vordub и знаков при помощи цикла
            for (int i = 0; i < 4; i++)
            {
                equals = new Label();
                equals.AutoSize = false;
                equals.Dock = DockStyle.Fill;
                equals.TextAlign = ContentAlignment.MiddleCenter;
                equals.Font = new Font("Calibri", 15, FontStyle.Regular);
                equals.Text = actions [i];
                
                tlp.Controls.Add(equals, 1, i);
                
                
                signs = new Label();
                signs.AutoSize = false;
                signs.Dock = DockStyle.Fill;
                signs.TextAlign = ContentAlignment.MiddleCenter;
                signs.Font = new Font("Calibri", 20, FontStyle.Regular);
                signs.Text = "=";
                
                tlp.Controls.Add(signs, 3, i);
            }

            // Добавляем метки для чисел
            tlp.Controls.Add(plusLeftLabel, 0, 0);
            tlp.Controls.Add(plusRightLabel, 2, 0);

            tlp.Controls.Add(minusLeftLabel, 0, 1);
            tlp.Controls.Add(minusRightLabel, 2, 1);

            tlp.Controls.Add(multiplicationLeft, 0, 2);
            tlp.Controls.Add(multiplicationRight, 2, 2);

            tlp.Controls.Add(divisionLeft, 0, 3);
            tlp.Controls.Add(divisionRight, 2, 3);

            //Button start the quiz
            start = new Button();
            start.Text = "Start the quiz";
            start.Font = new Font("Algerian", 18, FontStyle.Italic);
            start.Height = 45;
            start.Width = 200;
            start.Location = new Point(10, 300);
            start.BackColor = Color.Plum;
            start.Click += Start_Click;

            //Button - Close
            close = new Button();
            close.Text = "Close";
            close.Font = new Font("Algerian", 18, FontStyle.Italic);
            close.Height = 45;
            close.Width = 200;
            close.Location = new Point(10, 350);
            close.BackColor = Color.Plum;
            close.Click += Close_Click;

            //Button - alusta_otsast
            alusta_otsast = new Button();
            alusta_otsast.Text = "Alusta otsast";
            alusta_otsast.Font = new Font("Algerian", 18, FontStyle.Italic);
            alusta_otsast.Height = 45;
            alusta_otsast.Width = 200;
            alusta_otsast.Location = new Point(250, 350);
            alusta_otsast.BackColor = Color.Plum;
            alusta_otsast.Click += Alusta_otsast_Click;

            //Button - varv
            varv = new Button();
            varv.Text = "Muuda värvi";
            varv.Font = new Font("Algerian", 18, FontStyle.Italic);
            varv.Height = 45;
            varv.Width = 200;
            varv.Location = new Point(250, 300);
            varv.BackColor = Color.Plum;
            varv.Click += Varv_Click;

            //Button - taustavarv
            taustavarv = new Button();
            taustavarv.Text = "Tausta varvi";
            taustavarv.Font = new Font("Algerian", 18, FontStyle.Italic);
            taustavarv.Height = 45;
            taustavarv.Width = 200;
            taustavarv.Location = new Point(450, 300);
            taustavarv.BackColor = Color.Plum;
            taustavarv.Click += Taustavarv_Click;

            //nupp giveUp
            giveUp = new Button();
            giveUp.Text = "Give up";
            giveUp.Font = new Font("Harlow Solid Italic", 14);
            giveUp.BackColor = Color.DarkRed;
            giveUp.AutoSize = true;
            giveUp.Location = new Point(600, 295);
            giveUp.TabIndex = 0;
            giveUp.Click += GiveUp_Click; ;

            this.Controls.Add(giveUp);



            //Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            //MenuStrip
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem ajastus = new ToolStripMenuItem("Ajastus");
            ToolStripMenuItem nelikümmend = new ToolStripMenuItem("Nelikümmend", null, new EventHandler(NelikümmendMenuItem_Click));
            ToolStripMenuItem kolmkümmend = new ToolStripMenuItem("Kolmkümmend", null, new EventHandler(KolmkümmendMenuItem_Click));
            ToolStripMenuItem kakskümmend = new ToolStripMenuItem("kakskümmend", null, new EventHandler(kakskümmendMenuItem_Click));
            ToolStripMenuItem minut = new ToolStripMenuItem("Minut", null, new EventHandler(MinutMenuItem_Click));

            ajastus.DropDownItems.Add(nelikümmend);
            ajastus.DropDownItems.Add(kolmkümmend);
            ajastus.DropDownItems.Add(kakskümmend);
            ajastus.DropDownItems.Add(minut);

            ms.Items.Add(ajastus);
            this.MainMenuStrip = ms;
            this.Controls.Add(ms);

            this.Controls.Add(flp);
            this.Controls.Add(time);
            this.Controls.Add(start);
            this.Controls.Add(close);
            this.Controls.Add(alusta_otsast);
            this.Controls.Add(varv);
            this.Controls.Add(taustavarv);
        }

        private void GiveUp_Click(object? sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("You decided to give up! Better luck next time.");
        }

        private void NelikümmendMenuItem_Click(object? sender, EventArgs e)
        {
            NewTime(40);
            timeLeft = 40;
            time.Text = "Time Left: 40 seconds";
        }
        private void KolmkümmendMenuItem_Click(object? sender, EventArgs e)
        {
            NewTime(30);
            timeLeft = 30;
            time.Text = "Time Left: 30 seconds";
        }
        private void kakskümmendMenuItem_Click(object? sender, EventArgs e)
        {
            NewTime(20);
            timeLeft = 20;
            time.Text = "Time Left: 20 seconds";
        }
        private void MinutMenuItem_Click(object? sender, EventArgs e)
        {
            NewTime(60);
            timeLeft = 60;
            time.Text = "Time Left: 60 seconds";
        }

        private void NewTime(int seconds)
        {
            timeLeft = seconds;
            time.Text = $"Time Left: {timeLeft} seconds";

            timer.Stop();
            start.Enabled = true;
        }

        private void Taustavarv_Click(object? sender, EventArgs e)
        {
            int randomIndex = random.Next(tausta_varv.Count);
            this.BackColor = Color.FromName(tausta_varv[randomIndex]);
        }

        private void Varv_Click(object? sender, EventArgs e)
        {
            Random random = new Random();

            // Генерируем случайные цвета для элементов
            tlp.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            // Меняем цвета кнопок
            start.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            close.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            alusta_otsast.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            varv.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            // Меняем цвет текста меток
            quotient.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            product.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            difference.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            sum.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private void Alusta_otsast_Click(object? sender, EventArgs e)
        {
            // Сбрасываем задание на сложение
            sum.Value = 0;
            plusLeftLabel.Text = "?";
            plusRightLabel.Text = "?";

            // Сбрасываем задание на вычитание
            difference.Value = 0;
            minusLeftLabel.Text = "?";
            minusRightLabel.Text = "?";

            // Сбрасываем задание на умножение
            product.Value = 0;
            multiplicationLeft.Text = "?";
            multiplicationRight.Text = "?";

            // Сбрасываем задание на деление
            quotient.Value = 0;
            divisionLeft.Text = "?";
            divisionRight.Text = "?";

            // Останавливаем таймер и сбрасываем время
            timer.Stop();
            start.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer.Stop();
                MessageBox.Show("Vastasite kõigile küsimustele õigesti!",
                                "Palju õnne! :)");
                start.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                time.Text = "Time Left: " + timeLeft + " seconds";
            }
            else
            {
                timer.Stop();
                time.Text = "Aeg on läbi!";
                MessageBox.Show("Sa ei jõudnud aegade lõpuni");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                start.Enabled = true;
            }
        }
        
        private void Quotient_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Product_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Difference_Enter(object? sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Sum_Enter(object? sender, EventArgs e)
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
            difference.Value = 0;

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
