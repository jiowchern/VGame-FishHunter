using UnityEngine;
using System.Collections;

public class DontRemoveObjects : MonoBehaviour 
{
    public Object[] Targets;
	// Use this for initialization
	void Start () 
    {
        GameObject.DontDestroyOnLoad(gameObject);

        foreach(var tar in Targets)
        {
            GameObject.DontDestroyOnLoad(tar);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
