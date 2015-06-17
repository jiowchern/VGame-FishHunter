using UnityEngine;
using System.Collections;

public class SceneResetter : MonoBehaviour 
{

    public string ResetScene;
    public Client Client;
    VGame.Project.FishHunter.IAccountStatus _AccountStatus;
    Regulus.Utility.IOnline _Online;
	// Use this for initialization
	void Start () {

        Client.InitialDoneEvent += _Client_InitialDoneEvent;        
	}

    void _Client_InitialDoneEvent()
    {
        
        Client.User.AccountStatusProvider.Supply += AccountStatusProvider_Supply;
        Client.User.Remoting.OnlineProvider.Supply += OnlineProvider_Supply;
    }

    void OnlineProvider_Supply(Regulus.Utility.IOnline obj)
    {
        if (_Online != null)
            _Online.DisconnectEvent -= _Reset;
        _Online = obj;
        obj.DisconnectEvent += _Reset;
    }

   

    void AccountStatusProvider_Supply(VGame.Project.FishHunter.IAccountStatus obj)
    {
        if (_AccountStatus != null)
        {            
            _AccountStatus.KickEvent -= _Reset;
        }
                
        _AccountStatus = obj;        
        _AccountStatus.KickEvent += _Reset;
    }

    private void _Reset()
    {
        _Release();
        Application.LoadLevel(ResetScene);
    }

    
    void OnDestroy()
    {
        _Release();
        Client.InitialDoneEvent -= _Client_InitialDoneEvent;
        Client.User.AccountStatusProvider.Supply -= AccountStatusProvider_Supply;
    }

    private void _Release()
    {
        if (_AccountStatus != null)
        {
            _AccountStatus.KickEvent -= _Reset;
        }
        if (_Online != null)
            _Online.DisconnectEvent -= _Reset;
    }

    

    
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
