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

        // искажает изображение, создавая волнообразные линие
        private void Pildi_moonutamine_Click(object? sender, EventArgs e)
        {
            if (picturebox.Image != null)
            {
                Bitmap bmp = new Bitmap(picturebox.Image);
                Bitmap moonutatud_pilt = new Bitmap(bmp.Width, bmp.Height);

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // Искажаем пиксели
                        int niheX = (int)(10 * Math.Sin(y / 10.0));
                        int uusX = x + niheX;

                        if (uusX >= 0 && uusX < bmp.Width)
                        {
                            moonutatud_pilt.SetPixel(uusX, y, bmp.GetPixel(x, y));
                        }
                    }
                }
                picturebox.Image = moonutatud_pilt;
            }
        }

        //инвертирует цвета на изображении.
        private void Inversioon_Click(object? sender, EventArgs e)
        {
            if (picturebox.Image != null)
            {
                Bitmap bmp = new Bitmap(picturebox.Image);
                Bitmap piltide_inversioon = new Bitmap(bmp.Width, bmp.Height);

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color originaalvarv = bmp.GetPixel(x, y);
                        Color inversiooni_varv = Color.FromArgb(255 - originaalvarv.R, 255 - originaalvarv.G, 255 - originalvarv.B);
                        piltide_inversioon.SetPixel(x, y, inversiooni_varv);
                    }
                }
                picturebox.Image = piltide_inversioon;
            }
        }

        private void Salvesta_pilt_Click(object? sender, EventArgs e)
        {
            if (picturebox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
                saveFileDialog.Title = "Salvesta pilt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picturebox.Image.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("Puudub pilt, mida salvestada.");
            }
        }

        private void Filter_Click(object? sender, EventArgs e)
        {
            Bitmap redBlackEffect = (Bitmap)picturebox.Image.Clone();
            for (int yCoordinate = 0; yCoordinate < redBlackEffect.Height; yCoordinate++)
            {
                for (int xCoordinate = 0; xCoordinate < redBlackEffect.Width; xCoordinate++)
                {
                    Color color = redBlackEffect.GetPixel(xCoordinate, yCoordinate);
                    double grayColor = ((double)(color.R + color.G + color.B)) / 3.0d;
                    Color sepia = Color.FromArgb((byte)grayColor, (byte)(0), (byte)(0));
                    redBlackEffect.SetPixel(xCoordinate, yCoordinate, sepia);
                }
            }
            picturebox.Image = redBlackEffect;
        }

        private void Right_Click(object? sender, EventArgs e)
        {
            if (image != null)
            {
                // Поворачиваем изображение на 90 градусов
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                picturebox.Image = image; // Обновляем PictureBox
            }
        }

        private void Left_Click(object? sender, EventArgs e)
        {
            if (image != null)
            {
                // Поворачиваем изображение на 270 градусов
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                picturebox.Image = image; // Обновляем PictureBox
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
                image = Image.FromFile(openfiledialog.FileName); // Сохраняем изображение
                picturebox.Image = image; // Показываем изображение
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