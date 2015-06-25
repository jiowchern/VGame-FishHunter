using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour 
{
    public static Client Instance { get { return GameObject.FindObjectOfType<Client>(); } }
    public enum MODE
    {
        STANDALONG,REMOTING
    };
    public MODE Mode;
    public Console Console;

    
    Regulus.Utility.Updater _Updater;

    public VGame.Project.FishHunter.IUser User { get; private set; }

    public delegate void InitialDoneCallback();
    event InitialDoneCallback _InitialDoneEvent;
    public event InitialDoneCallback InitialDoneEvent 
    {
        add
        {
            if (User != null)
                value();

            _InitialDoneEvent += value;
        }

        remove
        {
            _InitialDoneEvent -= value;
        }
    }

    VGame.Project.FishHunter.Play.DummyStandalong _Standalong;
	public Client()
    {
        _Standalong = new VGame.Project.FishHunter.Play.DummyStandalong();
        _Updater = new Regulus.Utility.Updater();
    }
	void Start () {
        System.Reflection.Assembly.Load("Common");
        var client = new Regulus.Framework.Client<VGame.Project.FishHunter.IUser>(this.Console, this.Console);
        client.ModeSelectorEvent += _ModeSelector;
        _Updater.Add(client);
        _Updater.Add(_Standalong);
    }

    private void _ModeSelector(Regulus.Framework.GameModeSelector<VGame.Project.FishHunter.IUser> selector)
    {
        selector.AddFactoty("Standalong", new VGame.Project.FishHunter.StandalongUserFactory(_Standalong));
        selector.AddFactoty("Remoting", new VGame.Project.FishHunter.RemotingUserFactory());

        Regulus.Framework.UserProvider<VGame.Project.FishHunter.IUser> provider = null;
        if(this.Mode == Client.MODE.STANDALONG)
        {
            provider = selector.CreateUserProvider("Standalong");                
        }
        else if (this.Mode == Client.MODE.REMOTING)
        {
            provider = selector.CreateUserProvider("Remoting");                
        }

        User = provider.Spawn("1");
        provider.Select("1");
        if (_InitialDoneEvent != null)
            _InitialDoneEvent();
    }
	
	
	// Update is called once per frame
	void Update () {
        _Updater.Working();
	}

    void OnDestroy()
    {            
        _Updater.Shutdown();
    }
}
