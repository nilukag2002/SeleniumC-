using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unirest3.Utility;

namespace Unirest3.Utility
{
    public class JSonConverter
    {
        ReadExcel filePath = new ReadExcel();

        public JObject JSonConvertCreate() 
        {
            Excel readExcelReader = new Excel();
            
            string jsonStrCreate = @"{" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 3, 1, 2) + '"' + ":" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 3, 2, 2) + '"'  + "," + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 3, 1, 3) + '"' + ":" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 3, 2, 3) + '"' + "}";
            JObject jsonCreate = Newtonsoft.Json.Linq.JObject.Parse(jsonStrCreate);
            return jsonCreate;
        }

        public JObject JSonConvertUpdate()
        {
            Excel readExcelReader = new Excel();
            
            string jsonStrUpdate = @"{" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 5, 1, 2) + '"' + ":" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 5, 2, 2) + '"' + "," + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 5, 1, 3) + '"' + ":" + '"' + readExcelReader.readExcel(filePath.filePathToExcel(), 5, 2, 3) + '"' + "}";
            JObject jsonUpdate = Newtonsoft.Json.Linq.JObject.Parse(jsonStrUpdate);
            return jsonUpdate;
        }
    }
}
