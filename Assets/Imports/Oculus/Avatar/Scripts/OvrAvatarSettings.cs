using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
public sealed class OvrAvatarSettings : ScriptableObject
{
    public static string AppID
    {
        get => Instance.ovrAppID;
        set => Instance.ovrAppID = value;
    }

    public static string MobileAppID
    {
        get => Instance.ovrGearAppID;
        set => Instance.ovrGearAppID = value;
    }

    private static OvrAvatarSettings instance;

    public static OvrAvatarSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<OvrAvatarSettings>("OvrAvatarSettings");

                // This can happen if the developer never input their App Id into the Unity Editor
                // Use a dummy object with defaults for the getters so we don't have a null pointer exception
                if (instance == null)
                {
                    instance = CreateInstance<OvrAvatarSettings>();

#if UNITY_EDITOR
                    // Only in the editor should we save it to disk
                    var properPath = System.IO.Path.Combine(Application.dataPath, "Resources");
                    if (!System.IO.Directory.Exists(properPath))
                        UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");

                    var fullPath = System.IO.Path.Combine(
                        System.IO.Path.Combine("Assets", "Resources"),
                        "OvrAvatarSettings.asset"
                    );
                    UnityEditor.AssetDatabase.CreateAsset(instance, fullPath);
#endif
                }
            }

            return instance;
        }

        set { instance = value; }
    }

    [SerializeField] private string ovrAppID = "";

    [SerializeField] private string ovrGearAppID = "";
}