using UnityEngine;
using System.Collections;

public class StaticFishInitial : MonoBehaviour {
    private Client _Client;

	// Use this for initialization
	void Start () {
        

        _Client = Client.Instance;
        _Client.User.PlayerProvider.Supply += PlayerProvider_Supply;

	}

    void PlayerProvider_Supply(VGame.Project.FishHunter.Common.GPI.IPlayer obj)
    {
        _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        
    }

   
	// Update is called once per frame
	void Update () {
	
	}
}
