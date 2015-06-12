using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PathBinder : MonoBehaviour 
{
    public Transform Path;
    public UnitySteer.Behaviors.SteerForPathSimplified[] Fishs;
	// Use this for initialization
	void Start () {
	    foreach(var fish in Fishs)
        {
            var pathPoints = PathFromRoot(Path);

            fish.Path = new UnitySteer.SplinePathway(pathPoints, 3);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   

    public static List<Vector3> PathFromRoot(Transform root)
    {
        var children = new List<Transform>();
        foreach (Transform t in root)
        {
            if (t != null)
            {
                children.Add(t);
            }
        }
        return children.OrderBy(t => int.Parse(t.gameObject.name)).Select(t => t.position).ToList();
    }
}
