using System;
using System.Collections.Generic;
using System.IO;

namespace atm_uygulamasi
{
    public class FileLogger
    {
        private string CreateFilePath()
        {
            return $@"d:/EOD_{DateTime.Now.ToString("ddMMyyyy")}.txt";
        }
        public void WriteLog(LogType logType , string logMessage)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(CreateFilePath(), true);
                streamWriter.WriteLine($"{logType} - {logMessage}");
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dosyaya log yazma işleminde hata oluştu.");
                Console.WriteLine("Hata mesajı : " + ex.Message);
            }

            
        }
        public void ReadLog()
        {
            try
            {
                StreamReader streamReader = new StreamReader(CreateFilePath());

                string line;

                List<string> infoLogs = new List<string>();
                List<string> errorLogs = new List<string>();

                while ((line = streamReader.ReadLine()) != null)
                {
                    if(line.Contains(LogType.Info.ToString()))
                    {
                        infoLogs.Add(line);
                    }
                    else if (line.Contains(LogType.Error.ToString()))
                    {
                        errorLogs.Add(line);
                    }
                }


                Console.WriteLine("***** Loglar *****");
                foreach (string logMessage in infoLogs)
                {
                    Console.WriteLine(logMessage);
                }


                Console.WriteLine("\n***** Hata Logları *****");
                if (errorLogs.Count == 0)
                {
                    Console.WriteLine("Hatalı işlem yok.");
                }
                else
                {
                    foreach (string logMessage in errorLogs)
                    {
                        Console.WriteLine(logMessage);
                    }
                }

                
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dosyadan log okuma işleminde hata oluştu.");
                Console.WriteLine("Hata mesajı : " + ex.Message);
            }

            
        }
    }
}