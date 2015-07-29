using System;

using UnityEngine;
using System.Collections;

using VGame.Project.FishHunter;

public class UIMenuSwitcher : MonoBehaviour
{

    [Serializable]
    public struct Item
    {
        public GameObject Target;
        public FishEnvironment.EXCLUSIVE_FEATURE Feature;
    }

    
    public Item[] Items;
    

    private FishEnvironment _Environment;

    // Use this for initialization
	void Start () 
    {
        _Environment = VGame.Project.FishHunter.FishEnvironment.Instance;
        if (_Environment != null)
	    {
            _Environment.FeatureToggleEvent += _InstanceOnFeatureToggle;
	    }
	        
	}

    void OnDestroy()
    {
        if (_Environment)
            _Environment.FeatureToggleEvent -= _InstanceOnFeatureToggle;
    }

    private void _InstanceOnFeatureToggle(FishEnvironment.EXCLUSIVE_FEATURE current, FishEnvironment.EXCLUSIVE_FEATURE previous)
    {
        foreach(var i in Items)
        {
            _FeatureToggle(i.Target, current, i.Feature);        
        }
        
    }

    private void _FeatureToggle(GameObject ui, FishEnvironment.EXCLUSIVE_FEATURE current, FishEnvironment.EXCLUSIVE_FEATURE self)
    {
        ui.SetActive(current == self);
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void ToggleSetting()
    {
        VGame.Project.FishHunter.FishEnvironment.Instance.Toggle(FishEnvironment.EXCLUSIVE_FEATURE.SETTING);
    }

    public void ToggleContact()
    {
        VGame.Project.FishHunter.FishEnvironment.Instance.Toggle(FishEnvironment.EXCLUSIVE_FEATURE.CONTACT);
    }

    public void ToggleShop()
    {
        VGame.Project.FishHunter.FishEnvironment.Instance.Toggle(FishEnvironment.EXCLUSIVE_FEATURE.SHOP);
    }

    public void ToggleAltas()
    {
        VGame.Project.FishHunter.FishEnvironment.Instance.Toggle(FishEnvironment.EXCLUSIVE_FEATURE.ATLAS);
    }

    public void ToggleGame()
    {
        VGame.Project.FishHunter.FishEnvironment.Instance.Toggle(FishEnvironment.EXCLUSIVE_FEATURE.GAME);
    }
}
