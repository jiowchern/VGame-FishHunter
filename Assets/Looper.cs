﻿using UnityEngine;
using System.Collections;
using Regulus.Remoting;
using System.Collections.Generic;
public class Looper : MonoBehaviour {

    public float MinSeconds;
    public float MaxSeconds;
    public GameObject Prefab;
    float _Time;
    GameObject _Current;
    private Client _Client;
    VGame.Project.FishHunter.IPlayer _Player;

    Queue<FishCollider> _Fishs;

    public Looper()
    {
        _Fishs = new Queue<FishCollider>();
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

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
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

        if (_Player != null)
        {
            foreach (var fish in _Current.GetComponentsInChildren<FishCollider>())
            {
                _Fishs.Enqueue(fish);
                _Player.RequestFish().OnValue += _InitFish ;
            }
        }
    }

    private void _InitFish(short obj)
    {
        _Fishs.Dequeue().Initial(obj); 
    }
}