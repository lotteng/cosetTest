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


        // sliding menu (navigation bar)
        const int MAX_SLIDING_HEIGHT = 110;
        const int MIN_SLIDING_HEIGHT = 3;
        int SLIDING_LOCATION;

        const int STEP_SLIDING = 10;    // speed
        int _posSliding = 3;            // default sliding size (OPEN/CLOSE)
        int _stepSliding = 0;           // default sliding location (MOVE)




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


        // sliding menu (navigation bar)

        private void label16_MouseHover(object sender, EventArgs e)
        {
            timerNaviOpen.Start();
            timerNaviMove.Start();
            SLIDING_LOCATION = 8;

        }
        private void label13_MouseHover(object sender, EventArgs e)
        {
            timerNaviOpen.Start();
            timerNaviMove.Start();
            SLIDING_LOCATION = 88;
        }


        private void timerNaviMove_Tick(object sender, EventArgs e)
        {

            if ( SLIDING_LOCATION >= panelNaviMove.Location.X)
            {
                _stepSliding += 2;
            }

            else
            {
                _stepSliding -= 2;
            }

            if (SLIDING_LOCATION == panelNaviMove.Location.X)
            {
                timerNaviMove.Stop();
            }

            panelNaviMove.Location = new Point(_stepSliding, 84);
        }


        private void panelDrag_MouseHover(object sender, EventArgs e)
        {
            timerNaviClose.Start();
        }
        private void panelNavigation_MouseLeave(object sender, EventArgs e)
        {
            timerNaviClose.Start();
        }

        private void timerNaviOpen_Tick(object sender, EventArgs e)
        {
            timerNaviClose.Stop();

            _posSliding += STEP_SLIDING;
            if (_posSliding >= MAX_SLIDING_HEIGHT)
            {
                timerNaviOpen.Stop();
            }

            panelNavigation.Height = _posSliding;

        }

        private void timerNaviClose_Tick(object sender, EventArgs e)
        {
            timerNaviOpen.Stop();

            _posSliding -= STEP_SLIDING;
            if (_posSliding <= MIN_SLIDING_HEIGHT)
            {
                timerNaviClose.Stop();
            }

            panelNavigation.Height = _posSliding;
        }












        private void imgMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

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

            Total FormTotal = new Total();
            FormTotal.Show();
        }





        private void imgHelp_MouseHover(object sender, EventArgs e)
        {
            panelHelp.Visible = true;
        }

        private void imgHelp_MouseLeave(object sender, EventArgs e)
        {
            panelHelp.Visible = false;
        }








        private void panel1_Enter(object sender, EventArgs e)
        {
            panel5.BackColor = SystemColors.ActiveCaption;
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Gainsboro;
        }

        private void panel2_Enter(object sender, EventArgs e)
        {
            panel4.BackColor = SystemColors.ActiveCaption;
        }

        private void panel2_Leave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Gainsboro;
        }


    }
}
