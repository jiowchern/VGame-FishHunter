using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;
using  System.Linq;

using VGame.Extension;
using VGame.Project.FishHunter;

public class BaseFort : MonoBehaviour
{
	[System.Serializable]
	public class BulletSource
	{
		public VGame.Project.FishHunter.BULLET Type;
		public GameObject BulletPrefab;
	}

	public BulletSource[] BulletsSource;    
	private VGame.Project.FishHunter.IPlayer _Player;
	public Transform Base;
	public Transform Gun;
	public FortBehavior Behavior;

	public VGame.Project.FishHunter.BULLET Bullet;
	VGame.Project.FishHunter.BULLET _CurrentBullet;
	bool _Enable;
	public BaseFort()
	{
		_Enable = true;
	}
	// Use this for initialization
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
		if (Input.GetMouseButtonDown(0))
		{            
			var touchPosition = _GetPosition();
			var dir = _GetDirection(touchPosition);

			var result = this.Behavior.Fire(_GetCurrentBullet());
			if (result != null)
			{
				result.OnValue += (bullet) =>
				{
					if (_Player != null)
						_PlayerLaunch(dir);
					else
					{
						_LaunchBullet(dir, 0);
					}
				};
			}
				
		}


		_UpdateFort();
	}

	private static Vector3 _GetPosition()
	{
		var defaultPosition = Input.mousePosition;
		var target = FishEnvironment.Instance.Selected;
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

		if (_Player != null)
		{
			Bullet = _Player.Bullet;
		}
		if (Bullet != _CurrentBullet)
		{
			_UpdateLock(Bullet);
			this.Behavior.Idle(Bullet);

			_CurrentBullet = Bullet;
		}
	}

	private void _UpdateLock(BULLET bullet)
	{
		var data = new System.Collections.Generic.HashSet<BULLET>();
		data.Add(BULLET.WEAPON2);
		data.Add(BULLET.WEAPON4);
		data.Add(BULLET.WEAPON6);
		data.Add(BULLET.WEAPON8);
		
		FishEnvironment.Instance.Lock = data.Contains(bullet);
	}

	private BULLET _GetCurrentBullet()
	{
		return _Player != null
				   ? _Player.Bullet
				   : VGame.Project.FishHunter.BULLET.WEAPON1;
	}

	private void _PlayerLaunch(Vector3 dir)
	{
		
		_Player.RequestBullet().OnValue += (bullet) =>
			{
				if (bullet == 0 || !_Enable)
				{
					return;
				}
				_LaunchBullet(dir, bullet);
			};
	}

	private void _LaunchBullet(Vector3 dir, int bullet)
	{
		var angle = BaseFort._GetAngle(dir, Vector3.right);
		Base.rotation = Quaternion.Euler(0, 0, angle);
		_SpawnBullet(bullet, dir);
	}

	

	private GameObject _FindBullet()
	{
		if (_Player != null)
		{
			return (from w in this.BulletsSource where w.Type == _Player.Bullet select w.BulletPrefab).FirstOrDefault();
		}
		return null;
	}
	

	
	private void _SpawnBullet(int id,Vector3 dir)
	{
		var instance = GameObject.Instantiate(_FindBullet());
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
	private static void _SetRotation(Vector3 dir, Transform t)
	{

		var angle = _GetAngle(dir , Vector3.up);
		t.rotation = Quaternion.Euler(0, 0, angle);        
	}
	private static void _SetRotation(Vector3 dir, GameObject instance)
	{
		instance.GetComponent<BulletCollider>().Direction = dir.normalized;

		_SetRotation(dir, instance.transform );
		
	}

	private Vector3 _GetDirection(Vector3 vector3)
	{

		var dir = vector3 - CameraHelper.Front.WorldToScreenPoint(this.Base.position);        
		return dir;
	}

	public Client _Client { get; set; }
}
