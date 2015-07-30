using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour {

    public UnityEngine.UI.Text Account;
    public UnityEngine.UI.Text Password;
    public UnityEngine.UI.Text IP;
    public UnityEngine.UI.Text Port;
    public delegate void VerifyCallback(string ip , int port,string account ,string password );
    public event VerifyCallback VerifyEvent;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
    	
	}

    public void Verify()
    {
        VerifyEvent(IP.text , int.Parse(Port.text) , Account.text , Password.text );
    }
}
