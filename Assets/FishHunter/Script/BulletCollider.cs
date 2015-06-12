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
        transform.position = ((Vector3)Direction * UnityEngine.Time.deltaTime  )+ transform.position;
        //transform.Translate(Direction * UnityEngine.Time.deltaTime);
        

        VGame.Project.FishHunter.FishBounds[] fishs = VGame.Project.FishHunter.FishSet.Find(CameraHelper.Front , Collider.bounds);

        if(fishs.Length > 0)
        {



            var hits = _HitDetection(fishs);

            if (hits.Length > 0)
            {
                if (Mode == MODE.DAMAGER)
                {
                    _HitRequest(hits);
                }
                else if (Mode == MODE.TRIGGER)
                {
                    GameObject.Destroy(gameObject);
                    _SpawnBoom();
                }
                    
            }
                
        }

        if(ViewChecker.isVisible == false)
            GameObject.Destroy(gameObject);




	}
    bool _Requested;
    private void _HitRequest(VGame.Project.FishHunter.FishBounds[] fishs)
    {
        if (_Requested || _Player == null)
            return;
        _Requested = true;
        _Player.Hit(Id, (from f in fishs select f.Id).ToArray()).OnValue += (count) =>
        {
            _Requested = false;
            if (count > 0 && _Enable)
                GameObject.Destroy(gameObject);
        };
    }

    private void _SpawnBoom()
    {
        if (BoomBullet != null)
        {
            var boom = GameObject.Instantiate(BoomBullet);
            var collider = boom.GetComponent<BulletCollider>();
            collider.Id = Id;
            collider.Mode = MODE.DAMAGER;
            boom.transform.position = transform.position;
        }
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

