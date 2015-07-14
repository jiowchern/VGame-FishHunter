using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour {


    public GameObject Fish;
    private Client _Client;
    private VGame.Project.FishHunter.IPlayer _Player;    
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

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
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
