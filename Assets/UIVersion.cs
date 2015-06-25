using UnityEngine;
using System.Collections;

public class UIVersion : MonoBehaviour {

    public UnityEngine.UI.Text Version;
	// Use this for initialization
	void Start () {
        Version.text = Client.Instance.Version;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
