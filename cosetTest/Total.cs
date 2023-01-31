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

namespace cosetTest
{
    public partial class Total : Form
    {
        public Total()
        {
            InitializeComponent();
        }


        // default
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
            if (comboCompany.Items.Count > 0) comboCompany.SelectedIndex = 0;
            if (comboCode.Items.Count > 0) comboCode.SelectedIndex = 0;
            if (comboSerial.Items.Count > 0) comboSerial.SelectedIndex = 0;

            


        }


        // search
        private void btnSearch_Click(object sender, EventArgs e)
        {

            //--- 검색 조건 리턴해주는 함수 빌드하기 ---//

            String Request;
            Request = comboRequest1.Text + "-" + comboRequest2.Text + "-" + comboRequest3.Text ;
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

                lstTotal.Items.Add(
                    reader["REQUEST"] + " " + reader["CODE"] + " " + reader["PKG_SERIAL"] + " " + reader["CHIP_CO"] + " " + reader["CHIP_ID"]
                    + " " + reader["PB_TIME"] + " " + reader["PB_TEMP"] + " " + reader["CHP_DIFF"] + " " + reader["CHP_ITH"]
                    + " " + reader["CHP_WC"] + " " + reader["CHP_PLN"] + " " + reader["CHP_ILN"] + " " + reader["CHP_IOP"]
                    + " " + reader["CHP_LOT"] + " " + reader["CHP_LOT(O)"] + " " + reader["OSA_SUB_MDL"] + " " + reader["OSA_SUB_LOT"]
                    + " " + reader["OSA_PD_CO"] + " " + reader["OSA_PD_MDL"] + " " + reader["OSA_PD_LOT"] + " " + reader["OSA_THR_CO"]
                    + " " + reader["OSA_THR_MDL"] + " " + reader["OSA_THR_LOT"] + " " + reader["OSA_UNIT"]
                    );

            }


            reader.Close();
            connection.Close();
        }



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


        // code
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


        //// company
        //private void comboCompany_DropDown(object sender, EventArgs e)
        //{

        //    MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123");

        //    connection.Open();


        //    string sql = "SELECT DISTINCT CHIP_CO FROM `eunbi`.`PROGRESS` ORDER BY CHIP_CO";
        //    MySqlCommand cmd = new MySqlCommand(sql, connection);
        //    MySqlDataReader reader = cmd.ExecuteReader();



        //    while (reader.Read())
        //    {
        //        comboCompany.Items.Add(reader["CHIP_CO"]);
        //    }


        //    reader.Close();
        //    connection.Close();

        //}





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






        // exit
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
