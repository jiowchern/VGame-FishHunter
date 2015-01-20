using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour 
{
    public enum MODE
    {
        STANDALONG,REMOTING
    };
    public MODE Mode;
    public Console Console;

    VGame.Project.FishHunter.Center _Standalong;
    Regulus.Utility.Updater _Updater;
	public Client()
    {
        _Standalong = new VGame.Project.FishHunter.Center();
        _Updater = new Regulus.Utility.Updater();
    }
	void Start () {
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

        provider.Spawn("1");
        provider.Select("1");
    }
	
	
	// Update is called once per frame
	void Update () {
        _Updater.Update();
	}

    void OnDestroy()
    {
        _Updater.Shutdown();
    }
}
