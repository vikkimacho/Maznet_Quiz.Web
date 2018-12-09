using Excel;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Quiz.Web.UI.Helper
{
    public static class Common
    {        

        public static DataTable ConvertFileToDateTable(HttpPostedFileBase hpf,string FilePathSource)
        {

            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(hpf.FileName))
            {
                //string FilePath = System.Configuration.ConfigurationManager.AppSettings["UnitUploadFilePath"].ToString() + hpf.FileName;
                string FilePath = System.Web.Hosting.HostingEnvironment.MapPath(System.Configuration.ConfigurationManager.AppSettings[FilePathSource].ToString()) + hpf.FileName;
                int count = 1;
                string newFullPath = FilePath;
                while (System.IO.File.Exists(newFullPath))
                {
                    string tempFileName = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FilePath), count++);
                    newFullPath = Path.Combine(Path.GetDirectoryName(FilePath), tempFileName + Path.GetExtension(FilePath));
                }
                hpf.SaveAs(newFullPath);
                FileStream stream = new FileStream(newFullPath, FileMode.Open, FileAccess.ReadWrite);
                hpf.InputStream.CopyTo(stream);

                string fileExtension = Path.GetExtension(hpf.FileName);
                IExcelDataReader excelReader;
                excelReader = fileExtension == ".xlsx" ? ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream);
                stream.Close();
                excelReader.IsFirstRowAsColumnNames = true;

                DataSet ds = excelReader.AsDataSet();
                dt = ds.Tables[0];
                excelReader.Close();
                dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((Convert.ToString(field)).Trim(), string.Empty) == 0)).CopyToDataTable();
                //dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                foreach (DataColumn column in dt.Columns)
                    column.ColumnName = ti.ToTitleCase(column.ColumnName);
            }
            return dt;
        }
    }
}