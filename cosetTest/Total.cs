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
using Excel = Microsoft.Office.Interop.Excel; // 'Excel =' 추가한 이유? 모호한 참조 오류 발생(System에 선언되어 있는 클래스나 메소드 이름 같음)
using Google.Protobuf.WellKnownTypes;
using System.Runtime.CompilerServices;

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

            comboRequest1.Items.Add("A08");


            int year, month;

            DateTime thisDay = DateTime.Today;
            year = Convert.ToInt32(thisDay.ToString("yy"));
            month = Convert.ToInt32(thisDay.ToString("MM"));

 
            for (int i = 0; i < 24; i++)    // Add 24 month
            {

                if (month < 10)
                {
                    comboRequest2.Items.Add(year.ToString() + "0" + month.ToString());
                    
                }

                else
                {
                    comboRequest2.Items.Add(year.ToString() + month.ToString());
                }

                month = month - 1;

                if (month == 0)
                {
                    year = year - 1;
                    month = 12;
                }


            }
            
            

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

            if ((comboRequest1.Text == null) || (comboRequest2.Text == null))
            {
                MessageBox.Show("생산요구서를 선택해주세요.");

                return;

            }


            try
            {


                Cursor.Current = Cursors.WaitCursor;

                

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


                MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123; Allow Zero Datetime=True");

                MySqlDataAdapter mySqlDataAdapter;

                connection.Open();

                string sql = " SELECT * FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + Request + "%' ORDER BY REQUEST ";




                // Grid View
                mySqlDataAdapter = new MySqlDataAdapter(sql, connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];


                /* list View

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

                reader.Close();

                */


                // rows index setting
                dataGridView1.RowHeadersWidth = 60;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }


                // rows count
                lblCount.Text = "총 " + dataGridView1.Rows.Count.ToString() + "개";


                // Auto Insert Combo_Text
                comboCode_DropDown(sender, e);
                comboCode.SelectedItem = dataGridView1.Rows[0].Cells[1].FormattedValue.ToString();
                comboCompany.SelectedItem = dataGridView1.Rows[0].Cells[3].FormattedValue.ToString(); // ★ 3SP -> 3SPT 로 통일시켜야함 ★
                                                                                                      // 현재 콤보값은 3SP로 되어있어서 타사 선택 후 3SPT를 누르면 변경이 안됨
                connection.Close();
                
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("해당 생산요구서가 존재하지 않습니다.");
            }

            //catch (Exception e)
            //{

            //}


            finally
            {

                Cursor.Current = Cursors.Default;

            }
        }






    // Selected Rows Count
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblSelected.Text = dataGridView1.SelectedCells.Count.ToString() + "개 선택됨";
        }




        // Read table for Request : A**-****-'nnn'
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

        //private void comboCompany_DropDownClosed(object sender, EventArgs e)
        //{
        //    comboCompany.Items.Clear();
        //}

        //private void comboCode_DropDownClosed(object sender, EventArgs e)
        //{
        //    comboCode.Items.Clear();
        //}



    // reset
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


        // export excel -------> 참조: Microsoft.Office.Interop.Excel (없으면 .dll 다운)
        private void lblExcel_Click(object sender, EventArgs e)
        {
            imgExcel_Click(sender, e);
        }

        private void imgExcel_Click(object sender, EventArgs e)
        {


            if ( dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("데이터를 선택해주세요.");
                return;
            }


            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("엑셀이 설치되지 않았습니다.");
                return;
            }


            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Export Excel";
            sfd.Filter = "Excel Files(2007)|*.xlsx|Excel Files(2003)|*.xls";
            sfd.FileName = dataGridView1.Rows[0].Cells[0].FormattedValue.ToString();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                // copy
                dataGridView1.SelectAll();
                DataObject dataObj = dataGridView1.GetClipboardContent();
                if (dataObj != null) Clipboard.SetDataObject(dataObj);


                object misValue = System.Reflection.Missing.Value;

                Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); // get_Item(i) = n번째 시트;


                // paste in excel

                //xlApp.Visible = true;         // 실시간 엑셀파일 보기 끔. 저장 중인 엑셀파일을 바로 보려면 주석 풀기.
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];  // 컬럼은 제외!
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, true);
                xlWorkBook.SaveAs(sfd.FileName, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                    System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);

                xlApp.Quit();

                dataGridView1.ClearSelection();

                Cursor.Current = Cursors.Default;
            }


        }



    }
}
