using UnityEngine;
using System.Collections;

public class MutiPathBinder : MonoBehaviour {


    [System.Serializable]
    public struct Target
    {
        public UnitySteer.Behaviors.SteerForPathSimplified Steer;
        public Transform Path;
    }

    
    public Target[] Targets;
	// Use this for initialization
	void Start () {
	    foreach(var tar in Targets)
        {
            var points = PathBinder.PathFromRoot(tar.Path);
            tar.Steer.Path = new UnitySteer.SplinePathway(points , 3);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
