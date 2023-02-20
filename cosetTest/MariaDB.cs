using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cosetTest
{
    internal class MariaDB
    {
        public MariaDB() {

        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection("Server = 192.168.10.240 ; Database = eunbi; Uid = root ; Pwd = coset!!123; Allow Zero Datetime=True");

            connection.Open();

            return connection;

            // GetConnection().Close();     < require use ** AVOID OVERHEAD **
        }

        public MySqlDataReader GetReader(string query)
        {

            MySqlCommand command = new MySqlCommand(query, GetConnection());
            // MySqlCommand는 MySQL로 명령어를 전송, MySQL에 query, connection 값을 보내 연결 시도

            MySqlDataReader reader = command.ExecuteReader();

            return reader;

            // reader.Close();              < require use  ** AVOID OVERHEAD **

        }


        public DataSet GetAdapter(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, GetConnection());

            DataSet DS = new DataSet();
            adapter.Fill(DS);

            return DS;
        }
    }
}
