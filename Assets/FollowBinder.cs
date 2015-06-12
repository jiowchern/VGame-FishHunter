using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public class FollowBinder : MonoBehaviour 
{
    public Transform Path;
    public UnitySteer.Behaviors.SteerForPathSimplified Target;
    public UnitySteer.Behaviors.SteerToFollow[] Follwers;
	// Use this for initialization
	void Start () {
	    foreach(var f in Follwers)
        {
            f.Target = Target.transform;
        }
        var pathPoints = PathFromRoot(Path);
        Target.Path = new UnitySteer.Vector3Pathway(pathPoints, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static List<Vector3> PathFromRoot(Transform root)
    {
        var children = new List<Transform>();
        foreach (Transform t in root)
        {
            if (t != null)
            {
                children.Add(t);
            }
        }
        return children.OrderBy(t => t.gameObject.name).Select(t => t.position).ToList();
    }
}
