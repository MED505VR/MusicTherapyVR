using DataRecording;
using UnityEditor;
using UnityEngine;

public static class MyTools 
{
    [MenuItem("My Tools/Add To Report %F1")]
    static void DEV_AppendToReport()
    {
        CsvManager.AppendToReport(
            new string[18]
            {
                "Head X position",  //Get head X position
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
            }
        );
        EditorApplication.Beep();
        Debug.Log("Report updated succesfully!");
    }
    
    [MenuItem("My Tools/Reset Report %F12")]
    static void DEV_ResetReport()
    {
        CsvManager.CreateReport();
        EditorApplication.Beep();
        Debug.Log("Report reset");
    }
    
    
}
