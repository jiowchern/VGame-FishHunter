using UnityEngine;
using System.Collections;
using System.Linq;

public class SelectStageHandler : MonoBehaviour 
{
    
    public Stage[] Stages;
    VGame.Project.FishHunter.ILevelSelector _Selector;
    Client _Client;
 
    void OnDestroy()
    {
        if (_Client != null)
            _Client.User.LevelSelectorProvider.Supply -= LevelSelectorProvider_Supply;
    }
	// Use this for initialization
	void Start () {

        _AddDisable();
        _Client = Client.Instance;
        _Client.User.LevelSelectorProvider.Supply += LevelSelectorProvider_Supply;
	}

    private void _AddDisable()
    {
        foreach (var stage in Stages)
        {            
            stage.Disable();
        }
    }

    void LevelSelectorProvider_Supply(VGame.Project.FishHunter.ILevelSelector obj)
    {
        obj.QueryStages().OnValue += (stages) =>
        {
            SelectStageHandler_OnValue(stages);
            _Selector = obj;
        };
    }

    void SelectStageHandler_OnValue(int[] stages)
    {
        foreach( var stage in Stages)
        {
            if (stages.Any(s => stage.Id == s))
                stage.Enable();
            else
                stage.Disable();
            
        }

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Select(int stage)
    {
        _Selector.Select(stage).OnValue += (result) => 
        {
            if (result)
                Application.LoadLevel( _FindScene(stage ));
        };
    }

    private string _FindScene(int stage)
    {
        return (from s in Stages where s.Id == stage select s.Scene).Single();
    }

    

    
}
