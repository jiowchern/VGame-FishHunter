using UnityEngine;
using System.Collections;

public class BaseFort : MonoBehaviour {

    public GameObject Bullet;
    private VGame.Project.FishHunter.IPlayer _Player;
    public Transform Owner;
    bool _Enable;
    public BaseFort()
    {
        _Enable = true;
    }
	// Use this for initialization
	void Start () 
    {
        if (Owner == null)
            Owner = transform;
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
                {
                    
                    var angle = _GetAngle(dir, Vector3.right);
                    Owner.rotation = Quaternion.Euler(0,0,angle);
                    _SpawnBullet(bullet , dir);
                }
                    
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

    static float _GetAngle(Vector3 dir , Vector3 to)
    {
        var angle = Vector2.Angle(dir, to);
        Vector3 cross = Vector3.Cross(dir, to);
        if (cross.z > 0)
            angle = 360 - angle;
        return angle;
    }
    private static void _SetRotation(Vector3 dir, Transform t)
    {

        var angle = _GetAngle(dir , Vector3.up);
        t.rotation = Quaternion.Euler(0, 0, angle);        
    }
    private static void _SetRotation(Vector3 dir, GameObject instance)
    {
        instance.GetComponent<BulletCollider>().Direction = dir;

        _SetRotation(dir, instance.transform );
        
    }

    private Vector3 _GetDirection(Vector3 vector3)
    {

        var dir = vector3 - CameraHelper.Front.WorldToScreenPoint(Owner.position);        
        return dir;
    }

    public Client _Client { get; set; }
}
