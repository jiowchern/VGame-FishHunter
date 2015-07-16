using UnityEngine;
using System.Collections;

public class UIMoney : MonoBehaviour {

    public UnityEngine.UI.Text Money;
    public UILabel Score;
    VGame.Project.FishHunter.IPlayer _Player;
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

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
        _Player.MoneyEvent += _Player_MoneyEvent;
    }

    void _Player_MoneyEvent(int obj)
    {
        if (Score != null)
            Score.text = obj.ToString();
        if (Money != null)
            Money.text = obj.ToString();   
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Quit()
    {
        if(_Player != null)
        {
            _Release();
            _Player.Quit();
            
        }
        Application.LoadLevel("SelectStage");
    }
}
