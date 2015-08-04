using UnityEngine;
using System.Collections;
using Regulus.Remoting;
using System.Collections.Generic;
using VGame.Project.FishHunter.Common.GPI;

public class Looper : MonoBehaviour {

    public float MinSeconds;
    public float MaxSeconds;
    public GameObject Prefab;
    float _Time;
    GameObject _Current;
    private Client _Client;
    IPlayer _Player;

    

    public Looper()
    {
    
    }
    void OnDestroy()
    {
        if (_Client != null)
        {
            _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        }
    }
	// Use this for initialization
	void Start () 
    {
        _Client = Client.Instance;
        if(_Client != null )
        {
            _Client.User.PlayerProvider.Supply += PlayerProvider_Supply;
        }
	}

    void PlayerProvider_Supply(VGame.Project.FishHunter.Common.GPI.IPlayer obj)
    {
        _Player = obj;
    }
	
	// Update is called once per frame
	void Update () 
    {
        _Time -= UnityEngine.Time.deltaTime;
        if(_Time < 0)
        {
            _Reset();

            _Time = UnityEngine.Random.Range(MinSeconds, MaxSeconds);
        }

        
	}

    private void _Reset()
    {
        if (_Current != null)
            GameObject.Destroy(_Current);
        _Current = null;
        _Current = GameObject.Instantiate(Prefab);

        
    }
    
}
