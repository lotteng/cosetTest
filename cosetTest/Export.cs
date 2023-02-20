using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; // 'Excel =' 추가한 이유? 모호한 참조 오류 방지

namespace cosetTest
{
    internal class Export
    {
        public Export() { 

        }

        public void Excel(DataGridView dataGridView) // -------> 참조: Microsoft.Office.Interop.Excel(없으면.dll 다운)
        {

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("please select data first");
                return;
            }


            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Export was failed. might be not install EXCEL", "Check EXCEL install");
                return;
            }


            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Export Excel";
            sfd.Filter = "Excel Files(2007)|*.xlsx|Excel Files(2003)|*.xls";
            sfd.FileName = dataGridView.Rows[0].Cells[0].FormattedValue.ToString();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                // copy
                dataGridView.SelectAll();
                DataObject dataObj = dataGridView.GetClipboardContent();
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

                dataGridView.ClearSelection();

                Cursor.Current = Cursors.Default;
            }
        }

    }
}
