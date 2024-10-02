namespace Elemendid_vormis_TARpv23
{
    public partial class StartForm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkruut" };
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1, chk2;
        public StartForm()
        {
            this.Height = 500;
            this.Width = 700;
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
            }
            else if (chk2.Checked)
            {
                lbl.ForeColor = Color.AliceBlue;
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.ForeColor = Color.DarkSeaGreen;
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
            }
        }
    }
}
