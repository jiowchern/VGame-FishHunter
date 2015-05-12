using UnityEngine;
using System.Collections;

public class UIMoney : MonoBehaviour {

    public UnityEngine.UI.Text Money;
    VGame.Project.FishHunter.IPlayer _Player;
	// Use this for initialization
	void Start () {
	
        if(Client.Instance)
        {
            Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
        }
	}

    void OnDestroy()
    {
        if (Client.Instance != null)
        {
            Client.Instance.User.PlayerProvider.Supply -= PlayerProvider_Supply;
            if (_Player != null)
            _Player.MoneyEvent -= _Player_MoneyEvent;
        }
    }

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
        _Player.MoneyEvent += _Player_MoneyEvent;
    }

    void _Player_MoneyEvent(int obj)
    {
        Money.text = obj.ToString();   
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
