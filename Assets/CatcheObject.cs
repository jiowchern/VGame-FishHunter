using UnityEngine;
using System.Collections;

public class CatcheObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Enabled()
    {
        gameObject.SetActive(true);
    }

    internal void Disable()
    {
        gameObject.SetActive(false);
    }
}
