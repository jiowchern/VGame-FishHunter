using UnityEngine;
using System.Collections;

public class KickHandler : MonoBehaviour {
    private Client _Client;

    public string LoginScene;
    private VGame.Project.FishHunter.IAccountStatus _AccountStatus;


	// Use this for initialization
	void Start () {
        _Client = Client.Instance;
        if (_Client != null)
        {
            _Client.User.AccountStatusProvider.Supply += AccountStatusProvider_Supply;
            _Client.User.Remoting.ConnectProvider.Supply += ConnectProvider_Supply;
        }
        
	}

    void ConnectProvider_Supply(Regulus.Utility.IConnect obj)
    {
        Application.LoadLevel(LoginScene);
    }

    void AccountStatusProvider_Supply(VGame.Project.FishHunter.IAccountStatus obj)
    {
        _AccountStatus = obj;
        obj.KickEvent += obj_KickEvent;
    }

    void obj_KickEvent()
    {
        Application.LoadLevel(LoginScene);
    }
	
    void OnDestroy()
    {
        if (_AccountStatus != null)
            _AccountStatus.KickEvent -= obj_KickEvent;

        if (_Client != null)
        {
            _Client.User.AccountStatusProvider.Supply -= AccountStatusProvider_Supply;
            _Client.User.Remoting.ConnectProvider.Supply -= ConnectProvider_Supply;
        }
            
    }
	// Update is called once per frame
	void Update () {
	
	}
}
