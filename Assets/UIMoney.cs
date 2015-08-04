using UnityEngine;
using System.Collections;
using VGame.Project.FishHunter.Common.GPI;

public class UIMoney : MonoBehaviour {

    
    public UILabel Score;
    VGame.Project.FishHunter.Common.GPI.IPlayer _Player;
    Client _Client;
	// Use this for initialization
	void Start () {
	
        if(Client.Instance)
        {
            Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;

            _Client = Client.Instance;
        }
	}

    void OnDestroy()
    {

        _Release();
        
    }

    private void _Release()
    {
        if (_Client != null)
            _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        if (_Player != null)
            _Player.MoneyEvent -= _Player_MoneyEvent;
    }

    void PlayerProvider_Supply(IPlayer obj)
    {
        _Player = obj;
        _Player.MoneyEvent += _Player_MoneyEvent;
    }

    void _Player_MoneyEvent(int obj)
    {
        if (Score != null)
            Score.text = obj.ToString();
    
    }
	

    
}
