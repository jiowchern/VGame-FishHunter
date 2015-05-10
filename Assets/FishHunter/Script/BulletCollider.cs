using System.Linq;
using UnityEngine;
using System.Collections;
using VGame.Extension;
public class BulletCollider : MonoBehaviour 
{
    public int Id;

    public enum MODE { TRIGGER, DAMAGER };
    public MODE Mode;
    public Vector2 Direction;

    public PolygonCollider2D Collider;

    public Renderer ViewChecker;

    public GameObject BoomBullet;
    private VGame.Project.FishHunter.IPlayer _Player;
	// Use this for initialization
	void Start () 
    {
        if(Client.Instance!=null)
            Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
	}

    void OnDestroy()
    {
        if (Client.Instance != null)
            Client.Instance.User.PlayerProvider.Supply -= PlayerProvider_Supply;
    }

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = ((Vector3)Direction * UnityEngine.Time.deltaTime  )+ transform.position;
        //transform.Translate(Direction * UnityEngine.Time.deltaTime);
        

        VGame.Project.FishHunter.FishBounds[] fishs = VGame.Project.FishHunter.FishSet.Find(CameraHelper.Front , Collider.bounds);

        if(fishs.Length > 0)
        {
            

            bool hit = false;
            var bulletCollider= Collider.ToRegulusPolygon();
            foreach(var fish in fishs)
            {
                if (fish.IsHit(bulletCollider))
                {
                    hit = true;                    
                }
                    
            }

            

            if(hit)
            {
                if (Mode == MODE.DAMAGER)
                    _Player.Hit(Id , (from f in fishs select f.Id).ToArray());

                if(BoomBullet != null)
                {
                    var boom = GameObject.Instantiate(BoomBullet);
                    var collider = boom.GetComponent<BulletCollider>();
                    collider.Id = Id;
                    collider.Mode = MODE.DAMAGER;
                    boom.transform.position = transform.position;
                }
                
                GameObject.Destroy(gameObject);
            }
                
        }

        if(ViewChecker.isVisible == false)
            GameObject.Destroy(gameObject);




	}
}

