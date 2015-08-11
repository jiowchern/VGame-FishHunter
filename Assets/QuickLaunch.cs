using UnityEngine;
using System.Collections;
using VGame.Project.FishHunter.Common.GPI;

public class QuickLaunch : MonoBehaviour 
{
	public Client Target;
	public string Account;
	public string Password;
	public string IPAddress;
	public int Port;


	Regulus.Utility.StageMachine _Machine;
	public  string PlayScene;

	public QuickLaunch()
	{
		_Machine = new Regulus.Utility.StageMachine();
	}

	void OnDestroy()
	{
		Target.InitialDoneEvent -= Target_InitialDoneEvent;
	}
	// Use this for initialization
	void Start () {
		Target.InitialDoneEvent += Target_InitialDoneEvent;
		Target.Initial();
	}

	void Target_InitialDoneEvent()
	{
		_ToVerify();
	}

	private void _ToVerify()
	{
		var stage = new Assets.ConnectStage(IPAddress, Port, Target.User.Remoting.ConnectProvider);
		stage.SuccessEvent += _ConnectSuccess;
		_Machine.Push(stage);
	}

	void _ConnectSuccess()
	{
		var stage = new Assets.VerifyStage(Account, Password, Target.User.VerifyProvider);
		stage.SuccessEvent += _VerifySuccess;
		_Machine.Push(stage);
	}

	private void _VerifySuccess()
	{
		Target.User.LevelSelectorProvider.Supply += _SetSelector; 
	}

	private void _SetSelector(ILevelSelector obj)
	{
		Target.User.LevelSelectorProvider.Supply -= _SetSelector; 
		obj.Select(111).OnValue += _LoadScene ;
	}

	private void _LoadScene(bool obj)
	{
		Application.LoadLevel(PlayScene);

	}

	// Update is called once per frame
	void Update () {
		_Machine.Update();
	}
}
