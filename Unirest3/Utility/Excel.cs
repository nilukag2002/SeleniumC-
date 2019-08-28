using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelSheet = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Unirest3.Utility
{
    class Excel
    {

        public String readExcel(string filePath, int worksheetIndex, int column, int row)
        {



            ExcelSheet.Application xlApplication = new ExcelSheet.Application();

            ExcelSheet.Workbook xlWorkbook = xlApplication.Workbooks.Open(@filePath);
            ExcelSheet._Worksheet xlWorksheet = (ExcelSheet._Worksheet)xlWorkbook.Sheets[worksheetIndex];
            ExcelSheet.Range xlRange = xlWorksheet.UsedRange;
            Console.Write(xlRange.Cells[row, column].Value2.ToString());
            String value = xlRange.Cells[row, column].Value2.ToString();
            xlWorkbook.Close();
            xlApplication.Quit();
            return value;

        }
    }
}
