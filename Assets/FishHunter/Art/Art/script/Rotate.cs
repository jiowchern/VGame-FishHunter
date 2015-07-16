using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float speedX = 0f;
	public float speedY = 100f;
	public float speedZ = 0f;
	
	private Transform tr;
	
	void Awake()
	{
		tr = GetComponent<Transform>();
	}
	
	void Update(){
	
	{
		tr.Rotate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime);
	}
}
}

