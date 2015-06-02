using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stage : MonoBehaviour {

    public int Id;
    public string Scene;
    public Button UI;
	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Enable()
    {
        UI.interactable = true;
    }

    internal void Disable()
    {
        UI.interactable = false;
    }
}
