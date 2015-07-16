using UnityEngine;
using System.Collections;

public class FISH_CONTROLER_TURN : MonoBehaviour {
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.KeypadEnter)) {

			transform.rotation = Quaternion.identity;
		}

		if(Input.GetKey(KeyCode.Delete))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.PageDown))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.Insert))
			transform.Rotate(Vector3.right, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.PageUp))
			transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.End))
			transform.Rotate(Vector3.forward, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.Home))
			transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);




	}

}
