using System.IO;
using UnityEditor;
using UnityEngine;

namespace DataRecording
{
    public static class CsvManager
    {
        private static string reportDirectoryName = "Report";
        private static string reportFileName = "report.csv";
        private static string reportSeparator = ",";

        private static string[] reportHeaders = new string[18]
        {
            "Head X position",
            "Head Y position",
            "Head Z position",
            "Head X rotation",
            "Head Y rotation",
            "Head Z rotation",
            "Lhand X position",
            "Lhand Y position",
            "Lhand Z position",
            "Lhand X rotation",
            "Lhand Y rotation",
            "Lhand Z rotation",
            "Rhand X position",
            "Rhand Y position",
            "Rhand Z position",
            "Rhand X rotation",
            "Rhand Y rotation",
            "Rhand Z rotation",
        };
        private static string timeStampHeader = "time stamp";

        #region Interactions

        public static void AppendToReport(string[] strings)
        {
            VerifyDirectory();
            VerifyFile();
            using (StreamWriter sw = File.AppendText(GetFilePath()))
            {
                string finalString = "";
                for (int i = 0; i < strings.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }
                    finalString += strings[i];
                }

                finalString += reportSeparator + GetTimeStamp();
                sw.WriteLine(finalString);
            }
        }

        public static void CreateReport()
        {
            VerifyDirectory();
            using (StreamWriter sw = File.CreateText(GetFilePath()))
            {
                string finalString = "";
                for (int i = 0; i < reportHeaders.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }

                    finalString += reportHeaders[i];
                }

                finalString += reportSeparator + timeStampHeader;
                sw.WriteLine(finalString);
            }
        }
        
        #endregion
        
        #region Operations

        static void VerifyDirectory()
        {
            string dir = GetDirectoryPath();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        static void VerifyFile()
        {
            string file = GetFilePath();
            if (!File.Exists(file))
            {
                CreateReport();
            }
        }
        
        #endregion

        #region Queries

        static string GetDirectoryPath()
        {
            return Application.dataPath + "/" + reportDirectoryName;
        }

        static string GetFilePath()
        {
            return GetDirectoryPath() + "/" + reportFileName;
        }

        static string GetTimeStamp()
        {
            return System.DateTime.UtcNow.ToString();
        }
        
        #endregion
    }
    
    
}
