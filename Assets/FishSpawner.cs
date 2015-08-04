using UnityEngine;
using System.Collections;
using VGame.Project.FishHunter.Common.GPI;

public class FishSpawner : MonoBehaviour {


    public GameObject Fish;
    private Client _Client;
    private IPlayer _Player;    
	// Use this for initialization
	void Start () 
    {
	    if(Client.Instance != null)
        {
            _Client = Client.Instance;

            _Client.User.PlayerProvider.Supply += PlayerProvider_Supply;
        }
	}

    void OnDestroy()
    {
        if(_Client != null)
        {
            _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
            _Player = null;
        }
    }

    void PlayerProvider_Supply(IPlayer obj)
    {
        _Player = obj;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    internal FishCollider Spawn()
    {
        var fih = GameObject.Instantiate(Fish);
        var collider = fih.GetComponent<FishCollider>();

        
        return collider;
    }
}
