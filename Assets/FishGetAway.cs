using UnityEngine;
using System.Collections;
using System.Linq;

public class FishGetAway : MonoBehaviour {


    public PathMaker Maker;
    public GameObject DeadPoint;

    public float Speed;
	// Use this for initialization
	void Start () {
	
	}


    void OnEnable()
    {
        var fishObjects = GameObject.FindGameObjectsWithTag("Fish");

        var fishs = from f in fishObjects
                    let fishCollider = f.GetComponent<FishCollider>()
                    let fishPath = f.GetComponent<UnitySteer.Behaviors.SteerForPathSimplified>()
                    let fishPoint = f.GetComponent<Transform>().position
                    let fishBip = f.GetComponent<UnitySteer.Behaviors.Biped>()
                    select new { Collider = fishCollider, Path = fishPath , Point = fishPoint , Bip = fishBip};

        foreach(var fish in fishs)
        {
            var points = Maker.Maker();
            var endPoint = points[PathMaker.END_INDEX];
            var startPoint = fish.Point;

            fish.Path.Path = new UnitySteer.SplinePathway(new Vector3[] { startPoint  , endPoint}.ToList() , 1);

            fish.Bip.MaxSpeed *= Speed;

            var deadObject = GameObject.Instantiate(DeadPoint);
            var trigger = deadObject.GetComponent<FishDeadTrigger>();
            trigger.Fish = fish.Collider;
            deadObject.transform.position = points[PathMaker.END_INDEX];
        }



        
    }
	// Update is called once per frame
	void Update () {	    
	}
}
