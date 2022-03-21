using System.IO;
using UnityEditor;
using UnityEngine;

namespace DataRecording
{
    public static class CsvManager
    {
        private static string reportDirectoryName = "Report";
        private static string reportFileName = GetDayStamp()+".csv";
        private static string reportSeparator = ",";
        private static string timeStampHeader = "time stamp";
        private static string[] reportHeaders = new string[36]
        {
            "Head X position",  //float or double
            "Head Y position",  //float or double
            "Head Z position",  //float or double
            "Head X rotation",  //float or double
            "Head Y rotation",  //float or double
            "Head Z rotation",  //float or double
            "Lhand X position",  //float or double
            "Lhand Y position",  //float or double
            "Lhand Z position",  //float or double
            "Lhand X rotation",  //float or double
            "Lhand Y rotation",  //float or double
            "Lhand Z rotation",  //float or double
            "Rhand X position",  //float or double
            "Rhand Y position",  //float or double
            "Rhand Z position",  //float or double
            "Rhand X rotation",  //float or double
            "Rhand Y rotation",  //float or double
            "Rhand Z rotation",  //float or double
            "Angry smiley", //Bool
            "Sad smiley", //Bool
            "Happy smiley", //Bool
            "Drum", //Bool
            "Balafon key 1", //Bool
            "Balafon key 2", //Bool
            "Balafon key 3", //Bool
            "Balafon key 4", //Bool
            "Balafon key 5", //Bool
            "Balafon key 6", //Bool
            "Balafon key 7", //Bool
            "Balafon key 8", //Bool
            "Balafon key 9", //Bool
            "Balafon key 10", //Bool
            "Balafon key 11", //Bool
            "Balafon key 12", //Bool
            "Speed left hand",  //float
            "Speed right hand", //float
        };

        #region Interactions

        public static void AppendToReport(string[] strings)
        {
            Debug.Log(GetDayStamp());
            VerifyDirectory();
            VerifyFile();
            using (StreamWriter sw = File.AppendText(GetFilePath()))
            {
                string finalString = "";
                finalString += reportSeparator + GetTimeStamp();
                for (int i = 0; i < strings.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }
                    finalString += strings[i];
                }
                
                sw.WriteLine(finalString);
            }
        }

        public static void CreateReport()
        {
            VerifyDirectory();
            using (StreamWriter sw = File.CreateText(GetFilePath()))
            {
                string finalString = "";
                finalString += reportSeparator + timeStampHeader;
                for (int i = 0; i < reportHeaders.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }

                    finalString += reportHeaders[i];
                }
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

        static string GetDayStamp()
        {
            System.DateTime theTime = System.DateTime.Now;
            string date = theTime.Year + "-" + theTime.Month + "-" + theTime.Day;
            string time = date + "T" + theTime.Hour + "-" + theTime.Minute + "-" + theTime.Second;
            return time;
        }
        
        #endregion
    }
    
    
}
