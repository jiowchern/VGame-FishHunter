using UnityEngine;
using System.Collections;

using HighlightingSystem;


public class HightlightingController : MonoBehaviour 
{
    //HighlighterController
    protected Highlighter h;

    // 
    protected void Awake()
    {
        h = GetComponent<Highlighter>();
        //if (h == null) { h = gameObject.AddComponent<Highlighter>(); }
        
    }

	// Use this for initialization
	void Start () 
    {
        h.ConstantOn(Color.yellow);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
