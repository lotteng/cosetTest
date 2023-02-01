using System;
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
        // drag & drop
        private Point point = new Point();

        // Place holder
        TextBox[] txtList;

        const string IdPlaceholder = "아이디";
        const string PwPlaceholder = "비밀번호";



        public Login()
        {
            InitializeComponent();

            //ID, Password TextBox Placeholder 설정
            txtList = new TextBox[] { txtId, txtPw };
            foreach (var txt in txtList)
            {
                //처음 공백 Placeholder 지정
                txt.ForeColor = Color.DarkGray;
                if (txt == txtId) txt.Text = IdPlaceholder;
                else if (txt == txtPw) txt.Text = PwPlaceholder;
                //텍스트박스 커서 Focus 여부에 따라 이벤트 지정
                txt.GotFocus += RemovePlaceholder;
                txt.LostFocus += SetPlaceholder;
            }
        }


    // Place holder
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == IdPlaceholder | txt.Text == PwPlaceholder)
            { //텍스트박스 내용이 사용자가 입력한 값이 아닌 Placeholder일 경우에만, 커서 포커스일때 빈칸으로 만들기
                txt.ForeColor = Color.Black; //사용자 입력 진한 글씨
                txt.Text = string.Empty;
                if (txt == txtPw) txtPw.PasswordChar = '●'; // char형이기 때문에 꼭 어포스트로피(')로 사용해야 한다. " " (X)
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                //사용자 입력값이 하나도 없는 경우에 포커스 잃으면 Placeholder 적용해주기
                txt.ForeColor = Color.DarkGray; //Placeholder 흐린 글씨
                if (txt == txtId) txt.Text = IdPlaceholder;
                else if (txt == txtPw) { txt.Text = PwPlaceholder; txtPw.PasswordChar = default; }
            }
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



    // Enter key
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }




        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123");

                connection.Open(); // SQL 서버 연결

                int login_status = 0;   //로그인 = 1, 비로그인 = 0

                string id = txtId.Text;
                string pw = txtPw.Text;
                string selectQuery = "SELECT * FROM USER WHERE ID_PK = \'" + id + "\' "; // MySQL에 전송할 명령어 입력. (USER 테이블 값 읽기)

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                // MySqlCommand는 MySQL로 명령어를 전송하기 위한 클래스임.
                // MySQL에 selectQuery 값을 보내고, connection 값을 보내 연결을 시도한다.
                // 위 정보를 Selectcommand 변수에 저장한다.

                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                // MySqlDataReader = 입력값을 받기 위함.
                // Selectcommand 변수에 ExecuteReader() 객체를 통해 입력값을 받고, 해당 정보를 userAccount 변수에 저장함.


                while (userAccount.Read()) // userAccount가 Read 되고 있을 동안
                {
                    if ( id == (string)userAccount["ID_PK"] && pw == (string)userAccount["PW"]) // id, pw 가 USER 테이블의 필드명 정보와 일치한다면
                    {
                        login_status = 1;
                    }
                }

                connection.Close(); // MySQL과 연결 끊음.

                if (login_status == 1)
                {
                    //MessageBox.Show("환영합니다");


                    this.Hide();
                    
                    Main FormMain = new Main();
                    FormMain.Show();

                    //this.Close();
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

    // Find ID, PW
        private void lblFind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void imgExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void imgMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    // checkbox image change
        private void checkAutologin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAutologin.Checked)
            {
                checkAutologin.ImageIndex = 1;
                checkAutologin.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                checkAutologin.ImageIndex = 0;
            }
        }

        private void checkBoxID_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxID.Checked)
            {
                checkBoxID.ImageIndex = 1;
                checkBoxID.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                checkBoxID.ImageIndex = 0;
            }
        }


    // Link Label Underline setting 
        private void Login_Load(object sender, EventArgs e)
        {
            lblFind.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        }

        private void lblFind_MouseMove(object sender, MouseEventArgs e)
        {
            lblFind.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        }
    }
}
