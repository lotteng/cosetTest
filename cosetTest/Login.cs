﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient; // SQL 연동

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
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = sampledb; Uid = root ; Pwd = coset!!123");

                connection.Open(); // SQL 서버 연결

                int login_status = 0;   //로그인 = 1, 비로그인 = 0

                string id = txtId.Text;
                string pw = txtPw.Text;
                string selectQuery = "SELECT * FROM USER WHERE  = \'" + id + "\' "; // MySQL에 전송할 명령어 입력. (USER 테이블 값 읽기)

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                // MySqlCommand는 MySQL로 명령어를 전송하기 위한 클래스임.
                // MySQL에 selectQuery 값을 보내고, connection 값을 보내 연결을 시도한다.
                // 위 정보를 Selectcommand 변수에 저장한다.

                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                // MySqlDataReader = 입력값을 받기 위함.
                // Selectcommand 변수에 ExecuteReader() 객체를 통해 입력값을 받고, 해당 정보를 userAccount 변수에 저장함.


                while (userAccount.Read()) // userAccount가 Read 되고 있을 동안
                {
                    if (id == (string)userAccount["ID_PK"] && pw == (string)userAccount["PW"]) // id, pw 가 USER 테이블의 필드명 정보와 일치한다면
                    {
                        login_status = 1;
                    }
                }

                connection.Close(); // MySQL과 연결 끊음.

                if (login_status == 1)
                {
                    MessageBox.Show("환영합니다");

                }

                else
                {
                    MessageBox.Show("회원 정보를 확인해주세요.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // 예외값 발생 시 해당 정보와 관련된 정보를 띄운다.
            }



        }
    }
}
