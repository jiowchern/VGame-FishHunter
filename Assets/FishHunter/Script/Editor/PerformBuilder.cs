﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class PerformBuilder
{
    static string GetBuildPathAndroid()
    {
        string dirPath = Application.dataPath + "/../../../build/android";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        return dirPath;
    } 
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;

            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    } 


    [MenuItem("VGame/Update Version")]
	public static void UpdateVersion()
    {
        _VersionUpdate();
        
        
    }

    [MenuItem("VGame/Build Apk")]
    public static void BuildAndroid()
    {
        string[] scenes = GetBuildScenes();
        //string path = GetBuildPathAndroid();
        string path = UnityEditor.EditorUtility.OpenFilePanel("", UnityEditor.PlayerSettings.productName, "apk");

        if (string.IsNullOrEmpty(path) == false)
        {
            Debug.Log(string.Format("Path: \"{0}\"", path));
            BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
        }

    }

    [MenuItem("VGame/Release Apk To FishHunter.apk")]
    public static void ReleaseAndroid()
    {
        var sdk = EditorPrefs.GetString("AndroidSdkRoot");
        Debug.LogFormat("AndroidSdkRoot = {0}" , sdk);
        if(string.IsNullOrEmpty(sdk))
        {
            sdk = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");
            Debug.LogFormat("Environment AndroidSdkRoot = {0}", sdk);
        }

        if (string.IsNullOrEmpty(sdk))
        {
            sdk = @"D:\AppData\Local\Android\sdk";

            Debug.LogFormat("Default AndroidSdkRoot = {0}", sdk);
        }
        EditorPrefs.SetString("AndroidSdkRoot", sdk);


        string[] scenes = GetBuildScenes();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        BuildPipeline.BuildPlayer(scenes, "FishHunter.apk", BuildTarget.Android, BuildOptions.None);

    }

    public static void BuildWindows64()
    {
        _VersionUpdate();

        string[] scenes = GetBuildScenes();
        string path = WindowsPath( "/output/" + PlayerSettings.productName +".exe"); ;
        Debug.Log(string.Format("Path: \"{0}\"", path));
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.StandaloneWindows64 , BuildOptions.None);
    }

    private static void _VersionUpdate()
    {
        var versionData = System.IO.File.ReadAllText("Assets/FishHunter/Misc/Version.txt");        

        System.Version versionOld = new System.Version(versionData);
        System.Version versionNew = new System.Version(versionOld.Major, versionOld.Minor, versionOld.Build + 1, versionOld.Revision);

        Debug.Log(string.Format("Project Version {0} -> {1}.", versionOld.ToString(), versionNew.ToString()));
        System.IO.File.WriteAllText("Assets/FishHunter/Misc/Version.txt" , versionNew.ToString());
    }

    private static string WindowsPath(string name)
    {

        
        string dirPath = Application.dataPath + name;
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        return dirPath;
    }
}
