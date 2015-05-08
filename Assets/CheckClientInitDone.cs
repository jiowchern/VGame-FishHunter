using UnityEngine;
using System.Collections;

public class CheckClientInitDone : MonoBehaviour 
{

    public Client Target;
    public string NextScene;
	// Use this for initialization
	void Start () {
        Target.InitialDoneEvent += Target_InitialDoneEvent;
	}

    void Target_InitialDoneEvent()
    {
        Target.InitialDoneEvent -= Target_InitialDoneEvent;
        Application.LoadLevel(NextScene);
        
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
