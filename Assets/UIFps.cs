using UnityEngine;
using System.Collections;

public class UIFps : MonoBehaviour 
{
    private Client _Client;
    public UnityEngine.UI.Text Text;
    Regulus.Utility.TimeCounter _TimeCounter;
    private Regulus.Utility.IOnline _Online;
	// Use this for initialization
	void Start () {
        _Client = Client.Instance;
        _Client.InitialDoneEvent += _Client_InitialDoneEvent;
        _TimeCounter = new Regulus.Utility.TimeCounter();
	}

    void OnDestroy()
    {
        _Client.User.Remoting.OnlineProvider.Supply -= OnlineProvider_Supply;
        _Client.User.Remoting.OnlineProvider.Unsupply -= OnlineProvider_Unsupply;
        _Client.InitialDoneEvent -= _Client_InitialDoneEvent;
        if (_Online != null)
            _Online.DisconnectEvent -= _Online_DisconnectEvent;
    }

    void _Client_InitialDoneEvent()
    {
        _Client.User.Remoting.OnlineProvider.Supply += OnlineProvider_Supply;
        _Client.User.Remoting.OnlineProvider.Unsupply += OnlineProvider_Unsupply;
    }

    void OnlineProvider_Unsupply(Regulus.Utility.IOnline obj)
    {
        
        if (_Online != null)
        {
            _Online.DisconnectEvent -= _Online_DisconnectEvent;
            _Online = null;
            Text.text = "offline";
        }
        
    }

    void OnlineProvider_Supply(Regulus.Utility.IOnline obj)
    {
        _Online = obj;
        _Online.DisconnectEvent += _Online_DisconnectEvent;
    }

    void _Online_DisconnectEvent()
    {
        _Online.DisconnectEvent -= _Online_DisconnectEvent;
        _Online = null;
        Text.text = "offline";
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if(_TimeCounter.Second >= 1.0f && _Online != null)
        {
            Text.text = _Online.Ping.ToString();
            _TimeCounter.Reset();
        }
       


	}
}
