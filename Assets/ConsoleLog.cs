using UnityEngine;
using System.Collections;

public class ConsoleLog : MonoBehaviour {


    public Console Console;

	// Use this for initialization
	void Start () {
        Application.logMessageReceived += Application_logMessageReceived;
	}
    

    void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        Console.WriteLine(string.Format("[UnityLog][{0}]{1}\n{2}" , type.ToString() , condition , stackTrace ));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
