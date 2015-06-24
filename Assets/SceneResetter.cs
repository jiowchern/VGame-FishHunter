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
        Client.User.Remoting.ConnectProvider.Supply += ConnectProvider_Supply;
        Client.User.Remoting.OnlineProvider.Supply += OnlineProvider_Supply;
        
    }

    void ConnectProvider_Supply(Regulus.Utility.IConnect obj)
    {
        _Reset();
    }

   
    void OnlineProvider_Supply(Regulus.Utility.IOnline obj)
    {
        
        _Online = obj;        
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
        
    }

    

    
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
