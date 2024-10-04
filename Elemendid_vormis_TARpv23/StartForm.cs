namespace Elemendid_vormis_TARpv23
{
    public partial class StartForm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkruut", "Raadionupp", "Raadionupp1", "Tekstikast"};
        List<string> rbtn_list = new List<string> { "Üks", "Kaks", "Kolm" };
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1, chk2;
        RadioButton rbtn, rbtn1, rbtn2, rbtn3;
        TextBox txt;
        
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
            //silt-label
            lbl = new Label();
            lbl.Text = "Aknade elemendid C# abil"; 
            lbl.Font=new Font("Arial", 30, FontStyle.Bold);
            lbl.Size = new Size(520, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover; ;
            lbl.MouseLeave += Lbl_MouseLeave;

            pbox = new PictureBox();
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, btn.Height+lbl.Height + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.Image=Image.FromFile(@"..\..\..\ookul.jpg");
            pbox.DoubleClick += Pbox_DoubleClick;


        }

        int tt = 0;
        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "lilla.jpg", "pruun.jpg", "roosa.jpg", "ookul.jpg" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\"+ fail);
            tt++;
            if (tt == 4) { tt = 0; }
        }

        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Castellar", 25, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(70,50,150,200);
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
            if(e.Node.Text=="Nupp")
            {
                Controls.Add(btn);
            }
            else if(e.Node.Text=="Silt")
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
            else if(e.Node.Text =="Raadionupp")
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
                for (int i = 0; i<rbtn_list.Count; i++)
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
                txt.Location = new Point(150+btn.Width +10, btn.Height);
                txt.Font = new Font("Arial", 30);
                txt.Width = 200;
                txt.TextChanged += Txt_TextChanged;
                Controls.Add(txt);
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
                lbl.Font= new Font("Blackadder ITC", 30, FontStyle.Italic);
                lbl.ForeColor= Color.PaleVioletRed;
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
