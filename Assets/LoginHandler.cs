using UnityEngine;
using System.Collections;
using Assets;

public class LoginHandler : MonoBehaviour {

    VGame.Project.FishHunter.IUser _User;
    Regulus.Utility.StageMachine _Machine;
    public UILogin UI;
    public string NextScene;
    private string _Ip;
    private int _Port;
    private string _Account;
    private string _Password;

    

    public LoginHandler()
    {
        _Machine = new Regulus.Utility.StageMachine();
    }
	// Use this for initialization
	void Start () {
        var client  = GameObject.FindObjectOfType<Client>();
        if (client != null)
        {
            _User = client.User;

            UI.VerifyEvent += _Start;

        }
	}

    private void _Start(string ip, int port, string account, string password)
    {
        _Ip = ip;
        _Port = port;
        _Account = account;
        _Password = password;

        _ToConnect();
    }

    private void _ToConnect()
    {
        if(_User.Remoting.OnlineProvider.Ghosts.Length > 0)
        {
            _ToVerify();
        }
        else 
        {
            var stage = new ConnectStage(_Ip, _Port, _User.Remoting.ConnectProvider);

            stage.SuccessEvent += _ToVerify;
            stage.FailEvent += _ToConnectFail;

            _Machine.Push(stage);
        }
        
    }

    private void _ToConnectFail()
    {
        //throw new System.Exception("_ToConnectFail");        

        Debug.Log("_ToConnectFail");
    }

    

    private void _ToVerify()
    {
        var stage = new VerifyStage(_Account, _Password, _User.VerifyProvider);

        stage.SuccessEvent += _ToLoadPlay;
        stage.FailEvent += _ToVerifyFail;

        _Machine.Push(stage);
    }

    private void _ToVerifyFail()
    {
        throw new System.Exception("_ToVerifyFail");        
    }

    private void _ToLoadPlay()
    {
        _Machine.Termination();
        Application.LoadLevel(NextScene);
    }
	
	// Update is called once per frame
	void Update () 
    {
        _Machine.Update();
	}
    
}
