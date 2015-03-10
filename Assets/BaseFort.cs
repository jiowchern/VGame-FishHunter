using UnityEngine;
using System.Collections;

public class BaseFort : MonoBehaviour {

    public GameObject Bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            var instance = GameObject.Instantiate(Bullet);
            instance.GetComponent<BulletCollider>().Direction = _GetDirection(Input.mousePosition);
            var pos = CameraHelper.Front.WorldToScreenPoint(transform.position);            

            instance.transform.position = CameraHelper.Front.ScreenToWorldPoint(pos);            
        }
	}

    private Vector3 _GetDirection(Vector3 vector3)
    {
        var dir = vector3 - CameraHelper.Front.WorldToScreenPoint(transform.position);
        dir.z = 0;
        return dir;
    }
}
