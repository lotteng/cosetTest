using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cosetTest
{
    internal class Search
    {

        MariaDB mariaDB = new MariaDB();

        string SearchRequestWord;


        public Search()
        {

        }


        /// <summary>
        /// Request Word for Search Method | comboRequest1, 2, 3 사용
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="Third"></param>
        /// <returns></returns>

 

        public string GetSearchRequestWord(ComboBox First, ComboBox Second, ComboBox Third)
        {

            if (String.IsNullOrEmpty(Second.Text))
            {
                SearchRequestWord = First.Text;
            }
            else if (String.IsNullOrEmpty(Third.Text))
            {
                SearchRequestWord = First.Text + "-" + Second.Text;
            }
            else
                SearchRequestWord = First.Text + "-" + Second.Text + "-" + Third.Text;

            return SearchRequestWord;
        }


        public string SearchRequest(ComboBox First, ComboBox Second, ComboBox Third)
        {
            if ((First.Text == null) || (Second.Text == null))
            {
                MessageBox.Show("생산요구서를 선택해주세요.");

                return null;

            }

            SearchRequestWord = GetSearchRequestWord(First, Second, Third);


            string query = " SELECT * FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + SearchRequestWord + "%' ORDER BY REQUEST ";

            return query;



            // 아래 코드들을 위 함수로 대체함 ( * 생산요구서+@ 다중 검색되도록 구현할 것)

            //if ((First.Text == null) || (Second.Text == null))
            //{
            //    MessageBox.Show("생산요구서를 선택해주세요.");

            //    return null;

            //}

            //SearchRequestWord = request.GetSearchRequest(comboRequest1, comboRequest2, comboRequest3);

            //// 만약, 다른 combobox가 비어있지 않다면, SearchWord = " AND " + "%" + combo___.Text + "%" 를 추가해준다. => AND %comboCompany.Text% (= AND %3SPT%)
            //// 다중 조건 구현은 SearchWord 뒤에 문자열을 연결하여 추가해주자.

            //if (String.IsNullOrEmpty(SearchWord))
            //{
            //    query = " SELECT * FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + SearchRequestWord + "%'" + SearchWord + " ORDER BY REQUEST ";

            //}
            //else
            //{
            //    query = " SELECT * FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '" + SearchRequestWord + "%' ORDER BY REQUEST ";
            //}

        }




        // Set ComboBox (Load Datas and into ComboBox for Search)
        // 1) Share
        // 2) Request1, Request2, Request3
        // 3) *Company (* 3SPT로 통일해서 Common으로 사용할 것)


        /// <summary>
        /// 초기 콤보박스 설정(공용)
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnName"></param>
        // Share 
        public void SetSearchCombo(ComboBox comboBox, String columnName) // Event DropDown
        {

            string query = "SELECT " + columnName
                            + " FROM `eunbi`.`PROGRESS` WHERE UCASE("
                            + columnName
                            + ") LIKE 'PM%' GROUP BY " + columnName
                            + " ORDER BY " + columnName;

            MySqlDataReader reader = mariaDB.GetReader(query);

            while (reader.Read())
            {
                if (!comboBox.Items.Contains(reader[columnName]))
                    comboBox.Items.Add(reader[columnName]);
            }

            reader.Close();
            mariaDB.GetConnection().Close();
        }


        // Request1 ( When? : Form_Initialize )
        public void SetRequestComboFirst(ComboBox requestCombo)
        {
            requestCombo.Items.Add("A08");

            if (requestCombo.Items.Count > 0) requestCombo.SelectedIndex = 0;

        }


        // Request2 ( When? : Form_Initialize )
        public void SetRequestComboSecond(ComboBox requestCombo)
        {

            DateTime today = DateTime.Today;
            int year = Convert.ToInt32(today.ToString("yy"));
            int month = Convert.ToInt32(today.ToString("MM"));

            const int MAX_REQUEST_MONTH = 24;

            for (int i = 0; i < MAX_REQUEST_MONTH; i++)
            {

                if (month < 10)
                {
                    requestCombo.Items.Add(year.ToString() + "0" + month.ToString());
                }

                else
                {
                    requestCombo.Items.Add(year.ToString() + month.ToString());
                }

                month = month - 1;

                if (month == 0)
                {
                    year = year - 1;
                    month = 12;
                }
            }

            if (requestCombo.Items.Count > 0) requestCombo.SelectedIndex = 0;
        }


        // Request3 ( When? : comboRequest3_DropDown)
        public void SetRequestComboThird(ComboBox requestComboFirst, ComboBox requestComboSecond, ComboBox requestComboThird) 
        {

            string query = "SELECT DISTINCT RIGHT(REQUEST,3) FROM `eunbi`.`PROGRESS` WHERE REQUEST LIKE '"
                            + GetSearchRequestWord(requestComboFirst, requestComboSecond, requestComboThird)
                            + "%' ORDER BY REQUEST";

            MySqlDataReader reader = mariaDB.GetReader(query);

            while (reader.Read())
            {
                if (!requestComboThird.Items.Contains(reader["RIGHT(REQUEST,3)"]))
                    requestComboThird.Items.Add(reader["RIGHT(REQUEST,3)"]);
            }


            reader.Close();
            mariaDB.GetConnection().Close();
        }



        // Company
        public void SetCompanyCombo(ComboBox comboCompany)
        {
            if (!comboCompany.Items.Contains("3SP"))        comboCompany.Items.Add("3SP");      // DB데이터 3SPT로 변경해야 함
            if (!comboCompany.Items.Contains("Denselight")) comboCompany.Items.Add("Denselight");
            if (!comboCompany.Items.Contains("Lumics"))     comboCompany.Items.Add("Lumics");
            if (!comboCompany.Items.Contains("OptoEnergy")) comboCompany.Items.Add("OptoEnergy");
            if (!comboCompany.Items.Contains("NKT"))        comboCompany.Items.Add("NKT");
            if (!comboCompany.Items.Contains("SHM"))        comboCompany.Items.Add("SHM");
            if (!comboCompany.Items.Contains("Superlum"))   comboCompany.Items.Add("Superlum");
            if (!comboCompany.Items.Contains("Hulaser"))    comboCompany.Items.Add("Hulaser");
        }

    }






}

