using UnityEngine;
using System.Collections;

public class DebugSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    
    public void LockFish()
    {
        if (VGame.Project.FishHunter.FishEnvironment.Instance != null)
            VGame.Project.FishHunter.FishEnvironment.Instance.Lock = !VGame.Project.FishHunter.FishEnvironment.Instance.Lock;
    }
}
