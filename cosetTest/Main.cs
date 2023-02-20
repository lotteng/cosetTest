using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cosetTest
{
    public partial class Main : Form
    {
        // drag & drop
        Point point = new Point();

        Form fc = System.Windows.Forms.Application.OpenForms["Side"];
        Work work = new Work();


        // sliding menu (navigation bar)
        const int MAX_SLIDING_HEIGHT = 150;
        const int MIN_SLIDING_HEIGHT = 3;
        int SLIDING_LOCATION;

        const int STEP_SLIDING = 15;    // speed
        int _posSliding = 0;            // default sliding size (OPEN/CLOSE)
        int _stepSliding = 0;           // default sliding location (MOVE)




        public Main()
        {
            InitializeComponent();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            work.panelTop.Visible = false;
            work.panelSerial.Visible = false;
            work.panelBoard.Visible = false;
            work.panelFinal.Visible = false;


            // QuickBar
            comboQuick.Items.Add("바로가기");       // 파트장
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("실시간 공정 조회");
            comboQuick.Items.Add("실시간 자재 조회");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("자재 정보");
            comboQuick.Items.Add("제품 정보");
            comboQuick.Items.Add("BOM 정보");         
            comboQuick.Items.Add("────────────");   // 여기서부터는 root 전용
            comboQuick.Items.Add("생산계획");
            comboQuick.Items.Add("구매관리");
            comboQuick.Items.Add("자재관리");
            comboQuick.Items.Add("수입검사");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("조립-생산1팀");
            comboQuick.Items.Add("검사-생산1팀");
            comboQuick.Items.Add("파이버-생산1팀");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("조립-생산2팀");
            comboQuick.Items.Add("검사-생산2팀");
            
            if (comboQuick.Items.Count > 0) comboQuick.SelectedIndex = 0;


        }

        // open other form quickly ---(2 ways)---> [ select from combo / press enter key ]
        private void comboQuick_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboQuick_SelectedIndexChanged(sender, e);
            }
        }

        private void comboQuick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboQuick.Text == "실시간 공정 조회")
            {
                Total FormTotal = new Total();

                // Form state check (IsOpen)
                Form fc = System.Windows.Forms.Application.OpenForms["Total"];
                if (fc != null)
                {
                    fc.Close();
                    
                }


                FormTotal.Show();

            }
        }





        // sliding menu (navigation bar)

        private void lblNavi1_MouseHover(object sender, EventArgs e)
        {
            lblNavisub1.Visible = true;

            timerNaviOpen.Start();
            panelNaviMove.BackColor = SystemColors.ActiveCaption;
            //timerNaviMove.Start();
            SLIDING_LOCATION = 20;

            panelNaviMove.Location = new System.Drawing.Point(SLIDING_LOCATION, 91);
        }
        private void lblNavi1_MouseLeave(object sender, EventArgs e)
        {
            lblNavisub1.Visible = false;

            panelNaviMove.BackColor = Color.Gainsboro;
        }

        private void lblNavi2_MouseHover(object sender, EventArgs e)
        {
            lblNavisub2.Visible = true;

            timerNaviOpen.Start();
            panelNaviMove.BackColor = SystemColors.ActiveCaption;
            //timerNaviMove.Start();
            SLIDING_LOCATION = 120;

            panelNaviMove.Location = new System.Drawing.Point(SLIDING_LOCATION, 91);
        }

        private void lblNavi2_MouseLeave(object sender, EventArgs e)
        {
            lblNavisub2.Visible = false;

            panelNaviMove.BackColor = Color.Gainsboro;
        }

        private void timerNaviMove_Tick(object sender, EventArgs e)
        {

            //if ( SLIDING_LOCATION >= panelNaviMove.Location.X)
            //{
            //    _stepSliding += 2;
            //}

            //else
            //{
            //    _stepSliding -= 2;
            //}

            //if (SLIDING_LOCATION == panelNaviMove.Location.X)
            //{
            //    timerNaviMove.Stop();
            //}

            // panelNaviMove.Location = new Point(_stepSliding, 84);

        }


        private void panelDrag_MouseHover(object sender, EventArgs e)
        {
            timerNaviClose.Start();
        }

        private void panelNavigation_MouseLeave(object sender, EventArgs e)
        {
            //imerNaviOpen.Stop();
            //timerNaviClose.Start();
        }
        private void timerNaviOpen_Tick(object sender, EventArgs e)
        {
            timerNaviClose.Stop();


            if (_posSliding >= MAX_SLIDING_HEIGHT)
            {
                timerNaviOpen.Stop();
            }
            else
            {
                _posSliding += STEP_SLIDING;
            }
            panelNavigation.Height = _posSliding;

        }

        private void timerNaviClose_Tick(object sender, EventArgs e)
        {
            timerNaviOpen.Stop();


            panelNavigation.Height = MIN_SLIDING_HEIGHT;

            timerNaviClose.Stop();

            // 닫기 애니메이션 (삭제)
            //if (_posSliding <= MIN_SLIDING_HEIGHT)
            //{
            //    timerNaviClose.Stop();
            //}
            //else
            //{
            //    _posSliding -= STEP_SLIDING;
            //}

            //panelNavigation.Height = _posSliding;

        }



        // Click process on Menu
        private void lblMenu1_Click(object sender, EventArgs e)
        {
            Menu_Side(this.Name);

        }

        private void lblMenu13_Click(object sender, EventArgs e)
        {
            Work work = new Work();
            work.FormBorderStyle= FormBorderStyle.None;
            work.TopLevel = false;
            work.Show();
            panelMain.Controls.Add(work);
            work.Dock = DockStyle.Fill;
        }



        private void Menu_Side(String process)
        {
            // Form state(open) check

            if (fc != null)
            {
                fc.Close();
            }


            Side FormSide = new Side(this);

            FormSide.StartPosition = FormStartPosition.Manual;
            FormSide.Location = new System.Drawing.Point(this.Location.X + 1000, this.Location.Y);
            FormSide.Show();

            work.imgSide.Image = Properties.Resources.arrow_bck;
        }

        private void Menu_Only(String process)
        {

        }


        // side
        private void imgSide_Click(object sender, EventArgs e)
        {

            // Form state(open) check

            if (fc != null)
            {
                //fc.Close();
                fc.Hide();

                work.imgSide.Image = Properties.Resources.arrow_nxt;
            }

            else
            {
                Side FormSide = new Side(this);

                FormSide.StartPosition = FormStartPosition.Manual;
                FormSide.Location = new System.Drawing.Point(this.Location.X + 1000, this.Location.Y);
                FormSide.Show();

                work.imgSide.Image = Properties.Resources.arrow_bck;
            }

        }

        private void imgSide_MouseHover(object sender, EventArgs e)
        {
            work.imgSide.BackColor = Color.LightGray;
        }

        private void imgSide_MouseLeave(object sender, EventArgs e)
        {
            work.imgSide.BackColor = Color.Gainsboro;
        }




        // help control
        private void imgHelp_MouseHover(object sender, EventArgs e)
        {
            work.panelHelp.Visible = true;
        }

        private void imgHelp_MouseLeave(object sender, EventArgs e)
        {
            work.panelHelp.Visible = false;
        }


        private void imgSerialView_MouseHover(object sender, EventArgs e)
        {
            work.lblSerialView.Visible = true;
        }

        private void imgSerialView_MouseLeave(object sender, EventArgs e)
        {
            work.lblSerialView.Visible = false;
        }

        private void imgSubmit_MouseHover(object sender, EventArgs e)
        {
            work.lblSubmit.Visible = true;
        }

        private void imgSubmit_MouseLeave(object sender, EventArgs e)
        {
            work.lblSubmit.Visible = false;
        }

        private void imgDefect_MouseHover(object sender, EventArgs e)
        {
            work.lblDefect.Visible = true;
        }

        private void imgDefect_MouseLeave(object sender, EventArgs e)
        {
            work.lblDefect.Visible = false;
        }






        // spec

        String[] spec = new string[10];

        private void imgSpec_Click(object sender, EventArgs e)
        {   
            if (!work.textSpec1.Enabled)
            {   
                if (!string.IsNullOrWhiteSpace(work.textSpec1.Text)) spec[0] = work.textSpec1.Text;   // overflow index 방지 if문
                if (!string.IsNullOrWhiteSpace(work.textSpec2.Text)) spec[1] = work.textSpec2.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec3.Text)) spec[2] = work.textSpec3.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec4.Text)) spec[3] = work.textSpec4.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec5.Text)) spec[4] = work.textSpec5.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec6.Text)) spec[5] = work.textSpec6.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec7.Text)) spec[6] = work.textSpec7.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec8.Text)) spec[7] = work.textSpec8.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec9.Text)) spec[8] = work.textSpec9.Text;
                if (!string.IsNullOrWhiteSpace(work.textSpec10.Text)) spec[9] = work.textSpec10.Text;

                work.textSpec1.Enabled = true; work.textSpec1.BackColor = Color.White;
                work.textSpec2.Enabled = true; work.textSpec2.BackColor = Color.White;
                work.textSpec3.Enabled = true; work.textSpec3.BackColor = Color.White;
                work.textSpec4.Enabled = true; work.textSpec4.BackColor = Color.White;
                work.textSpec5.Enabled = true; work.textSpec5.BackColor = Color.White;
                work.textSpec6.Enabled = true; work.textSpec6.BackColor = Color.White;
                work.textSpec7.Enabled = true; work.textSpec7.BackColor = Color.White;
                work.textSpec8.Enabled = true; work.textSpec8.BackColor = Color.White;
                work.textSpec9.Enabled = true; work.textSpec9.BackColor = Color.White;
                work.textSpec10.Enabled = true; work.textSpec10.BackColor = Color.White;

            }
            else
            {
                if (MessageBox.Show("변경사항을 저장하시겠습니까?", "Spec 변경", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    if (!string.IsNullOrWhiteSpace(spec[0])) work.textSpec1.Text = spec[0].ToString();   // overflow index 방지 if문
                    if (!string.IsNullOrWhiteSpace(spec[1])) work.textSpec2.Text = spec[1].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[2])) work.textSpec3.Text = spec[2].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[3])) work.textSpec4.Text = spec[3].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[4])) work.textSpec5.Text = spec[4].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[5])) work.textSpec6.Text = spec[5].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[6])) work.textSpec7.Text = spec[6].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[7])) work.textSpec8.Text = spec[7].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[8])) work.textSpec9.Text = spec[8].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[9])) work.textSpec10.Text = spec[9].ToString();
                }

                work.textSpec1.Enabled = false; work.textSpec1.BackColor = Color.LightGray;
                work.textSpec2.Enabled = false; work.textSpec2.BackColor = Color.LightGray;
                work.textSpec3.Enabled = false; work.textSpec3.BackColor = Color.LightGray;
                work.textSpec4.Enabled = false; work.textSpec4.BackColor = Color.LightGray;
                work.textSpec5.Enabled = false; work.textSpec5.BackColor = Color.LightGray;
                work.textSpec6.Enabled = false; work.textSpec6.BackColor = Color.LightGray;
                work.textSpec7.Enabled = false; work.textSpec7.BackColor = Color.LightGray;
                work.textSpec8.Enabled = false; work.textSpec8.BackColor = Color.LightGray;
                work.textSpec9.Enabled = false; work.textSpec9.BackColor = Color.LightGray;
                work.textSpec10.Enabled = false; work.textSpec10.BackColor = Color.LightGray;
            }
            
        }


        private void imgReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("초기 상태로 되돌립니다.", "Spec 초기화", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                work.textSpec1.Text = "246.5483";
                work.textSpec2.Text = "157";
                work.textSpec3.Text = "159.73";
                work.textSpec4.Text = "159.79";
                work.textSpec5.Text = "156.48";
                work.textSpec6.Text = "157.67";
                work.textSpec7.Text = "";
                work.textSpec8.Text = 393.ToString();
                work.textSpec9.Text = 379.ToString();
                work.textSpec10.Text = "";

                work.textSpec1.Enabled = false; work.textSpec1.BackColor = Color.LightGray;
                work.textSpec2.Enabled = false; work.textSpec2.BackColor = Color.LightGray;
                work.textSpec3.Enabled = false; work.textSpec3.BackColor = Color.LightGray;
                work.textSpec4.Enabled = false; work.textSpec4.BackColor = Color.LightGray;
                work.textSpec5.Enabled = false; work.textSpec5.BackColor = Color.LightGray;
                work.textSpec6.Enabled = false; work.textSpec6.BackColor = Color.LightGray;
                work.textSpec7.Enabled = false; work.textSpec7.BackColor = Color.LightGray;
                work.textSpec8.Enabled = false; work.textSpec8.BackColor = Color.LightGray;
                work.textSpec9.Enabled = false; work.textSpec9.BackColor = Color.LightGray;
                work.textSpec10.Enabled = false; work.textSpec10.BackColor = Color.LightGray;
            }
        }







        // remark

        String remark;
        
        private void imgRemark_Click(object sender, EventArgs e)
        {
            if ( !work.textRemark.Enabled )
            {
                remark = work.textRemark.Text;

                work.textRemark.Enabled = true;
                work.textRemark.BackColor = Color.White;
            }
            else
            {

                if (MessageBox.Show("변경사항을 저장하시겠습니까?", "Remark 변경", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    work.textRemark.Text = remark;
                }

                work.textRemark.Enabled = false;
                work.textRemark.BackColor = Color.WhiteSmoke;
                
            }
        }





        // drag & drop (Move For Form)


        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            point = new System.Drawing.Point(e.X, e.Y);
        }

        private void panelDrag_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Location = new System.Drawing.Point(this.Left - (point.X - e.X), this.Top - (point.Y - e.Y));
            }

        }



        private void imgMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void imgExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }


    }
}
