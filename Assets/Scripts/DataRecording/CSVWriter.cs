using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


namespace DataRecording
{
    public class CSVWriter
    {
        private static string reportDirectoryName = "Report";
        private static string reportFileName = "Data";
        private static string reportSeparator = ",";
        private static string[] reportHeaders;
        public const int numReportHeaders = 4;

        public CSVWriter()
        {
            reportHeaders = new string[numReportHeaders]
            {
                "Drum hits",
                "Happy smiley time",
                "Sad smiley time",
                "Angry smiley time"
                
            };
            reportFileName = GetDateStamp()+".csv";
        }


        public void AppendToReport(string[] strings)
        {
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
                
                sw.Write(finalString);
            }
                
        }

        public void AppendField(string CSVHeader, string val)
        {
            
        }

        public void CreateReport()
        {
            VerifyDirectory();
            using (StreamWriter sw = File.CreateText(GetFilePath()))
            {
                string finalString = "";
                finalString += reportSeparator + "Time Stamp";
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

        #region Operations

         void VerifyDirectory()
        {
            string directory = GetDirectoryPath();
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

         void VerifyFile()
        {
            string file = GetFilePath();
            if (!File.Exists(file))
            {
                CreateReport();
            } 
        }

        #endregion

        #region Queries

         string GetDirectoryPath()
        {
            return Application.dataPath + "/" + reportDirectoryName;
        }

          string GetFilePath()
        {
            return GetDirectoryPath() + "/" + reportFileName;
        }

         string GetDateStamp()
        {
            System.DateTime theDate = System.DateTime.Now;
            string date = theDate.Year + "-" + theDate.Month + "-" + theDate.Day;
            return date;
        }

          string GetTimeStamp()
        {
            System.DateTime theTime = System.DateTime.Now;
            string time =  theTime.Hour + ":" + theTime.Minute;
            return time;
        }
        #endregion
    }
}