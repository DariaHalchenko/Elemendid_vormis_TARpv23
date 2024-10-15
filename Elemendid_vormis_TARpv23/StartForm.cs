using Microsoft.VisualBasic;
using System.Data;

namespace Elemendid_vormis_TARpv23
{
    public partial class StartForm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkruut", "Raadionupp", "Raadionupp1", "Tekstikast", "Loetelu", "Tabel", "Dialogaknad", "Picture Viewer", "Math Quiz", "Matching Game" };
        List<string> rbtn_list = new List<string> { "Üks", "Kaks", "Kolm" };
        TreeView tree;
        Button btn, btn1, btn2, btn3;
        Label lbl;
        PictureBox pbox, pbox2;
        CheckBox chk1, chk2;
        RadioButton rbtn, rbtn1, rbtn2, rbtn3;
        TextBox txt;
        ListBox lb;
        DataSet ds;
        DataGridView dg;

        public StartForm()
        {
            this.Height = 800;
            this.Width = 900;
            this.Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid: ");
            foreach (var element in elemendid)
            {
                tn.Nodes.Add(new TreeNode(element));
            }

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
            //nupp-кнопка
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 70;
            btn.Location = new Point(150, 50);
            btn.Click += Btn_Click;
            //nupp1-Picture Viewer
            btn1 = new Button();
            btn1.Text = "Picture Viewer";
            btn1.Height = 60;
            btn1.Width = 90;
            btn1.Location = new Point(150, 500);
            btn1.Click += Btn1_Click;
            //nupp2-Math Quiz
            btn2 = new Button();
            btn2.Text = "Math Quiz";
            btn2.Height = 60;
            btn2.Width = 90;
            btn2.Location = new Point(250, 500);
            btn2.Click += Btn2_Click;
            //nupp3-Matching Game
            btn3 = new Button();
            btn3.Text = "Matching Game";
            btn3.Height = 60;
            btn3.Width = 90;
            btn3.Location = new Point(350, 500);
            btn3.Click += Btn3_Click;


            //silt-label
            lbl = new Label();
            lbl.Text = "Aknade elemendid C# abil";
            lbl.Font = new Font("Arial", 30, FontStyle.Bold);
            lbl.Size = new Size(520, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover; ;
            lbl.MouseLeave += Lbl_MouseLeave;

            pbox = new PictureBox();
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, btn.Height + lbl.Height + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.Image = Image.FromFile(@"..\..\..\ookul.jpg");
            pbox.DoubleClick += Pbox_DoubleClick;


        }

        private void Btn3_Click(object? sender, EventArgs e)
        {
            NeljasVorm neljasVorm = new NeljasVorm(800, 800);
            neljasVorm.Show();
            btn3.BackColor = Color.RosyBrown;
        }

        private void Btn2_Click(object? sender, EventArgs e)
        {
            KolmVorm kolmVorm = new KolmVorm(800, 450);
            kolmVorm.Show();
            btn2.BackColor = Color.Turquoise;
        }

        private void Btn1_Click(object? sender, EventArgs e)
        {
            TeineVorm teineVorm = new TeineVorm(800, 900);
            teineVorm.Show();
            btn1.BackColor = Color.Violet;
        }

        int tt = 0;
        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "lilla.jpg", "pruun.jpg", "roosa.jpg", "ookul.jpg" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 4) { tt = 0; }
        }

        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Castellar", 25, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(70, 50, 150, 200);
            lbl.BackColor = Color.LightYellow;
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Algerian", 30, FontStyle.Regular);
            lbl.ForeColor = Color.MediumVioletRed;
            lbl.BackColor = Color.AliceBlue;
        }

        int t = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            t++;
            if (t % 2 == 0)
            {
                btn.BackColor = Color.Yellow;
            }
            else
            {
                btn.BackColor = Color.Orange;
            }
        }

        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp")
            {
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Märkruut")
            {
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(chk1.Text.Length * 10, chk1.Size.Height);
                chk1.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + 10);
                chk1.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                chk2 = new CheckBox();
                chk2.Checked = false;
                chk2.Size = pbox.Size;
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\ookul.jpg");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + 15);
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                Controls.Add(chk1);
                Controls.Add(chk2);
            }
            else if (e.Node.Text == "Raadionupp")
            {
                rbtn1 = new RadioButton();
                rbtn1.Checked = false;
                rbtn1.Text = "TARpv23";
                rbtn1.Location = new Point(150, 420);
                rbtn1.CheckedChanged += Rbtnd_CheckedChanged;

                rbtn2 = new RadioButton();
                rbtn2.Checked = false;
                rbtn2.Text = "LOGITpv23";
                rbtn2.Location = new Point(150, 440);
                rbtn2.CheckedChanged += Rbtnd_CheckedChanged;

                rbtn3 = new RadioButton();
                rbtn3.Checked = false;
                rbtn3.Text = "TITpv23";
                rbtn3.Location = new Point(150, 460);
                rbtn3.CheckedChanged += Rbtnd_CheckedChanged;

                Controls.Add(rbtn1);
                Controls.Add(rbtn2);
                Controls.Add(rbtn3);
            }
            else if (e.Node.Text == "Raadionupp1")
            {
                int x = 20;
                for (int i = 0; i < rbtn_list.Count; i++)
                {
                    rbtn = new RadioButton();
                    rbtn.Checked = false;
                    rbtn.Text = rbtn_list[i];
                    rbtn.Height = x;
                    x = x + 20;
                    rbtn.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + chk2.Height + rbtn.Height);
                    rbtn.CheckedChanged += new EventHandler(Btn_CheckedChanged);

                    Controls.Add(rbtn);
                }
            }
            else if (e.Node.Text == "Tekstikast")
            {
                txt = new TextBox();
                txt.Location = new Point(150 + btn.Width + 10, btn.Height);
                txt.Font = new Font("Arial", 10);
                txt.Width = 200;
                txt.TextChanged += Txt_TextChanged;
                Controls.Add(txt);
            }
            else if (e.Node.Text == "Loetelu")
            {
                lb = new ListBox();
                foreach (string item in rbtn_list)
                {
                    lb.Items.Add(item);
                }
                lb.Height = 50;
                lb.Location = new Point(160 + btn.Width + txt.Width, btn.Height);
                lb.SelectedIndexChanged += Lb_SelectedIndexChanged;
                Controls.Add(lb);
            }
            else if (e.Node.Text == "Tabel")
            {
                ds = new DataSet("XML file");
                ds.ReadXml(@"..\..\..\menu.xml");
                dg = new DataGridView();
                dg.Location = new Point(155 + chk1.Width + 25, txt.Height + lbl.Height + 30);
                dg.DataSource = ds;
                dg.DataMember = "food";
                dg.RowHeaderMouseClick += Dg_RowHeaderMouseClick;
                Controls.Add(dg);
            }
            else if (e.Node.Text == "Dialogaknad")
            {
                MessageBox.Show("Dialoog", "See on lihtne aken");
                var vastus = MessageBox.Show("Sisestame andmed", "Kas tahad InputBoxi kasutada?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vastus == DialogResult.Yes)
                {
                    string text = Interaction.InputBox("Sisesta midagi siia", "andmete sisestamine"); MessageBox.Show("Oli sisestanud : " + text);
                    Random random = new Random();
                    DataRow dr = ds.Tables["food"].NewRow();
                    dr["name"] = text;
                    dr["price"] = "$" + (random.NextSingle() * 10).ToString();
                    dr["description"] = "Väga maitsev ";
                    dr["calories"] = random.Next(10, 100);

                    ds.Tables["food"].Rows.Add(dr);
                    if (ds == null) { return; }
                    ds.WriteXml(@"..\..\..\menu.xml");
                    MessageBox.Show("Oli sisestatud" + text);
                }
            }
            else if (e.Node.Text == "Picture Viewer")
            {
                Controls.Add(btn1);
            }
            else if (e.Node.Text == "Math Quiz")
            {
                Controls.Add(btn2);
            }
            else if (e.Node.Text == "Matching Game")
            {
                Controls.Add(btn3);
            }
        }

        private void Dg_RowHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            txt.Text = dg.Rows[e.RowIndex].Cells[0].Value.ToString() + " hind " + dg.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void Lb_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (lb.SelectedIndex)
            {
                case 0: tree.BackColor = Color.Sienna; break;
                case 1: tree.BackColor = Color.Cyan; break;
                case 2: tree.BackColor = Color.DarkOrchid; break;
            }
        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {
            lbl.Text = txt.Text;
        }

        private void Btn_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            lbl.Text = rb.Text;
        }

        private void Rbtnd_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                this.BackColor = Color.AliceBlue;
                this.ForeColor = Color.OrangeRed;
                lbl.Font = new Font("Blackadder ITC", 30, FontStyle.Italic);
                lbl.ForeColor = Color.PaleVioletRed;
            }
            else if (rbtn2.Checked)
            {
                this.BackColor = Color.MistyRose;
                this.ForeColor = Color.PaleVioletRed;
                lbl.Font = new Font("Arial Rounded MT Bold", 25, FontStyle.Italic);
                lbl.ForeColor = Color.HotPink;
            }
            else if (rbtn3.Checked)
            {
                this.BackColor = Color.FloralWhite;
                this.ForeColor = Color.OrangeRed;
                lbl.Font = new Font("Bauhaus 93", 25, FontStyle.Underline);
                lbl.ForeColor = Color.Orchid;
            }
            else
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.OrangeRed;
            }

        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.ForeColor = Color.IndianRed;
                lbl.BorderStyle = BorderStyle.FixedSingle;
                pbox.BorderStyle = BorderStyle.Fixed3D;

            }
            else if (chk1.Checked)
            {

                lbl.ForeColor = Color.YellowGreen;
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.None;
                pbox.Image = Image.FromFile(@"..\..\..\lilla.jpg");
            }
            else if (chk2.Checked)
            {
                lbl.ForeColor = Color.Orchid;
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.ForeColor = Color.MediumBlue;
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
                pbox.Image = Image.FromFile(@"..\..\..\roosa.jpg");
            }
        }
    }
}
