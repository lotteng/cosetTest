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
using static System.Net.Mime.MediaTypeNames;

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
           
            //MessageBox.Show(id);  메시지창 띄움

            string sql = "INSERT INTO product (num, productID, productName) VALUES (NULL,'"
                         + id
                         + " ',' "
                         + pw
                         + " ') "; //Why 숫자만 전송? => *작은따옴표 추가 안해서*
            
            //string sql = "INSERT INTO product (num, productID, productName) VALUES (NULL, '{0}', '{1}' );", id, pw); 

            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

        }




    }





}
