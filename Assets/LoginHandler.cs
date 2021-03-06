﻿using UnityEngine;
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

    void OnDestroy()
    {
        _Machine.Termination();
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

        _ToDisconnect(); 
    }

    private void _ToDisconnect()
    {
        var stage = new DisconnectStage(_User.Remoting.OnlineProvider);

        stage.DoneEvent += _ToConnect;        

        _Machine.Push(stage);
    }

    private void _ToConnect()
    {

        var stage = new ConnectStage(_Ip, _Port, _User.Remoting.ConnectProvider);

        stage.SuccessEvent += _ToVerify;
        stage.FailEvent += _ToConnectFail;

        _Machine.Push(stage);
        
    }    

    private void _ToConnectFail()
    {
        //throw new System.Exception("_ToConnectFail");        

        //Debug.Log("_ToConnectFail");

        _Machine.Empty();
    }

    

    private void _ToVerify()
    {
        var stage = new VerifyStage(_Account, _Password, _User.VerifyProvider);

        stage.SuccessEvent += _ToLoadPlay;
        stage.FailEvent += _ToVerifyFail;

        var code = this.GetHashCode();
        _Machine.Push(stage);
    }

    private void _ToVerifyFail()
    {
        _Machine.Empty();
    }

    private void _ToLoadPlay()
    {
        _Machine.Termination();
        Application.LoadLevel(NextScene);
    }
	
	// Update is called once per frame
	void Update () 
    {
        var code = this.GetHashCode();
        _Machine.Update();
	}
    
}
