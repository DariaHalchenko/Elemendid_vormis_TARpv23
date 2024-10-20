using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class TeineVorm : Form
    {
        TableLayoutPanel tlp;
        FlowLayoutPanel flp;
        Button sap, ctp, close, stbc, left, right, filter, salvesta_pilt, inversioon, pildi_moonutamine;
        PictureBox picturebox;
        CheckBox chk;
        OpenFileDialog openfiledialog;
        ColorDialog colordialog;
        Image image;

        public TeineVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Picture Viewer";

            // TableLayoutPanel
            tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;

            
            // PictureBox
            picturebox = new PictureBox();
            picturebox.Dock = DockStyle.Fill;
            picturebox.BorderStyle = BorderStyle.Fixed3D;
            picturebox.SizeMode = PictureBoxSizeMode.CenterImage;

            tlp.Controls.Add(picturebox);
            tlp.SetColumnSpan(picturebox, 2);

            // CheckBox
            chk = new CheckBox();
            chk.Text = "Stretch";
            chk.CheckedChanged += Chk_CheckedChanged;
            tlp.Controls.Add(chk);

            // FlowLayoutPanel
            flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Fill;
            flp.FlowDirection = FlowDirection.RightToLeft;

            //Button - Close
            close = new Button();
            close.Text = "Close";
            close.AutoSize = true;
            close.BackColor = Color.MediumSpringGreen;
            close.Click += Close_Click;

            //Button - Set the background color
            stbc = new Button();
            stbc.Text = "Set the background color";
            stbc.AutoSize = true;
            stbc.BackColor = Color.MediumSpringGreen;
            stbc.Click += Stbc_Click;

            //Button - Clear the picture
            ctp = new Button();
            ctp.Text = "Clear the picture";
            ctp.AutoSize = true;
            ctp.BackColor = Color.MediumSpringGreen;
            ctp.Click += Ctp_Click;

            //Button - Show a picture
            sap = new Button();
            sap.Text = "Show a picture";
            sap.AutoSize = true;
            sap.BackColor = Color.MediumSpringGreen;
            sap.Click += Sap_Click;

            //Button - left
            left = new Button();
            left.Text = "Pööra vasakule";
            left.AutoSize = true;
            left.BackColor = Color.MediumSpringGreen;
            left.Click += Left_Click;

            //Button - right
            right = new Button();
            right.Text = "Pööra paremale";
            right.AutoSize = true;
            right.BackColor = Color.MediumSpringGreen;
            right.Click += Right_Click;

            //Button - filter
            filter = new Button();
            filter.Text = "Punane & Must";
            filter.AutoSize = true;
            filter.BackColor = Color.MediumSpringGreen;
            filter.Click += Filter_Click;

            //Button - Salvesta pilt
            salvesta_pilt = new Button();
            salvesta_pilt.Text = "Salvesta pilt";
            salvesta_pilt.AutoSize = true;
            salvesta_pilt.BackColor = Color.MediumSpringGreen;
            salvesta_pilt.Click += Salvesta_pilt_Click;

            //Button - inversioon 
            inversioon = new Button();
            inversioon.Text = "Inversioon";
            inversioon.AutoSize = true;
            inversioon.BackColor = Color.MediumSpringGreen;
            inversioon.Click += Inversioon_Click;

            //Button - pildi moonutamine
            pildi_moonutamine = new Button();
            pildi_moonutamine.Text = "Pildi moonutamine";
            pildi_moonutamine.AutoSize = true;
            pildi_moonutamine.BackColor = Color.MediumSpringGreen;
            pildi_moonutamine.Click += Pildi_moonutamine_Click;

            // Lisa buttons to FlowLayoutPanel
            tlp.Controls.Add(flp);
            flp.Controls.Add(salvesta_pilt);
            flp.Controls.Add(inversioon);
            flp.Controls.Add(pildi_moonutamine);
            flp.Controls.Add(ctp);
            flp.Controls.Add(left);
            flp.Controls.Add(right);
            flp.Controls.Add(filter);
            flp.Controls.Add(sap);
            flp.Controls.Add(close);
            flp.Controls.Add(stbc);

            //  Lisa ColumnStyles
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));

            //  Lisa RowStyles
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            this.Controls.Add(tlp);

            openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openfiledialog.Title = "Select a picture file";

            // ColorDialog
            colordialog = new ColorDialog();
        }

        //moonutab pilti, tekitades lainekujulisi liine
        //искажает изображение, создавая волнообразные линие
        private void Pildi_moonutamine_Click(object? sender, EventArgs e)
        {
            // Kontrollib, kas pilt on üles laetud
            // Проверяет загружено ли изображение 
            if (picturebox.Image != null)
            {
                Bitmap bmp = new Bitmap(picturebox.Image);
                Bitmap moonutatud_pilt = new Bitmap(bmp.Width, bmp.Height);

                //Kolitakse iga pikslit y koordinaatide järgi
                //Перебирает каждый пиксель по координатам y
                for (int y = 0; y < bmp.Height; y++)
                {
                    //Kerib iga piksli läbi x koordinaatide järgi
                    //Перебирает каждый пиксель по координатам x
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // Otsime piksleid, tekitades X-telje nihke
                        // Искажаем пиксели, создавая сдвиг по оси X
                        int niheX = (int)(10 * Math.Sin(y / 10.0)); //Math.Sin - синусоидальная функция

                        //uus piksli asend X telje järgi ja lisame nihke
                        //новое положение пикселя по оси X и  добавляем сдвиг 
                        int uusX = x + niheX;

                        //Kontrollime, kas uus asend on pildi piirides
                        //Проверяем, находиться ли новое положение в границах изображения
                        if (uusX >= 0 && uusX < bmp.Width)
                        {
                            // Viime piksli lähtest uude, moonutatud asendisse
                            // Переносим пиксель из исходного в новое, искаженное положение
                            moonutatud_pilt.SetPixel(uusX, y, bmp.GetPixel(x, y));
                        }
                    }
                }
                // Paigaldame moonutatud pildi
                // Устанавливаем искаженное изображение
                picturebox.Image = moonutatud_pilt;
            }
        }

        //inverteerib pildil olevaid värve
        //инвертирует цвета на изображении
        private void Inversioon_Click(object? sender, EventArgs e)
        {
            // Kontrollib, kas pilt on üles laetud
            // Проверяет загружено ли изображение 
            if (picturebox.Image != null)
            {
                Bitmap bmp = new Bitmap(picturebox.Image);
                Bitmap piltide_inversioon = new Bitmap(bmp.Width, bmp.Height);

                //Kerib iga piksli läbi x koordinaatide järgi
                //Перебирает каждый пиксель по координатам x
                for (int x = 0; x < bmp.Width; x++)
                {
                    //Kolitakse iga pikslit y koordinaatide järgi
                    //Перебирает каждый пиксель по координатам y
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        // Saame lähtepiksli värvi
                        // Получаем цвет исходного пикселя
                        Color originaalvarv = bmp.GetPixel(x, y);

                        // Inverteerime värvi punasest komponendist, rohelisest ja sinisest 
                        // Инвертируем цвет из красного компонента, зеленого и синего 
                        Color inversiooni_varv = Color.FromArgb(255 - originaalvarv.R, 255 - originaalvarv.G, 255 - originaalvarv.B); //255 tähendab maksimaalset värviküllust (255 означает максимальное насыщенность цвета)

                        // Paigaldame inverteeritud värvi
                        // Устанавливаем инвертированный цвет
                        piltide_inversioon.SetPixel(x, y, inversiooni_varv);
                    }
                }
                // Paigaldame inverteeritud pildi
                // Устанавливаем инвертированное изображение
                picturebox.Image = piltide_inversioon;
            }
        }

        // Salvestab pildi faili
        // Сохраняет изображение в файл
        private void Salvesta_pilt_Click(object? sender, EventArgs e)
        {
            if (picturebox.Image != null)
            {
                // Faili salvestamiseks dialoogiakna loomine
                // Создание диалогового окна для сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
                //dialoogiakna pealkiri
                //заголовок диалогового окна
                saveFileDialog.Title = "Salvesta pilt";

                //Säilitame pildi (Сохраняем изображение)
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picturebox.Image.Save(saveFileDialog.FileName);
                }
            }
            //kui pilti pole näitab sõnumit
            //если нет изображения  показывает сообщение
            else
            {
                MessageBox.Show("Puudub pilt, mida salvestada.");
            }
        }

        //muundab pildi punaseks ja mustaks
        //преобразует изображение в красное и черное
        private void Filter_Click(object? sender, EventArgs e)
        {
            Bitmap redBlackEffect = (Bitmap)picturebox.Image.Clone();
            //Kolime üle kõik pildipikslid
            //Перебираем все пиксели изображения
            for (int yCoordinate = 0; yCoordinate < redBlackEffect.Height; yCoordinate++)
            {
                for (int xCoordinate = 0; xCoordinate < redBlackEffect.Width; xCoordinate++)
                {
                    // Saame piksli praeguse värvi
                    // Получаем текущий цвет пикселя
                    Color color = redBlackEffect.GetPixel(xCoordinate, yCoordinate);
                    
                    //Loome halli (musta) värvi isa 
                    //Создаем отенок серого (черного) цвета 
                    double grayColor = ((double)(color.R + color.G + color.B)) / 3.0d;
                    
                    //Loome punast värvi isa 
                    //Создаем отенок красного цвета 
                    Color red = Color.FromArgb((byte)grayColor, (byte)(0), (byte)(0));
                    
                    // Paigaldame pikslite uue värvi
                    // Устанавливаем новый цвет пикселей
                    redBlackEffect.SetPixel(xCoordinate, yCoordinate, red);
                }
            }
            //Uuendame pilti (Обновляем изображение)
            picturebox.Image = redBlackEffect;
        }

        private void Right_Click(object? sender, EventArgs e)
        {
            if (image != null)
            {
                // Pöörame pildi 90 kraadi peale
                // Поворачиваем изображение на 90 градусов
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                picturebox.Image = image; // Uuendame (Обновляем)
            }
        }

        private void Left_Click(object? sender, EventArgs e)
        {
            if (image != null)
            {
                // Pöörame pildi 270 kraadi peale
                // Поворачиваем изображение на 270 градусов
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                picturebox.Image = image; //Uuendame (Обновляем)
            }
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            picturebox.SizeMode = chk.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Normal;

        }

        private void Sap_Click(object? sender, EventArgs e)
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openfiledialog.FileName); 
                picturebox.Image = image; 
            }
        }

        private void Ctp_Click(object? sender, EventArgs e)
        {
            picturebox.Image = null;
        }

        private void Stbc_Click(object? sender, EventArgs e)
        {
            if (colordialog.ShowDialog() == DialogResult.OK)
                picturebox.BackColor = colordialog.Color;
        }

        private void Close_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}