using UnityEngine;
using System.Collections;

public class BaseFort : MonoBehaviour {

    public GameObject Bullet;
    private VGame.Project.FishHunter.IPlayer _Player;

    bool _Enable;
    public BaseFort()
    {
        _Enable = true;
    }
	// Use this for initialization
	void Start () 
    {

        if (Client.Instance != null)
        {

            Client.Instance.User.PlayerProvider.Supply += _PlayerSupply;
            _Client = Client.Instance;
        }
            
	}

    void OnDestroy()
    {
        _Enable = false;
        if (_Client != null)
            _Client.User.PlayerProvider.Supply -= _PlayerSupply;
    }

    private void _PlayerSupply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0) && _Player != null)
        {
            var touchPosition = Input.mousePosition;
            var dir = _GetDirection(touchPosition);

            _Player.RequestBullet().OnValue += (bullet) => 
            {
                if (bullet != 0 && _Enable)
                    _SpawnBullet(bullet , dir);
            };
            
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

    private void _SpawnBullet(int id,Vector3 dir)
    {
        var instance = GameObject.Instantiate(Bullet);
        var collider = instance.GetComponent<BulletCollider>();
        collider.Id = id;
        collider.Mode = BulletCollider.MODE.TRIGGER;
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

    public Client _Client { get; set; }
}
