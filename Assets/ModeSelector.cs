using UnityEngine;
using System.Collections;

public class ModeSelector : MonoBehaviour 
{
    public Client Target;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RunRemoting()
    {
        Target.Mode = Client.MODE.REMOTING;
        Target.Initial();
    }

    public void RunStandalone()
    {
        Target.Mode = Client.MODE.Standalone;
        Target.Initial();
    }
}
