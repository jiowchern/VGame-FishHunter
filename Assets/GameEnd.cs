using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour {
    private Client _Client;
    public string SelectLevelScene;


    void OnDestroy()
    {
        _Client.User.LevelSelectorProvider.Supply -= _ToSelect;
    }
	// Use this for initialization
	void Start () {
	    Client.Instance.User.LevelSelectorProvider.Supply += _ToSelect   ;
        _Client = Client.Instance;
	}

    private void _ToSelect(VGame.Project.FishHunter.Common.GPI.ILevelSelector obj)
    {
        Application.LoadLevel(SelectLevelScene);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
