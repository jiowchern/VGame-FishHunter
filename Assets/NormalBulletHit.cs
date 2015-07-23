using UnityEngine;


using VGame.Project.FishHunter;

public class NormalBulletHit : BulletHitHandler  {

    public GameObject BoomBullet;

    // Update is called once per frame
	void Update () {
	
	}

    public override void Hit(int id, FishBounds[] hits)
    {
        if (hits.Length > 0)
        {
            _SpawnBoom(id);
            GameObject.DestroyObject(gameObject);
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
