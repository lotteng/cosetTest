using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace cosetTest
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            MariaDbLib connect = new MariaDbLib();

            if (connect.ConnectionTest() == true)
            {
                lblResult.Text = "Connect Successed";

                connect.InsertDB(txtId.Text, txtPw.Text);

            }

            else lblResult.Text = "Connect Failed";

        }

    }



    public class MariaDbLib
    {
        public bool ConnectionTest()
        {
            string connectString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "192.168.10.240", "sampledb", "root", "coset!!123");


            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //INSERT

        public void InsertDB(string id, string pw)
        {
            string connectString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "192.168.10.240", "sampledb", "root", "coset!!123");

            string sql = "Insert Into product (num, productID, productName) values (NULL," + (string)id + "," + (string)pw + ")";

            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

        }




    }





}
