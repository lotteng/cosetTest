using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
        // drag & drop
        private Point point = new Point();



        // Global instance
        Main main = null;            //public Main FormMain = new Main();
        MariaDB mariaDB = new MariaDB();
        Search search = new Search();
        Work work = new Work();

        string query;



        public Side(Main FormMain)
        {
            InitializeComponent();


            this.main = FormMain;

            search.SetRequestComboFirst(comboRequest1);
            search.SetRequestComboSecond(comboRequest2);

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

                Cursor.Current = Cursors.WaitCursor;


                Work work = new Work();
                work.FormBorderStyle = FormBorderStyle.None;
                work.TopLevel = false;
                work.Show();
                main.panelMain.Controls.Add(work);
                work.Dock = DockStyle.Fill;


                work.panelTop.Visible = true;
                work.panelSerial.Visible = true;
                work.panelBoard.Visible = true;
                work.panelFinal.Visible = true;

                //FormMain.panelTop.Visible = true;
                //FormMain.panelSerial.Visible = true;
                //FormMain.panelBoard.Visible = true;
                //FormMain.panelFinal.Visible = true;


                query = search.SearchRequest(comboRequest1, comboRequest2, comboRequest3);


                work.dataGridViewSerial.DataSource = mariaDB.GetAdapter(query).Tables[0];    //dataGridView1.DataSource = DS.Tables[0];


                // rows index setting

                for (int i = 0; i < work.dataGridViewSerial.Rows.Count; i++)
                {
                    work.dataGridViewSerial.Rows[i].Cells[1].Value = i+1 ;

                }

                // rows count
                work.lblColor0.Text = work.dataGridViewSerial.Rows.Count.ToString();


                work.dataGridViewSerial.Columns[0].ReadOnly = false; // user have to be checking serial
                work.dataGridViewSerial.Columns[1].ReadOnly = true;
                work.dataGridViewSerial.Columns[2].ReadOnly = true;


                mariaDB.GetConnection().Close();

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
            search.SetRequestComboThird(comboRequest1, comboRequest2, comboRequest3);
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
