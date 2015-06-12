using UnityEngine;
using System.Collections;

public class RunHide : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
