using UnityEngine;
using System.Collections;

public class TouchBulletLauncher : MonoBehaviour 
{

    public GameObject Bullet;
    public Vector3 Direction;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0))
        {
            
            var instance = GameObject.Instantiate(Bullet);
            instance.GetComponent<BulletCollider>().Direction = Direction;
            var pos= Input.mousePosition;
            
            instance.transform.position = CameraHelper.Front.ScreenToWorldPoint(pos);            
        }
	}
}
