using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.IO;

namespace Quiz.Web.UI.Helper
{
    public class Logger
    {
        public void WriteToLogFile(string logMessage)
        {
            try
            {
                string strLogMessage = string.Empty;
                string strLogFile = ConfigurationSettings.AppSettings["logFilePath"];
                StreamWriter swLog;

                strLogMessage = string.Format("{0}: {1}", DateTime.Now, logMessage);

                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();

                swLog.Close();

            }
            catch
            {

            }
        }
    }
}