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


    public float FrontSpeed;

	
	private VGame.Project.FishHunter.IPlayer _Player;
	
	bool _Enable;
	// Use this for initialization
	void Start () 
	{
		_Enable = true;
		if(Client.Instance!=null)
		{
			_Client = Client.Instance;
			Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
			_Client.User.PlayerProvider.Unsupply += PlayerProvider_Unsupply;
		}

        FrontSpeed = Vector3.Distance(CameraHelper.Front.GetScreenPoint(((Vector3)Direction) * Speed), Vector3.zero);
	}

    

    void PlayerProvider_Unsupply(VGame.Project.FishHunter.IPlayer obj)
	{
		_Player = null;
	}

	void OnDestroy()
	{
		_Enable = false;
		_Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
		_Client.User.PlayerProvider.Unsupply -= PlayerProvider_Unsupply;
	}

	void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
	{
		_Player = obj;
	}
	
	// Update is called once per frame
	void Update () 
	{

        transform.position = ((Vector3)Direction * UnityEngine.Time.deltaTime * FrontSpeed) + transform.position;				

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
		System.Collections.Generic.List<VGame.Project.FishHunter.FishBounds> hits = new System.Collections.Generic.List<VGame.Project.FishHunter.FishBounds>();
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

	public Client _Client { get; set; }
}

