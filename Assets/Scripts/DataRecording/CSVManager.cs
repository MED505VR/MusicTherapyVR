using System;
using System.IO;
using UnityEngine;

namespace DataRecording
{
    public static class CsvManager
    {
        private static readonly string ReportDirectoryName = "Report";
        private static readonly string ReportFileName = GetDayStamp() + ".csv";
        private static readonly string ReportSeparator = ",";
        private static readonly string TimeStampHeader = "time stamp";

        private static readonly string[] ReportHeaders = new string[18]
        {
            "Head X position", //float or double
            "Head Y position", //float or double
            "Head Z position", //float or double
            "Head X rotation", //float or double
            "Head Y rotation", //float or double
            "Head Z rotation", //float or double
            "Lhand X position", //float or double
            "Lhand Y position", //float or double
            "Lhand Z position", //float or double
            "Lhand X rotation", //float or double
            "Lhand Y rotation", //float or double
            "Lhand Z rotation", //float or double
            "Rhand X position", //float or double                                                                                                                                                                                                                                                                            
            "Rhand Y position", //float or double
            "Rhand Z position", //float or double
            "Rhand X rotation", //float or double
            "Rhand Y rotation", //float or double
            "Rhand Z rotation" //float or double
        };

        #region Interactions

        public static void AppendToReport(int num, string[] strings)
        {
            Debug.Log(GetDayStamp());
            VerifyDirectory();
            VerifyFile(num);
            using (var sw = File.AppendText(GetFilePath(num)))
            {
                var finalString = "";
                finalString += ReportSeparator + GetTimeStamp();
                for (var i = 0; i < strings.Length; i++)
                {
                    if (finalString != "") finalString += ReportSeparator;
                    finalString += strings[i];
                }

                sw.WriteLine(finalString);
            }
        }

        public static void CreateReport(int num)
        {
            VerifyDirectory();
            using (var sw = File.CreateText(GetFilePath(num)))
            {
                var finalString = "";
                finalString += ReportSeparator + TimeStampHeader;
                for (var i = 0; i < ReportHeaders.Length; i++)
                {
                    if (finalString != "") finalString += ReportSeparator;

                    finalString += ReportHeaders[i];
                }

                sw.WriteLine(finalString);
            }
        }

        #endregion

        #region Operations

        private static void VerifyDirectory()
        {
            var dir = GetDirectoryPath();
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        private static void VerifyFile(int playernum)
        {
            var file = GetFilePath(playernum);
            if (!File.Exists(file)) CreateReport(playernum);
        }

        #endregion

        #region Queries

        private static string GetDirectoryPath()
        {
            return Application.dataPath + "/" + ReportDirectoryName + "/" + GetDateStamp();
        }

        private static string GetFilePath(int playerNum)
        {
            return GetDirectoryPath() + "/" + playerNum + ReportFileName;
        }

        private static string GetTimeStamp()
        {
            return DateTime.UtcNow.ToString();
        }

        private static string GetDayStamp()
        {
            var theTime = DateTime.Now;
            var time = "T" + theTime.Hour + "-" + theTime.Minute;
            return time;
        }

        private static string GetDateStamp()
        {
            var theTime = DateTime.Now;
            var date = theTime.Year + "-" + theTime.Month + "-" + theTime.Day;
            return date;
        }

        #endregion
    }
}