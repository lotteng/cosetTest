using Microsoft.Office.Interop.Excel;
using MySqlX.XDevAPI;
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

        Total FormTotal = new Total();
        Form fc = System.Windows.Forms.Application.OpenForms["Side"];

        private System.Drawing.Point point = new System.Drawing.Point(); // drag & drop


        // sliding menu (navigation bar)
        const int MAX_SLIDING_HEIGHT = 150;
        const int MIN_SLIDING_HEIGHT = 3;
        int SLIDING_LOCATION;

        const int STEP_SLIDING = 12;    // speed
        int _posSliding = 0;            // default sliding size (OPEN/CLOSE)
        int _stepSliding = 0;           // default sliding location (MOVE)




        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            panelTop.Visible = false;
            panelSerial.Visible = false;
            panelBoard.Visible = false;
            panelFinal.Visible = false;


            // 조회용
            comboQuick.Items.Add("바로가기");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("생산진행현황");            
            comboQuick.Items.Add("정보조회"); //자재 제품 BOM
            comboQuick.Items.Add("3SPT FBG 조회");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("수입검사");
            comboQuick.Items.Add("구매관리");
            comboQuick.Items.Add("자재관리");
            comboQuick.Items.Add("생산계획");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("조립-생산1팀");
            comboQuick.Items.Add("검사-생산1팀");
            comboQuick.Items.Add("파이버-생산1팀");
            comboQuick.Items.Add("────────────");
            comboQuick.Items.Add("조립-생산2팀");
            comboQuick.Items.Add("검사-생산2팀");
            
            
            



            if (comboQuick.Items.Count > 0) comboQuick.SelectedIndex = 0;


            dataGridViewSerial.Rows.Add(false, 1, "SK23040511");
            dataGridViewSerial.Rows.Add(false, 2, "SK23040512");
            dataGridViewSerial.Rows.Add(false, 3, "SK23040513");

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


            if (_posSliding <= MIN_SLIDING_HEIGHT)
            {
                timerNaviClose.Stop();
            }
            else
            {
                _posSliding -= STEP_SLIDING;
            }

            panelNavigation.Height = _posSliding;
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
            if (comboQuick.Text == "생산진행현황")
            {
                // Form state check (IsOpen)
                Form fc = System.Windows.Forms.Application.OpenForms["Total"];
                if (fc != null)
                {
                    fc.Close();
                }


                FormTotal.Show();

            }
        }



        // final (Test Process)
        private void lblFinal_Click(object sender, EventArgs e)
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

                imgSide.Image = Properties.Resources.arrow_bck;
            

        }

        // side
        private void imgSide_Click(object sender, EventArgs e)
        {

            // Form state(open) check


            if (fc != null)
            {
                //fc.Close();
                fc.Hide();

                imgSide.Image = Properties.Resources.arrow_nxt;
            }

            else
            {
                Side FormSide = new Side(this);

                FormSide.StartPosition = FormStartPosition.Manual;
                FormSide.Location = new System.Drawing.Point(this.Location.X + 1000, this.Location.Y);
                FormSide.Show();

                imgSide.Image = Properties.Resources.arrow_bck;
            }

            //if (fc != null)
            //{
            //    //fc.Close();
            //    fc.Hide();

            //    imgSide.Image = Properties.Resources.arrow_nxt;
            //}

            //else
            //{
            //    Side FormSide = new Side(this);

            //    FormSide.StartPosition = FormStartPosition.Manual;
            //    FormSide.Location = new System.Drawing.Point(this.Location.X + 1000, this.Location.Y);
            //    FormSide.Show();

            //    imgSide.Image = Properties.Resources.arrow_bck;
            //}
        }

        private void imgSide_MouseHover(object sender, EventArgs e)
        {
            imgSide.BackColor = Color.LightGray;
        }

        private void imgSide_MouseLeave(object sender, EventArgs e)
        {
            imgSide.BackColor = Color.Gainsboro;
        }




        // help control
        private void imgHelp_MouseHover(object sender, EventArgs e)
        {
            panelHelp.Visible = true;
        }

        private void imgHelp_MouseLeave(object sender, EventArgs e)
        {
            panelHelp.Visible = false;
        }


        private void imgSerialView_MouseHover(object sender, EventArgs e)
        {
            lblSerialView.Visible = true;
        }

        private void imgSerialView_MouseLeave(object sender, EventArgs e)
        {
            lblSerialView.Visible = false;
        }

        private void imgSubmit_MouseHover(object sender, EventArgs e)
        {
            lblSubmit.Visible = true;
        }

        private void imgSubmit_MouseLeave(object sender, EventArgs e)
        {
            lblSubmit.Visible = false;
        }

        private void imgDefect_MouseHover(object sender, EventArgs e)
        {
            lblDefect.Visible = true;
        }

        private void imgDefect_MouseLeave(object sender, EventArgs e)
        {
            lblDefect.Visible = false;
        }






        // spec

        String[] spec = new string[10];

        private void imgSpec_Click(object sender, EventArgs e)
        {   
            if (!textSpec1.Enabled)
            {   
                if (!string.IsNullOrWhiteSpace(textSpec1.Text)) spec[0] = textSpec1.Text;   // overflow index 방지 if문
                if (!string.IsNullOrWhiteSpace(textSpec2.Text)) spec[1] = textSpec2.Text;
                if (!string.IsNullOrWhiteSpace(textSpec3.Text)) spec[2] = textSpec3.Text;
                if (!string.IsNullOrWhiteSpace(textSpec4.Text)) spec[3] = textSpec4.Text;
                if (!string.IsNullOrWhiteSpace(textSpec5.Text)) spec[4] = textSpec5.Text;
                if (!string.IsNullOrWhiteSpace(textSpec6.Text)) spec[5] = textSpec6.Text;
                if (!string.IsNullOrWhiteSpace(textSpec7.Text)) spec[6] = textSpec7.Text;
                if (!string.IsNullOrWhiteSpace(textSpec8.Text)) spec[7] = textSpec8.Text;
                if (!string.IsNullOrWhiteSpace(textSpec9.Text)) spec[8] = textSpec9.Text;
                if (!string.IsNullOrWhiteSpace(textSpec10.Text)) spec[9] = textSpec10.Text;

                textSpec1.Enabled = true;   textSpec1.BackColor = Color.White;
                textSpec2.Enabled = true;   textSpec2.BackColor = Color.White;
                textSpec3.Enabled = true;   textSpec3.BackColor = Color.White;
                textSpec4.Enabled = true;   textSpec4.BackColor = Color.White;
                textSpec5.Enabled = true;   textSpec5.BackColor = Color.White;
                textSpec6.Enabled = true;   textSpec6.BackColor = Color.White;
                textSpec7.Enabled = true;   textSpec7.BackColor = Color.White;
                textSpec8.Enabled = true;   textSpec8.BackColor = Color.White;
                textSpec9.Enabled = true;   textSpec9.BackColor = Color.White;
                textSpec10.Enabled = true;  textSpec10.BackColor = Color.White;

            }
            else
            {
                if (MessageBox.Show("변경사항을 저장하시겠습니까?", "Spec 변경", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    if (!string.IsNullOrWhiteSpace(spec[0])) textSpec1.Text = spec[0].ToString();   // overflow index 방지 if문
                    if (!string.IsNullOrWhiteSpace(spec[1])) textSpec2.Text = spec[1].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[2])) textSpec3.Text = spec[2].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[3])) textSpec4.Text = spec[3].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[4])) textSpec5.Text = spec[4].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[5])) textSpec6.Text = spec[5].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[6])) textSpec7.Text = spec[6].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[7])) textSpec8.Text = spec[7].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[8])) textSpec9.Text = spec[8].ToString();
                    if (!string.IsNullOrWhiteSpace(spec[9])) textSpec10.Text = spec[9].ToString();
                }

                textSpec1.Enabled = false;  textSpec1.BackColor = Color.LightGray;
                textSpec2.Enabled = false;  textSpec2.BackColor = Color.LightGray;
                textSpec3.Enabled = false;  textSpec3.BackColor = Color.LightGray;
                textSpec4.Enabled = false;  textSpec4.BackColor = Color.LightGray;
                textSpec5.Enabled = false;  textSpec5.BackColor = Color.LightGray;
                textSpec6.Enabled = false;  textSpec6.BackColor = Color.LightGray;
                textSpec7.Enabled = false;  textSpec7.BackColor = Color.LightGray;
                textSpec8.Enabled = false;  textSpec8.BackColor = Color.LightGray;
                textSpec9.Enabled = false;  textSpec9.BackColor = Color.LightGray;
                textSpec10.Enabled = false; textSpec10.BackColor = Color.LightGray;
            }
            
        }


        private void imgReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("초기 상태로 되돌립니다.", "Spec 초기화", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                textSpec1.Text = "246.5483";
                textSpec2.Text = "157";
                textSpec3.Text = "159.73";
                textSpec4.Text = "159.79";
                textSpec5.Text = "156.48";
                textSpec6.Text = "157.67";
                textSpec7.Text = "";
                textSpec8.Text = 393.ToString();
                textSpec9.Text = 379.ToString();
                textSpec10.Text = "";

                textSpec1.Enabled = false; textSpec1.BackColor = Color.LightGray;
                textSpec2.Enabled = false; textSpec2.BackColor = Color.LightGray;
                textSpec3.Enabled = false; textSpec3.BackColor = Color.LightGray;
                textSpec4.Enabled = false; textSpec4.BackColor = Color.LightGray;
                textSpec5.Enabled = false; textSpec5.BackColor = Color.LightGray;
                textSpec6.Enabled = false; textSpec6.BackColor = Color.LightGray;
                textSpec7.Enabled = false; textSpec7.BackColor = Color.LightGray;
                textSpec8.Enabled = false; textSpec8.BackColor = Color.LightGray;
                textSpec9.Enabled = false; textSpec9.BackColor = Color.LightGray;
                textSpec10.Enabled = false; textSpec10.BackColor = Color.LightGray;
            }
        }







        // remark

        String remark;
        
        private void imgRemark_Click(object sender, EventArgs e)
        {
            if ( !textRemark.Enabled )
            {
                remark = textRemark.Text;

                textRemark.Enabled = true;
                textRemark.BackColor = Color.White;
            }
            else
            {

                if (MessageBox.Show("변경사항을 저장하시겠습니까?", "Remark 변경", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    textRemark.Text = remark;
                }

                textRemark.Enabled = false;
                textRemark.BackColor = Color.WhiteSmoke;
                
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
