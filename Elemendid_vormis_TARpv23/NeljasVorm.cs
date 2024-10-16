using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class NeljasVorm : Form
    {
        TableLayoutPanel tlp, table;
        Label[] labelsmail = new Label[16];
        Label label, time;
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        List<string> icons_naine = new List<string>()
        {
            "R", "R", "T", "T", "Y", "Y", "Z", "Z",
            "[", "[", "]", "]", "{", "{", "|", "|"
        };
        List<string> icons_mees = new List<string>()
        {
            "N", "N", "M", "M", "=", "=", "#", "#",
            ":", ":", "P", "P", "Q", "Q", ")", ")"
        };
        Label firstClicked = null;
        Label secondClicked = null;

        System.Windows.Forms.Timer timer, gametimer;

        Button close, start, varv;

        FlowLayoutPanel flp;

        Random random = new Random();

        int timeLeft;

        string stiilis;

        public NeljasVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Matching Game";


            tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            tlp.RowStyles.Add(new ColumnStyle(SizeType.Percent, 10));


            table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.BackColor = Color.CornflowerBlue;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            table.ColumnCount = 4;
            table.RowCount = 4;

            time = new Label();
            time.AutoSize = true;
            time.Text = "Time Left: 60 seconds";
            time.Font = new Font("Arial", 18, FontStyle.Italic);
            time.TextAlign = ContentAlignment.MiddleCenter;
            time.Dock = DockStyle.Top;

            close = new Button();
            close.Text = "Close";
            close.Font = new Font("Arial", 18, FontStyle.Italic);
            close.Height = 50;
            close.Width = 100;
            close.AutoSize = true;
            close.Click += Close_Click;

            start = new Button();
            start.Text = "Start";
            start.Font = new Font("Arial", 18, FontStyle.Italic);
            start.Height = 60;
            start.Width = 150;
            start.AutoSize = true;
            start.Click += Start_Click;

            flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Bottom;
            flp.FlowDirection = FlowDirection.LeftToRight;
            flp.AutoSize = true;

            flp.Controls.Add(time);
            flp.Controls.Add(start);
            flp.Controls.Add(close);

            tlp.Controls.Add(table); // Игровое поле сверху
            tlp.Controls.Add(flp); // Таймер и кнопки снизу

            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                for (int j = 0; j < 4; j++)
                {
                    table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

                    labelsmail[i] = new Label();
                    labelsmail[i].BackColor = Color.CornflowerBlue;
                    labelsmail[i].AutoSize = false;
                    labelsmail[i].Dock = DockStyle.Fill;
                    labelsmail[i].TextAlign = ContentAlignment.MiddleCenter;
                    labelsmail[i].Font = new Font("Wingdings", 48, FontStyle.Bold);

                    table.Controls.Add(labelsmail[i], j, i);
                    labelsmail[i].Click += Game_Click;

                }
            }

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            gametimer = new System.Windows.Forms.Timer();
            gametimer.Interval = 1000;
            gametimer.Tick += Gametimer_Tick;

            // Создание MenuStrip 
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem font = new ToolStripMenuItem("Font");
            ToolStripMenuItem arialMenuItem = new ToolStripMenuItem("Arial", null, new EventHandler(ArialMenuItem_Click));
            ToolStripMenuItem wingdingsMenuItem = new ToolStripMenuItem("Wingdings", null, new EventHandler(WingdingsMenuItem_Click));
            
            ToolStripMenuItem sugu = new ToolStripMenuItem("Sugu");
            ToolStripMenuItem naissoostMenuItem = new ToolStripMenuItem("Naissoost", null, new EventHandler(NaissoostMenuItem_Click));
            ToolStripMenuItem meessoostMenuItem = new ToolStripMenuItem("Meessoost", null, new EventHandler(MeessoostMenuItem_Click));

            // Добавление подменю
            font.DropDownItems.Add(arialMenuItem);
            font.DropDownItems.Add(wingdingsMenuItem);

            sugu.DropDownItems.Add(naissoostMenuItem);
            sugu.DropDownItems.Add(meessoostMenuItem);

            ms.Items.Add(font);
            ms.Items.Add(sugu);
            this.MainMenuStrip = ms;
            this.Controls.Add(ms);

            this.Controls.Add(tlp);
        }

        private void MeessoostMenuItem_Click(object? sender, EventArgs e)
        {
            foreach (Control control in table.Controls)
            {
                if (control is Label labelsmail)
                {
                    labelsmail.Text = string.Empty; 
                    labelsmail.ForeColor = labelsmail.BackColor;   
                }
            }
            stiilis = "mees";
            AssignIconsToSquares(icons_mees, Color.PowderBlue);
        }

        private void NaissoostMenuItem_Click(object? sender, EventArgs e)
        {
            foreach (Control control in table.Controls)
            {
                if (control is Label labelsmail)
                {
                    labelsmail.Text = string.Empty; 
                    labelsmail.ForeColor = labelsmail.BackColor; 
                }
            }
            stiilis = "naine";
            AssignIconsToSquares(icons_naine, Color.HotPink);
        }

        private void ArialMenuItem_Click(object sender, EventArgs e)
        {
            // Изменяем шрифт меток на Arial
            foreach (Control control in table.Controls)
            {
                if (control is Label labelsmail)
                {
                    labelsmail.Font = new Font("Arial", 48, FontStyle.Bold);
                }
            }
        }

        private void WingdingsMenuItem_Click(object sender, EventArgs e)
        {
            // Изменяем шрифт меток на Wingdings
            foreach (Control control in table.Controls)
            {
                if (control is Label labelsmail)
                {
                    labelsmail.Font = new Font("Wingdings", 48, FontStyle.Bold);
                }
            }
           
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartGame();
            start.Enabled = false;
            start.BackColor = Color.BlueViolet;
        }

        private void StartGame()
        {
            if (stiilis == "naine")
            {
                AssignIconsToSquares(icons_naine, Color.HotPink);
            }
            else if (stiilis == "mees")
            {
                AssignIconsToSquares(icons_mees, Color.PowderBlue);
            }
            else 
            {
                AssignIconsToSquares(icons, Color.CornflowerBlue);
            }

            timeLeft = 60;
            time.Text = "Time Left: 60 seconds";
            gametimer.Start();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Gametimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                time.Text = "Time Left: " + timeLeft + " seconds";
            }
            else
            {
                gametimer.Stop();
                time.Text = "Aeg on läbi!";
                MessageBox.Show("Sa ei jõudnud aegade lõpuni:(", "Vabandust!");
                start.Enabled = true;
            }
        }

        private void CheckForWinner()
        {
            foreach (Control control in table.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            gametimer.Stop();
            MessageBox.Show("Sa sobisid kõik ikoonid!", "Õnnitlus");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            firstClicked.ForeColor = firstClicked.BackColor; // Скрыть первую карточку
            secondClicked.ForeColor = secondClicked.BackColor; // Скрыть вторую карточку
            firstClicked = null;
            secondClicked = null;
        }

        private void Game_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {

                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer.Start();

            }
        }

        private void AssignIconsToSquares(List<string> icons, Color backColor)
        {
            List<string> list = new List<string>(icons);

            // Перемешивание иконок
            for (int i = 0; i < list.Count; i++)
            {
                int j = random.Next(i, list.Count);
                string tagasi = list[i];
                list[i] = list[j];
                list[j] = tagasi; // Возвращаем сохраненное значение на позицию j
            }

            int iconsIndex = 0;
            foreach (Control control in table.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.Text = list[iconsIndex];
                    iconLabel.BackColor = backColor;
                    iconLabel.ForeColor = iconLabel.BackColor; // Скрыть иконку
                    iconsIndex++;
                }
            }
        }
    }
}

