using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VGame.Project.FishHunter;

public class NormalNetHit : BulletHitHandler {

	// Use this for initialization
    void OnDestroy()
    {
        _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        _Client.User.PlayerProvider.Unsupply -= PlayerProvider_Unsupply;
    }
    // Use this for initialization
    void Start()
    {
        if (Client.Instance != null)
        {
            _Client = Client.Instance;
            Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
            _Client.User.PlayerProvider.Unsupply += PlayerProvider_Unsupply;
        }
    }

    private void PlayerProvider_Unsupply(IPlayer obj)
    {
        _Player = null;
    }

    private void PlayerProvider_Supply(IPlayer obj)
    {
        _Player = obj;
    }

    bool _Requested;

    private Client _Client;

    private IPlayer _Player;
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Hit(int id, VGame.Project.FishHunter.FishBounds[] hits)
    {
        if (hits.Length > 0)
        {
            _HitRequest(id, hits);
        }
    }

    private void _HitRequest(int id, IEnumerable<FishBounds> fishs)
    {
        if (_Requested || _Player == null)
            return;
        _Requested = true;
        _Player.Hit(id, (from f in fishs select f.Id).ToArray()).OnValue += (count) =>
        {
            _Requested = false;            
            GameObject.Destroy(gameObject);
        };
    }

}
