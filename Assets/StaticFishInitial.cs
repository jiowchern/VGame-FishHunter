using UnityEngine;
using System.Collections;

public class StaticFishInitial : MonoBehaviour {
    private Client _Client;

	// Use this for initialization
	void Start () {
        

        _Client = Client.Instance;
        _Client.User.PlayerProvider.Supply += PlayerProvider_Supply;

	}

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        obj.RequestFish().OnValue += StaticFishInitial_OnValue;
    }

    void StaticFishInitial_OnValue(short obj)
    {
        var fish = GetComponent<FishCollider>();
        fish.Initial(obj);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
