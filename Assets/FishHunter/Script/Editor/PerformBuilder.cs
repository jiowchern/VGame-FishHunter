using UnityEngine;
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

	public static void BuildAndroid()
    {
        string[] scenes = GetBuildScenes();
        string path = GetBuildPathAndroid();
        Debug.Log(string.Format("Path: \"{0}\"", path)); 
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None); 
    }

    public static void BuildWindows64()
    {
        string[] scenes = GetBuildScenes();
        string path = WindowsPath( "/output/" + PlayerSettings.productName +".exe"); ;
        Debug.Log(string.Format("Path: \"{0}\"", path));
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.StandaloneWindows64 , BuildOptions.None);
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
