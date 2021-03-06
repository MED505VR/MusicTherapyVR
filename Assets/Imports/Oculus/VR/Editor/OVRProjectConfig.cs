/************************************************************************************

Copyright   :   Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus SDK License Version 3.4.1 (the "License");
you may not use the Oculus SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

https://developer.oculus.com/licenses/sdk-3.4.1

Unless required by applicable law or agreed to in writing, the Oculus SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[Serializable]
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class OVRProjectConfig : ScriptableObject
{
    public enum DeviceType
    {
        //GearVrOrGo = 0, // DEPRECATED
        Quest = 1,
        Quest2 = 2
    }

    public enum HandTrackingSupport
    {
        ControllersOnly = 0,
        ControllersAndHands = 1,
        HandsOnly = 2
    }


    public List<DeviceType> targetDeviceTypes;
    public HandTrackingSupport handTrackingSupport;

    public bool disableBackups;
    public bool enableNSCConfig;
    public string securityXmlPath;

    public bool skipUnneededShaders;
    public bool focusAware;
    public bool requiresSystemKeyboard;

    //public const string OculusProjectConfigAssetPath = "Assets/Oculus/OculusProjectConfig.asset";

    static OVRProjectConfig()
    {
        // BuildPipeline.isBuildingPlayer cannot be called in a static constructor
        // Run Update once to call GetProjectConfig then remove delegate
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        // Initialize the asset if it doesn't exist
        GetProjectConfig();
        // Stop running Update
        EditorApplication.update -= Update;
    }

    private static string GetOculusProjectConfigAssetPath()
    {
        var so = CreateInstance(typeof(OVRPluginUpdaterStub));
        var script = MonoScript.FromScriptableObject(so);
        var assetPath = AssetDatabase.GetAssetPath(script);
        var editorDir = Directory.GetParent(assetPath).FullName;
        var ovrDir = Directory.GetParent(editorDir).FullName;
        var oculusDir = Directory.GetParent(ovrDir).FullName;
        var configAssetPath = Path.GetFullPath(Path.Combine(oculusDir, "OculusProjectConfig.asset"));
        var configUri = new Uri(configAssetPath);
        var projectUri = new Uri(Application.dataPath);
        var relativeUri = projectUri.MakeRelativeUri(configUri);

        return relativeUri.ToString();
    }

    public static OVRProjectConfig GetProjectConfig()
    {
        OVRProjectConfig projectConfig = null;
        var oculusProjectConfigAssetPath = GetOculusProjectConfigAssetPath();
        try
        {
            projectConfig =
                AssetDatabase.LoadAssetAtPath(oculusProjectConfigAssetPath, typeof(OVRProjectConfig)) as
                    OVRProjectConfig;
        }
        catch (Exception e)
        {
            Debug.LogWarningFormat("Unable to load ProjectConfig from {0}, error {1}", oculusProjectConfigAssetPath,
                e.Message);
        }

        // Initialize the asset only if a build is not currently running.
        if (projectConfig == null && !BuildPipeline.isBuildingPlayer)
        {
            projectConfig = CreateInstance<OVRProjectConfig>();
            projectConfig.targetDeviceTypes = new List<DeviceType>();
            projectConfig.targetDeviceTypes.Add(DeviceType.Quest);
            projectConfig.targetDeviceTypes.Add(DeviceType.Quest2);
            projectConfig.handTrackingSupport = HandTrackingSupport.ControllersOnly;
            projectConfig.disableBackups = true;
            projectConfig.enableNSCConfig = true;
            projectConfig.skipUnneededShaders = false;
            projectConfig.focusAware = true;
            projectConfig.requiresSystemKeyboard = false;
            AssetDatabase.CreateAsset(projectConfig, oculusProjectConfigAssetPath);
        }

        // Force migration to Quest device if still on legacy GearVR/Go device type
        if (projectConfig.targetDeviceTypes.Contains((DeviceType)0)) // deprecated GearVR/Go device
        {
            projectConfig.targetDeviceTypes.Remove((DeviceType)0); // deprecated GearVR/Go device
            if (!projectConfig.targetDeviceTypes.Contains(DeviceType.Quest))
                projectConfig.targetDeviceTypes.Add(DeviceType.Quest);
            if (!projectConfig.targetDeviceTypes.Contains(DeviceType.Quest2))
                projectConfig.targetDeviceTypes.Add(DeviceType.Quest2);
        }

        return projectConfig;
    }

    public static void CommitProjectConfig(OVRProjectConfig projectConfig)
    {
        var oculusProjectConfigAssetPath = GetOculusProjectConfigAssetPath();
        if (AssetDatabase.GetAssetPath(projectConfig) != oculusProjectConfigAssetPath)
            Debug.LogWarningFormat("The asset path of ProjectConfig is wrong. Expect {0}, get {1}",
                oculusProjectConfigAssetPath, AssetDatabase.GetAssetPath(projectConfig));
        EditorUtility.SetDirty(projectConfig);
    }
}