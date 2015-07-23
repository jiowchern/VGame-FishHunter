using UnityEngine;
using System.Linq;
using System.Collections;

public class LockBulletHit : BulletHitHandler {

    public GameObject BoomBullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Hit(int id, VGame.Project.FishHunter.FishBounds[] hits)
    {
        var env = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishEnvironment>();
        if(env.Selected != null)
        {
            if(hits.Any( fish => env.Selected == fish))
            {
                _SpawnBoom(id);
                GameObject.DestroyObject(gameObject);
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
