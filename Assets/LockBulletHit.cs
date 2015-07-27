using UnityEngine;
using System.Linq;

using VGame.Extension;


[RequireComponent(typeof(BulletCollider))]
public class LockBulletHit : BulletHitHandler {

	public GameObject BoomBullet;

	

	private int _BulletId;

	// Use this for initialization
	void Start ()
	{
		var bullet = GetComponent<BulletCollider>();
		_BulletId = bullet.Id;

		var selectId = VGame.Project.FishHunter.FishEnvironment.Instance.Selected;
		var fish = FishCollider.Find(selectId);
		if (selectId != 0 && fish != null)
		{
			var end = CameraHelper.Middle.GetScreenPoint(fish.transform.position) ;
			var start = CameraHelper.Front.GetScreenPoint(transform.position);
			
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*_DeadTime -= UnityEngine.Time.deltaTime;
		if (_DeadTime < 0)
		{
			_Boom();
		}*/
	}

	private void _Boom()
	{
		_SpawnBoom(_BulletId);
		GameObject.DestroyObject(gameObject);
	}

	public override void Hit(int id, VGame.Project.FishHunter.FishBounds[] hits)
	{
		var env = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishEnvironment>();
		if(env.Selected != null)
		{
			if(hits.Any( fish => env.Selected == fish.Id))
			{
				_Boom();
			}
		}
	}

	private void _SpawnBoom(int id)
	{
		if (BoomBullet != null)
		{
			var boom = GameObject.Instantiate(BoomBullet);
			var collider = boom.GetComponent<BulletCollider>();
			collider.Id = id;
			boom.transform.position = transform.position;
		}
	}
}
