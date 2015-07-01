using UnityEngine;
using UnityEditor;
using System.Collections;

public class PathBuilder : EditorWindow
{
    class Point3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public GameObject Target;
    public TextAsset File;
    public GameObject Node;

    internal void Build()
    {

        var points = Regulus.Utility.CSV.Parse<Point3>(File.text, ",", "\n");

        int name = 1;
        foreach(var point in points)
        {
            var node = GameObject.Instantiate<GameObject>(Node);
            node.transform.parent = Target.transform;
            node.transform.position = new Vector3(point.x, point.y, point.z) + Target.transform.position;            
            node.name = name.ToString();
            name++;
        }        
    }

    [MenuItem("VGame/產生路徑")]
    public static void ShowWindow()
    {
        var pathNode = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/FishHunter/Entity/Path/Node.prefab", typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;
        var wnd = EditorWindow.GetWindow<PathBuilder>();
        wnd.Node = pathNode;
        wnd.Show();
    }


    void OnGUI()
    {
        
        EditorGUILayout.BeginVertical();
        //Node = EditorGUILayout.ObjectField(Node, typeof(GameObject), true) as GameObject;
        Target = EditorGUILayout.ObjectField("目標" , Target , typeof(GameObject) , true) as GameObject;
        File = EditorGUILayout.ObjectField("CSV" , File, typeof(TextAsset), true) as TextAsset;
        if(GUILayout.Button("製作"))
        {
            Build();
        }
        
        EditorGUILayout.EndVertical();
    }
}
