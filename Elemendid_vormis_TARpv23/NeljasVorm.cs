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
        Label label, time, katsedlabel;
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

        System.Windows.Forms.Timer timer, gametimer, timer2;

        Button close, start, alusta_otsast, naita_ikoone;

        FlowLayoutPanel flp;

        Random random = new Random();

        int timeLeft;

        string stiilis;

        int katsed = 0;

        public NeljasVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Matching Game";

            //TableLayoutPanel - tbl
            tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            tlp.RowStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            //TableLayoutPanel - table
            table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.BackColor = Color.White;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset; 
            table.ColumnCount = 4;
            table.RowCount = 4;

            // Puhastame veergude ja ridade praegused stiilid (Очищаем текущие стили столбцов и строк)
            table.ColumnStyles.Clear();
            table.RowStyles.Clear();

            // Seadistame igale tulbale stiili (Устанавливаем стиль для каждого столбца)
            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            }

            // Seadistame igale reale stiili (Устанавливаем стиль для каждой строки)
            for (int i = 0; i < table.RowCount; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            }

            //Label - time
            time = new Label();
            time.AutoSize = true;
            time.Text = "Time Left: 60 seconds";
            time.Font = new Font("Arial", 15, FontStyle.Italic);
            time.TextAlign = ContentAlignment.MiddleCenter;
            time.Dock = DockStyle.Top;

            //Label - katsedlabel
            katsedlabel = new Label();
            katsedlabel.AutoSize = true;
            katsedlabel.Text = "Katsed: 0";
            katsedlabel.Font = new Font("Arial", 15, FontStyle.Italic);
            katsedlabel.TextAlign = ContentAlignment.MiddleCenter;
            katsedlabel.Dock = DockStyle.Top;

            //Button - close
            close = new Button();
            close.Text = "Close";
            close.Font = new Font("Algerian", 18, FontStyle.Italic);
            close.Height = 50;
            close.Width = 150;
            close.BackColor = Color.Plum;
            close.AutoSize = true;
            close.Click += Close_Click;

            //Button - start
            start = new Button();
            start.Text = "Start";
            start.Font = new Font("Algerian", 18, FontStyle.Italic);
            start.Height = 50;
            start.Width = 150;
            start.AutoSize = true;
            start.BackColor = Color.Plum;
            start.Enabled = false;
            start.Click += Start_Click;

            //Button - naita ikoone
            naita_ikoone = new Button();
            naita_ikoone.Text = "Näita ikoone";
            naita_ikoone.Font = new Font("Algerian", 18, FontStyle.Italic);
            naita_ikoone.Height = 50;
            naita_ikoone.Width = 150;
            naita_ikoone.AutoSize = true;
            naita_ikoone.BackColor = Color.Plum;
            naita_ikoone.Click += Naita_ikoone_Click;

            //FlowLayoutPanel - flp
            flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Bottom;
            flp.FlowDirection = FlowDirection.TopDown;
            flp.AutoSize = true;

            //Lisame juhtelemendid (Добавляем элементы управления)
            flp.Controls.Add(time);
            flp.Controls.Add(katsedlabel);
            flp.Controls.Add(start);
            flp.Controls.Add(close);
            flp.Controls.Add(naita_ikoone);

            tlp.Controls.Add(table); //Mänguväli ülevalt (Игровое поле сверху)
            tlp.Controls.Add(flp); //Taimer ja alt nupud (Таймер и кнопки снизу)

            //Loome võrgu (Создаем сетку)
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    labelsmail[i] = new Label();
                    labelsmail[i].BackColor = Color.White;
                    labelsmail[i].AutoSize = false;
                    labelsmail[i].Dock = DockStyle.Fill;
                    labelsmail[i].TextAlign = ContentAlignment.MiddleCenter;
                    labelsmail[i].Margin = new Padding(2); //Paigaldame tagasilöögid 2 peale (Устанавливаем отступы на 2)
                    labelsmail[i].Padding = new Padding(2); //Paigaldame tagasilöögid 2 peale (Устанавливаем отступы на 2)

                    table.Controls.Add(labelsmail[i], j, i);
                    labelsmail[i].Click += Game_Click;
                }
            }


            // Loome  timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            // Loome gametimer
            gametimer = new System.Windows.Forms.Timer();
            gametimer.Interval = 1000;
            gametimer.Tick += Gametimer_Tick;

            //Ikooni kuvamise taimeri initsialiseerimine
            // Инициализация таймера отображения иконок
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 10000; 
            timer2.Tick += Timer2_Tick; ;

            // Loome MenuStrip 
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem font = new ToolStripMenuItem("Font");
            ToolStripMenuItem arialMenuItem = new ToolStripMenuItem("Arial", null, new EventHandler(ArialMenuItem_Click));
            ToolStripMenuItem wingdingsMenuItem = new ToolStripMenuItem("Wingdings", null, new EventHandler(WingdingsMenuItem_Click));

            ToolStripMenuItem sugu = new ToolStripMenuItem("Sugu");
            ToolStripMenuItem naissoostMenuItem = new ToolStripMenuItem("Naissoost", null, new EventHandler(NaissoostMenuItem_Click));
            ToolStripMenuItem meessoostMenuItem = new ToolStripMenuItem("Meessoost", null, new EventHandler(MeessoostMenuItem_Click));
            ToolStripMenuItem tutorialMenuItem = new ToolStripMenuItem("Tutorial", null, new EventHandler(TutorialMenuItem_Click));

            ToolStripMenuItem taustal = new ToolStripMenuItem("Taustal");
            ToolStripMenuItem salmonMenuItem = new ToolStripMenuItem("Salmon", null, new EventHandler(SalmonMenuItem_Click));
            ToolStripMenuItem springGreenMenuItem = new ToolStripMenuItem("SpringGreen", null, new EventHandler(SpringGreenMenuItem_Click));
            ToolStripMenuItem indianRedGreenMenuItem = new ToolStripMenuItem("IndianRed", null, new EventHandler(IndianRedMenuItem_Click));
            ToolStripMenuItem crimsonMenuItem = new ToolStripMenuItem("Crimson", null, new EventHandler(CrimsonMenuItem_Click));
            ToolStripMenuItem orchidMenuItem = new ToolStripMenuItem("SpringGreen", null, new EventHandler(OrchidMenuItem_Click));

            //Alammenüü lisamine (Добавление подменю)
            font.DropDownItems.Add(arialMenuItem);
            font.DropDownItems.Add(wingdingsMenuItem);

            sugu.DropDownItems.Add(naissoostMenuItem);
            sugu.DropDownItems.Add(meessoostMenuItem);
            sugu.DropDownItems.Add(tutorialMenuItem);

            taustal.DropDownItems.Add(salmonMenuItem);
            taustal.DropDownItems.Add(springGreenMenuItem);
            taustal.DropDownItems.Add(indianRedGreenMenuItem);
            taustal.DropDownItems.Add(crimsonMenuItem);
            taustal.DropDownItems.Add(orchidMenuItem);

            ms.Items.Add(font);
            ms.Items.Add(sugu);
            ms.Items.Add(taustal);
            this.MainMenuStrip = ms;
            this.Controls.Add(ms);

            this.Controls.Add(tlp);
        }

        private void Timer2_Tick(object? sender, EventArgs e)
        {
            // Peata taimer (Остановить таймер)
            timer2.Stop();

            //Peida kõik ikoonid uuesti (Скрыть все иконки снова)
            foreach (Control control in table.Controls)
            {
                if (control is Label iconLabel)
                {
                    iconLabel.ForeColor = iconLabel.BackColor; 
                }
            }
        }

        private void Naita_ikoone_Click(object? sender, EventArgs e)
        {
            //Kuva kõik ikoonid (Отобразить все иконки)
            foreach (Control control in table.Controls)
            {
                if (control is Label iconLabel)
                {
                    iconLabel.ForeColor = Color.Black; //Kuva ikoon (Показать иконку)
                }
            }
            //Käivita taimer 10 sekundiks (Запустить таймер на 10 секунд)
            timer2.Start();
        }

        private void SalmonMenuItem_Click(object? sender, EventArgs e)
        {
            this.BackColor = Color.Salmon;
        }

        private void SpringGreenMenuItem_Click(object? sender, EventArgs e)
        {
            this.BackColor = Color.SpringGreen;
        }
        private void IndianRedMenuItem_Click(object? sender, EventArgs e)
        {
            this.BackColor = Color.IndianRed;
        }
        private void CrimsonMenuItem_Click(object? sender, EventArgs e)
        {
            this.BackColor = Color.Crimson;
        }
        private void OrchidMenuItem_Click(object? sender, EventArgs e)
        {
            this.BackColor = Color.Orchid;
        }

        //kutsume meetodi välja - mees (вызываем метод - mees)
        private void MeessoostMenuItem_Click(object? sender, EventArgs e)
        {
            stiilis = "mees";
        }

        //kutsume meetodi välja - naine (вызываем метод - naine)
        private void NaissoostMenuItem_Click(object? sender, EventArgs e)
        {
            stiilis = "naine";
        }
        //kutsume meetodi välja - tutorial (вызываем метод - tutorial)
        private void TutorialMenuItem_Click(object? sender, EventArgs e)
        {
            stiilis = "tutorial";
        }

        private void ArialMenuItem_Click(object sender, EventArgs e)
        {
            //Aktiveerime nupu - start (Активируем кнопку - start)
            start.Enabled = true;
            // Läbime kõik tabelis olevad elemendid ( Проходим по всем элементам в таблице)
            foreach (Control control in table.Controls)
            {
                if (control is Label iconLabel)
                {
                    //Paigaldame kirja (Устанавливаем шрифт)
                    iconLabel.Font = new Font("Arial", 48, FontStyle.Bold);
                }
            }
        }

        private void WingdingsMenuItem_Click(object sender, EventArgs e)
        {
            //Aktiveerime nupu - start (Активируем кнопку - start)
            start.Enabled = true;
            // Läbime kõik tabelis olevad elemendid ( Проходим по всем элементам в таблице)
            foreach (Control control in table.Controls)
            {
                if (control is Label iconLabel)
                {
                    //Paigaldame kirja (Устанавливаем шрифт)
                    iconLabel.Font = new Font("Wingdings", 48, FontStyle.Bold);
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartGame();
            start.Enabled = false;
        }

        private void StartGame()
        {
            //Kontrollime, milline stiil on valitud (Проверяем, какой стиль выбран)
            if (stiilis == "naine")
            {
                // Määrame ikoonid ja värvi (Назначаем иконки и цвет)
                AssignIconsToSquares(icons_naine, Color.HotPink);
            }
            else if (stiilis == "mees")
            {
                // Määrame ikoonid ja värvi (Назначаем иконки и цвет)
                AssignIconsToSquares(icons_mees, Color.PowderBlue);
            }
            else if (stiilis == "tutorial")
            {
                // Määrame ikoonid ja värvi (Назначаем иконки и цвет)
                AssignIconsToSquares(icons, Color.CornflowerBlue);
            }
            else
            {
                MessageBox.Show("Ei valinud sugu!");
                return;
            }

            timeLeft = 60;
            katsed = 0;
            katsedlabel.Text = "Katsed: 0";
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
                MessageBox.Show("Sa ei jõudnud aegade lõpuni");
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
            MessageBox.Show("Kogusid kõik ikoonid kokku!");
            start.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

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

                katsed++;
                katsedlabel.Text = "Katsed: " + katsed;

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

            //Ikooni segamine (Перемешивание иконок)
            for (int i = 0; i < list.Count; i++)
            {
                int j = random.Next(i, list.Count);
                string tagasi = list[i];
                list[i] = list[j];
                list[j] = tagasi; //Tagastame salvestatud väärtuse positsioonile  (Возвращаем сохраненное значение на позицию)
            }

            int iconsIndex = 0;
            foreach (Control control in table.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.Text = list[iconsIndex];
                    iconLabel.BackColor = backColor;
                    iconLabel.ForeColor = iconLabel.BackColor; //Varja ikoon (Скрыть иконку)
                    iconsIndex++;
                }
            }
        }
    }
}
