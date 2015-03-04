using UnityEngine;
using System.Collections;

public class Replay : MonoBehaviour {

    
    Vector3 _Position;
    
	// Use this for initialization
	void Start () {
        _Position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnReplay()
    {
        gameObject.transform.position = _Position;        
        
    }
}
