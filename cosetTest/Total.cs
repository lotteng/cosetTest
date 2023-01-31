using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Text;
using Org.BouncyCastle.Asn1.Crmf;

namespace cosetTest
{
    public partial class Total : Form
    {

        private Point point = new Point(); // drag & drop


        ////add shadow
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        const int CS_DROPSHADOW = 0x20000;
        //        CreateParams cp = base.CreateParams;
        //        cp.ClassStyle |= CS_DROPSHADOW;
        //        return cp;
        //    }
        //}



        public Total()
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
            {
                this.Close();
            }
        }

        // Open Form : Main
        private void imgLogo_Click(object sender, EventArgs e)
        {
            // Form state(open) check
            Form fc = Application.OpenForms["Main"];
            if (fc != null)
            {
                fc.Close();
            }

            // Open Form
            Main FormMain = new Main();
            FormMain.Show();
        }






        // default setting
        private void Total_Load(object sender, EventArgs e)
        {
            string year, month ;

            DateTime thisDay = DateTime.Today;

            year = thisDay.ToString("yy");
            month = thisDay.ToString("MM");

            comboRequest1.Items.Add("A08");

            //-- string -> int 로 변환한 뒤, 반복문으로 콤보박스에 -1 씩 넣어서 연도만들기 (2212, 2211, ...) --//
            comboRequest2.Items.Add(year + month);  
            comboRequest2.Items.Add("2212");
            //---------------------------------//

            if (!comboCode.Items.Contains("3SP"))           comboCompany.Items.Add("3SP");
            if (!comboCode.Items.Contains("Denselight"))    comboCompany.Items.Add("Denselight");
            if (!comboCode.Items.Contains("Lumics"))        comboCompany.Items.Add("Lumics");
            if (!comboCode.Items.Contains("OptoEnergy"))    comboCompany.Items.Add("OptoEnergy");
            if (!comboCode.Items.Contains("NKT"))           comboCompany.Items.Add("NKT");
            if (!comboCode.Items.Contains("SHM"))           comboCompany.Items.Add("SHM");
            if (!comboCode.Items.Contains("Superlum"))      comboCompany.Items.Add("Superlum");
            if (!comboCode.Items.Contains("Hulaser"))       comboCompany.Items.Add("Hulaser");


            if (comboRequest1.Items.Count > 0) comboRequest1.SelectedIndex = 0;
            if (comboRequest2.Items.Count > 0) comboRequest2.SelectedIndex = 0;
            if (comboRequest3.Items.Count > 0) comboRequest3.SelectedIndex = 0;
            //if (comboCompany.Items.Count > 0) comboCompany.SelectedIndex = 0;
            //if (comboCode.Items.Count > 0) comboCode.SelectedIndex = 0;
            //if (comboSerial.Items.Count > 0) comboSerial.SelectedIndex = 0;


        }





        // * immediate search
        private void comboRequest3_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgSearch_Click(sender, e);
        }


        // search
        private void imgSearch_Click(object sender, EventArgs e)
        {

            //--- 시리얼 Key로 잡아서 그리드뷰 중복방지 할 것 ---//


            // 그리드뷰에 값이 존재하는 경우, 추가할 것인지 초기화 할 것인지 회신 요청
            if (dataGridView1.Rows.Count != 0) {

                if (MessageBox.Show("데이터가 이미 존재합니다. 추가하시겠습니까?" +
                    "\n(Y: 이어서 추가, N: 비우기)", "데이터 추가", MessageBoxButtons.YesNoCancel) == DialogResult.No)
                {
                    dataGridView1.Rows.Clear();
                }
            }

            //--- 검색 조건 리턴해주는 함수 빌드하기 ---//

            String Request;

            if (String.IsNullOrEmpty(comboRequest2.Text))
            {
                Request = comboRequest1.Text + "-";
            }
            else if (String.IsNullOrEmpty(comboRequest3.Text))
            {
                Request = comboRequest1.Text + "-" + comboRequest2.Text + "-";
            }

            else
                Request = comboRequest1.Text + "-" + comboRequest2.Text + "-" + comboRequest3.Text;

            //-------------------------------------------------------------------------------------//


            MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123");

            connection.Open();

            //string sql = "SELECT REQUEST, CODE, PKG_SERIAL, CHIP_CO, CHIP_ID FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE 'A08%'";
            string sql = " SELECT * FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + Request + "%' ORDER BY REQUEST ";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            


            while (reader.Read())
            {
                // lstTotal.Items.Add(reader["REQUEST"] + " " + reader["CODE"] + " " + reader["PKG_SERIAL"] + " " + reader["CHIP_CO"] + " " + reader["CHIP_ID"]);


                // lstTotal.Items.Add(          // ListBox
                dataGridView1.Rows.Add(         // DataGrid

                    reader["REQUEST"] + " " + reader["CODE"] + " " + reader["PKG_SERIAL"] + " " + reader["CHIP_CO"] + " " + reader["CHIP_ID"]
                    + " " + reader["PB_TIME"] + " " + reader["PB_TEMP"] + " " + reader["CHP_DIFF"] + " " + reader["CHP_ITH"]
                    + " " + reader["CHP_WC"] + " " + reader["CHP_PLN"] + " " + reader["CHP_ILN"] + " " + reader["CHP_IOP"]
                    + " " + reader["CHP_LOT"] + " " + reader["CHP_LOT(O)"] + " " + reader["OSA_SUB_MDL"] + " " + reader["OSA_SUB_LOT"]
                    + " " + reader["OSA_PD_CO"] + " " + reader["OSA_PD_MDL"] + " " + reader["OSA_PD_LOT"] + " " + reader["OSA_THR_CO"]
                    + " " + reader["OSA_THR_MDL"] + " " + reader["OSA_THR_LOT"] + " " + reader["OSA_UNIT"]
                    );

               // dataGridView1.Columns.Add(reader["PB_TIME"]);
            }




            // rows index setting
            dataGridView1.RowHeadersWidth = 60;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }


            // rows count
            lblCount.Text = "총 " + dataGridView1.Rows.Count.ToString() + "개";


            reader.Close();
            connection.Close();
        }



        // Selected Rows Count
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            lblSelected.Text = dataGridView1.SelectedCells.Count.ToString() + "개 선택됨";
        }



        // Read table for "A**-****-nnn"
        private void comboRequest3_DropDown(object sender, EventArgs e)
        {
            String Request;
            Request = comboRequest1.Text + "-" + comboRequest2.Text + "-";

            MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123");

            connection.Open();


            string sql = "SELECT DISTINCT RIGHT(REQUEST,3) FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + Request + "%' ORDER BY REQUEST";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                if (!comboRequest3.Items.Contains(reader["RIGHT(REQUEST,3)"]))
                    comboRequest3.Items.Add(reader["RIGHT(REQUEST,3)"]);
            }


            reader.Close();
            connection.Close();
        }


        // comboCode
        private void comboCode_DropDown(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123");

            connection.Open();

            
            string sql = "SELECT CODE FROM `eunbi`.`PROGRESS` WHERE UCASE(CODE) LIKE 'PM%' GROUP BY CODE ORDER BY CODE";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                if (!comboCode.Items.Contains(reader["CODE"]) )
                comboCode.Items.Add( reader["CODE"] );
            }


            reader.Close();
            connection.Close();
        }



        // DropDown Events

        private void comboRequest2_DropDownClosed(object sender, EventArgs e)
        {
            comboRequest3.Items.Clear();
        }
        private void comboRequest2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboRequest3.Text = "";
        }

        private void comboCompany_DropDownClosed(object sender, EventArgs e)
        {
            comboCompany.Items.Clear();
        }

        private void comboCode_DropDownClosed(object sender, EventArgs e)
        {
            comboCode.Items.Clear();
        }



        ////reset
        private void imgReset_Click(object sender, EventArgs e)
        {
        //    comboRequest3.Items.Clear();
        //    comboCompany.Items.Clear();
        //    comboCode.Items.Clear();
        //    textSerial.Text = null;
        //    textChipid.Text = null;
        //    textDetail.Text = null;

        //    string str =
        //    //If textDetail.Text.IsNullorEmpty() 

        }


    }
}
