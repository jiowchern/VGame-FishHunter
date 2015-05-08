using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour {


    public delegate void VerifyCallback(string ip , int port,string account ,string password );
    public event VerifyCallback VerifyEvent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
