using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cosetTest
{
    public partial class Main : Form
    {

        private Point point = new Point(); // drag & drop

        public Main()
        {
            InitializeComponent();
        }



        // drag & drop (Move For Form)
        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panelDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Location = new Point(this.Left - (point.X - e.X), this.Top - (point.Y - e.Y));
            }
        }



        // minimize
        private void imgMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        // exit
        private void imgExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show ("프로그램을 종료하시겠습니까?","종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Form state check (IsOpen)
            Form fc = Application.OpenForms["Total"];
            if (fc != null)
            {
                fc.Close();
            }

            // Open Form
            Total FormTotal = new Total();
            FormTotal.Show();
        }
    }
}
