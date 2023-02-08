using MySql.Data.MySqlClient;
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
    public partial class Side : Form
    {



        // Global instance
        Main FormMain = null;            //public Main FormMain = new Main();

        // drag & drop
        private Point point = new Point();



        public Side(Main FormMain)
        {
            InitializeComponent();
            this.FormMain = FormMain;
        }




        private void Side_Load(object sender, EventArgs e)
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

            if (comboRequest1.Items.Count > 0) comboRequest1.SelectedIndex = 0;
            if (comboRequest2.Items.Count > 0) comboRequest2.SelectedIndex = 0;
            if (comboRequest3.Items.Count > 0) comboRequest3.SelectedIndex = 0;

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

        private void imgExit_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }



        // help panel
        private void imgHelp_MouseHover(object sender, EventArgs e)
        {
            panelHelp.Visible = true;
        }

        private void imgHelp_MouseLeave(object sender, EventArgs e)
        {
            panelHelp.Visible = false;
        }



        // After Select, Show the Main Form
        private void comboRequest3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {


                // Form Main.Designer.cs 에서 private -> public 으로 접근한정자 변경하였음 (23/02/08)

                    FormMain.panelTop.Visible = true;
                    FormMain.panelSerial.Visible = true;
                    FormMain.panelBoard.Visible = true;
                    FormMain.panelFinal.Visible = true;


                Cursor.Current = Cursors.WaitCursor;




                String Request = comboRequest1.Text + "-" + comboRequest2.Text + "-" + comboRequest3.Text;


                MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123; Allow Zero Datetime=True");

                MySqlDataAdapter mySqlDataAdapter;

                connection.Open();

                string sql = " SELECT PKG_SERIAL FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + Request + "%' ORDER BY PKG_SERIAL ";



                // Grid View
                mySqlDataAdapter = new MySqlDataAdapter(sql, connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                FormMain.dataGridViewSerial.DataSource = DS.Tables[0];    //dataGridView1.DataSource = DS.Tables[0];


                // rows index setting

                for (int i = 0; i < FormMain.dataGridViewSerial.Rows.Count; i++)
                {
                    FormMain.dataGridViewSerial.Rows[i].Cells[1].Value = i+1 ;

                }

                // rows count
                FormMain.lblColor0.Text = FormMain.dataGridViewSerial.Rows.Count.ToString();


                // Columns ReadOnly
                FormMain.dataGridViewSerial.Columns[0].ReadOnly = false;
                FormMain.dataGridViewSerial.Columns[1].ReadOnly = true;
                FormMain.dataGridViewSerial.Columns[2].ReadOnly = true;


                connection.Close();

            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("해당 생산요구서가 존재하지 않습니다.");
            }

            //catch (Exception e)
            //{

            //}

            catch (NullReferenceException)
            {
                MessageBox.Show("NullReferenceException"); // 글로벌 인스턴스 생성하면 발생 X ? << (23/02/08)
                                                            // "개체 참조가 개체의 인스턴스로 설정되지 않았습니다." <- 참조하려는 객체가 인스턴스(new)가 되지 않은 경우가 많다.
            }


            finally
            {

                Cursor.Current = Cursors.Default;

            }
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


        // DropDown Events

        private void comboRequest2_DropDownClosed(object sender, EventArgs e)
        {
            comboRequest3.Items.Clear();
        }
        private void comboRequest2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboRequest3.Text = "";
        }


    }



}
