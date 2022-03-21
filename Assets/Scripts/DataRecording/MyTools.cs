using UnityEditor;
using UnityEngine;

namespace DataRecording
{
    public class MyTools : MonoBehaviour
    {
        void FixedUpdate()
        {
            CsvManager.AppendToReport(
                new string[36]
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
                }
            );
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
}
