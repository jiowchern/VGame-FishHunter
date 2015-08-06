using System.Linq;
using UnityEngine;
using System.Collections;
using VGame.Extension;
public class BulletCollider : MonoBehaviour 
{
	

	public int Id;
	
	public Vector2 Direction;

	public PolygonCollider2D Collider;

	public Renderer ViewChecker;


	public BulletHitHandler HitHandler;

	public float Speed = 100;


    private Rigidbody2D _Rigidbody2D;

	
	private VGame.Project.FishHunter.Common.GPI.IPlayer _Player;
	
	bool _Enable;

	private Client _Client;
    private float _Angle;

    void Start () 
	{
		_Enable = true;
		if(Client.Instance!=null)
		{
			_Client = Client.Instance;
			Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
			_Client.User.PlayerProvider.Unsupply += PlayerProvider_Unsupply;
		}

        _Rigidbody2D = GetComponent<Rigidbody2D>();

        _Rigidbody2D.AddForce(Direction * Speed);
        
	}

	

	void PlayerProvider_Unsupply(VGame.Project.FishHunter.Common.GPI.IPlayer obj)
	{
		_Player = null;
	}

	void OnDestroy()
	{
		_Enable = false;
		_Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
		_Client.User.PlayerProvider.Unsupply -= PlayerProvider_Unsupply;
	}

	void PlayerProvider_Supply(VGame.Project.FishHunter.Common.GPI.IPlayer obj)
	{
		_Player = obj;
	}
    static float _GetAngle(Vector3 dir, Vector3 to)
    {
        var angle = Vector2.Angle(dir, to);
        Vector3 cross = Vector3.Cross(dir, to);
        if (cross.z > 0)
            angle = 360 - angle;
        return angle;
    }
	// Update is called once per frame
	void Update ()
	{
        var dir = _Rigidbody2D.velocity;
        var a = _GetAngle(dir , Vector3.up);
	    //transform.rotation = rot;
        _Rigidbody2D.rotation = a;
        //_Rigidbody2D.MoveRotation(angle);
	    
        
	    
        
        
		//transform.position = ((Vector3)Direction * UnityEngine.Time.deltaTime * Speed) + transform.position;				

		VGame.Project.FishHunter.FishBounds[] fishs = VGame.Project.FishHunter.FishSet.Find(CameraHelper.Front , Collider.bounds);

		if(fishs.Length > 0)
		{
			var hits = _HitDetection(fishs);
			HitHandler.Hit(Id , hits );
		}
        
		if(ViewChecker.isVisible == false)
			GameObject.Destroy(gameObject);
	}

	


	private VGame.Project.FishHunter.FishBounds[] _HitDetection(VGame.Project.FishHunter.FishBounds[] fishs)
	{
		var hits = new System.Collections.Generic.List<VGame.Project.FishHunter.FishBounds>();
		var bulletCollider = Collider.ToRegulusPolygon();
		foreach (var fish in fishs)
		{
			if (fish.IsHit(bulletCollider))
			{
				hits.Add(fish);
			}

		}

		return hits.ToArray();
	}

	
}

