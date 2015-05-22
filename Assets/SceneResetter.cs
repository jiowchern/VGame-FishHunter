using UnityEngine;
using System.Collections;

public class SceneResetter : MonoBehaviour 
{

    public string ResetScene;
    public Client Client;
    VGame.Project.FishHunter.IAccountStatus _AccountStatus;
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
        Application.LoadLevel(ResetScene);
    }

    
    void OnDestroy()
    {
        Client.InitialDoneEvent -= _Client_InitialDoneEvent;
        Client.User.AccountStatusProvider.Supply -= AccountStatusProvider_Supply;
    }

    

    
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
