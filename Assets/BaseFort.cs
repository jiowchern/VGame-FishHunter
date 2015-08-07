using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;
using  System.Linq;

using VGame.Extension;
using VGame.Project.FishHunter;
using VGame.Project.FishHunter.ZsFormula.Rules;

public class BaseFort : MonoBehaviour
{

	

	
	[System.Serializable]
	public class BulletSource
	{
		public int No;
		public GameObject BulletPrefab;
	}

	public BulletSource[] BulletsSource;
	private VGame.Project.FishHunter.Common.GPI.IPlayer _Player;
	public Transform Base;
	public Transform Gun;
	public FortBehavior Behavior;


	private Magazine _Magazine;

	public VGame.Project.FishHunter.Common.Data.BULLET Bullet;    
	VGame.Project.FishHunter.Common.Data.BULLET _CurrentBullet;
	bool _Enable;

	private bool _CanFire;
    private int _CurrentOdds;
    private bool _CurrentLock;

    public BaseFort()
	{
		_Enable = true;
	}
	void OnDestroy()
	{
		Behavior.FireEvent -= _InFire;
		Behavior.IdleEvent -= _InIdle;

		if (FishEnvironment.Instance != null)
			FishEnvironment.Instance.TouchEvent -= _TouchFire;

		_Enable = false;
		if (_Client != null)
			_Client.User.PlayerProvider.Supply -= _PlayerSupply;
	}
	

	void Start () 
	{
		if (this.Base == null)
			this.Base = transform;

		if (this.Gun == null)
			Gun = transform;

		if (Client.Instance != null)
		{
			Client.Instance.User.PlayerProvider.Supply += _PlayerSupply;
			_Client = Client.Instance;           
		}
		else
		{
			_Magazine = new Magazine();            
		}
		Behavior.FireEvent += _InFire;
		Behavior.IdleEvent += _InIdle;

		FishEnvironment.Instance.TouchEvent += _TouchFire;


	}

	private void _TouchFire()
	{
		if (_CanFire)
		{
			int bulletid;
			if (_Magazine.TryGet(out bulletid))
			{
				_CanFire = false;
				var dir = _GetCurrentDirection();
				_RotationBase(dir);
				Behavior.Fire(bulletid , dir);

			}

		}
	}

	private void _InIdle()
	{
		_CanFire = true;

	}

	private void _InFire(int bulletid,int bullet_type,Vector3 direction)
	{
		_LaunchBullet(bulletid , bullet_type, direction);
	}

	

	private void _PlayerSupply(VGame.Project.FishHunter.Common.GPI.IPlayer obj)
	{
		_Player = obj;
		_Magazine = new Magazine(_Player);        
	}
	
	// Update is called once per frame	
	void Update ()
	{


		if (_Magazine != null)
			_Magazine.Reload();

		

		_UpdateFort();
	}

	private static Vector3 _GetPosition()
	{
		var defaultPosition = Input.mousePosition;
		var target = FishEnvironment.Instance.SelectedId;
		if (target != 0)
		{
			var fish = FishCollider.Find(target);
		
			if (fish != null)
			{
				var firstPosition = fish.transform.position;
				var cameraPosition = CameraHelper.Middle.GetScreenPoint(firstPosition);				
				return cameraPosition;
			}
			
		}
		return defaultPosition;
	}

	private void _UpdateFort()
	{
        Lock = FishEnvironment.Instance.Lock;
		if (_Player != null)
		{
			Bullet = _Player.Bullet;
            Odds = _Player.WeaponOdds;		    
		}
        if (Bullet != _CurrentBullet || Odds != _CurrentOdds || Lock != _CurrentLock)
		{
            Behavior.Idle(Bullet, FishEnvironment.Instance.Lock, Odds);
			_CurrentBullet = Bullet;
            _CurrentOdds = Odds ;
		    _CurrentLock = Lock;
		}
	}

    public bool Lock { get; set; }

    public int Odds { get; set; }

    private VGame.Project.FishHunter.Common.Data.BULLET _GetCurrentBullet()
	{
		return _Player != null
				   ? _Player.Bullet
				   : VGame.Project.FishHunter.Common.Data.BULLET.WEAPON1;
	}

	

	private void _LaunchBullet(int bullet, int bulletType, Vector3 direction)
	{
		_SpawnBullet(bullet, bulletType, direction);
	}

	private Vector3 _GetCurrentDirection()
	{
		var touchPosition = BaseFort._GetPosition();
		var dir = _GetDirection(touchPosition);
		return dir;
	}

	private void _RotationBase(Vector3 dir)
	{
		var angle = BaseFort._GetAngle(dir, Vector3.right);
		Base.rotation = Quaternion.Euler(0, 0, angle);
	}

	private GameObject _FindBullet(int bulletType)
	{
		return _Player != null
				   ? (from w in this.BulletsSource where w.No == bulletType select w.BulletPrefab).FirstOrDefault()
				   : null;
	}

	private void _SpawnBullet(int id, int bulletType, Vector3 dir)
	{
		var instance = GameObject.Instantiate(_FindBullet(bulletType));
		var collider = instance.GetComponent<BulletCollider>();
		collider.Id = id;		
		_SetRotation(dir, instance);

		_SetPosition(instance);
		
	}

	private void _SetPosition(GameObject instance)
	{
		instance.transform.position = Gun.position;
	}

	static float _GetAngle(Vector3 dir , Vector3 to)
	{
		var angle = Vector2.Angle(dir, to);
		Vector3 cross = Vector3.Cross(dir, to);
		if (cross.z > 0)
			angle = 360 - angle;
		return angle;
	}
	/*private static void _SetRotation(Vector3 dir, Transform t)
	{

		var angle = _GetAngle(dir , Vector3.up);
		t.rotation = Quaternion.Euler(0, 0, angle);        
	}*/
	private static void _SetRotation(Vector3 dir, GameObject instance)
	{
		instance.GetComponent<BulletCollider>().Direction = dir.normalized;

		//_SetRotation(dir, instance.transform );
		
	}

	private Vector3 _GetDirection(Vector3 vector3)
	{

		var dir = vector3 - CameraHelper.Front.WorldToScreenPoint(this.Base.position);        
		return dir;
	}

	public Client _Client { get; set; }
}