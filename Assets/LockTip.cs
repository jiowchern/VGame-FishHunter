using UnityEngine;
using System.Collections;

public class LockTip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}


    public void Set(bool set)
    {
        GetComponent<MeshRenderer>().enabled = set;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
