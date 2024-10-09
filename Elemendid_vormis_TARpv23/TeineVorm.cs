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
    public partial class TeineVorm : Form
    {
        TableLayoutPanel tlp;
        FlowLayoutPanel flp;
        Button sap, ctp, close, stbc;
        PictureBox picture;
        CheckBox chk;
        OpenFileDialog openfiledialog;
        ColorDialog colordialog;

        public TeineVorm(int w, int h)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Picture Viewer";

            // TableLayoutPanel
            tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;

            // PictureBox
            picture = new PictureBox();
            picture.Dock = DockStyle.Fill;
            picture.BorderStyle = BorderStyle.Fixed3D;
            tlp.Controls.Add(picture);
            tlp.SetColumnSpan(picture, 2);

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

            // Lisa buttons to FlowLayoutPanel
            tlp.Controls.Add(flp);
            flp.Controls.Add(stbc);
            flp.Controls.Add(ctp);
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

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            picture.SizeMode = chk.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Normal;
        }

        private void Sap_Click(object? sender, EventArgs e)
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                picture.Load(openfiledialog.FileName);
            }
        }

        private void Ctp_Click(object? sender, EventArgs e)
        {
            picture.Image = null;
        }

        private void Stbc_Click(object? sender, EventArgs e)
        {
            if (colordialog.ShowDialog() == DialogResult.OK)
                picture.BackColor = colordialog.Color;
        }

        private void Close_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}

