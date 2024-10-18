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
        Button sap, ctp, close, stbc, left, right, filter;
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
            close.Click += Close_Click;

            //Button - Set the background color
            stbc = new Button();
            stbc.Text = "Set the background color";
            stbc.AutoSize = true;
            stbc.Click += Stbc_Click;

            //Button - Clear the picture
            ctp = new Button();
            ctp.Text = "Clear the picture";
            ctp.AutoSize = true;
            ctp.Click += Ctp_Click;

            //Button - Show a picture
            sap = new Button();
            sap.Text = "Show a picture";
            sap.AutoSize = true;
            sap.Click += Sap_Click;

            //Button - left
            left = new Button();
            left.Text = "Pööra vasakule";
            left.AutoSize = true;
            left.Click += Left_Click;

            //Button - right
            right = new Button();
            right.Text = "Pööra paremale";
            right.AutoSize = true;
            right.Click += Right_Click;

            //Button - filter
            filter = new Button();
            filter.Text = "Punane & Must";
            filter.AutoSize = true;
            filter.Click += Filter_Click;

            // Lisa buttons to FlowLayoutPanel
            tlp.Controls.Add(flp);
            flp.Controls.Add(stbc);
            flp.Controls.Add(ctp);
            flp.Controls.Add(left);
            flp.Controls.Add(right);
            flp.Controls.Add(filter);
            flp.Controls.Add(sap);
            flp.Controls.Add(close);


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