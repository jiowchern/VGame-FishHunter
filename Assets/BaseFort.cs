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
            var touchPosition = Input.mousePosition;
            var dir = _GetDirection(touchPosition);
            _SpawnBullet(dir);

            //_SpawnBulletLookAt(touchPosition);
        }
	}

    private void _SpawnBulletLookAt(Vector3 touchPosition)
    {
        var instance = GameObject.Instantiate(Bullet);

        _SetRotationLookAt(touchPosition, instance);

        _SetPosition(instance);
    }

    private void _SetRotationLookAt(Vector3 touchPosition, GameObject instance)
    {
        instance.GetComponent<BulletCollider>().Direction = _GetDirection(touchPosition);
        

        var ray = CameraHelper.Middle.ScreenPointToRay(touchPosition);

        instance.transform.LookAt(ray.GetPoint(-1000) , Vector3.forward);
    }

    private void _SpawnBullet(Vector3 dir)
    {
        var instance = GameObject.Instantiate(Bullet);

        _SetRotation(dir, instance);

        _SetPosition(instance);
        
    }

    private void _SetPosition(GameObject instance)
    {
        instance.transform.position = transform.position;
    }

    private static void _SetRotation(Vector3 dir, GameObject instance)
    {
        instance.GetComponent<BulletCollider>().Direction = dir;
        var angle = Vector2.Angle(dir, Vector2.up);
        Vector3 cross = Vector3.Cross(dir, Vector2.up);
        if (cross.z > 0)
            angle = 360 - angle;

        instance.transform.rotation = Quaternion.Euler(0, 0, angle);        
    }

    private Vector3 _GetDirection(Vector3 vector3)
    {
        var dir = vector3 - CameraHelper.Front.WorldToScreenPoint(transform.position);        
        return dir;
    }
}
