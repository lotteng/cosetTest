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
using Google.Protobuf.WellKnownTypes;
using System.Runtime.CompilerServices;
using System.CodeDom;

namespace cosetTest
{
    public partial class Total : Form
    {   
        // drag & drop
        private Point point = new Point();

        // Global instance
        MariaDB mariaDB = new MariaDB();
        Export export = new Export();
        Search search = new Search();


        string query;


        public Total()
        {
            InitializeComponent();

            search.SetRequestComboFirst(comboRequest1);
            search.SetRequestComboSecond(comboRequest2);
            search.SetCompanyCombo(comboCompany);

        }


        // * immediate search
        private void comboRequest3_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgSearch_Click(sender, e);

        }


    // search
        private void imgSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                query = search.SearchRequest(comboRequest1, comboRequest2, comboRequest3);

                dataGridView1.DataSource = mariaDB.GetAdapter(query).Tables[0];



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
                mariaDB.GetConnection().Close();
                
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
            search.SetRequestComboThird(comboRequest1, comboRequest2, comboRequest3);
        }


        // comboCode
        private void comboCode_DropDown(object sender, EventArgs e)
        {
            search.SetSearchCombo(comboCode, "CODE");

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





        // export excel
        private void lblExcel_Click(object sender, EventArgs e)
        {
            export.Excel(dataGridView1);
        }

        private void imgExcel_Click(object sender, EventArgs e)
        {
            export.Excel(dataGridView1);

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
    }
}
