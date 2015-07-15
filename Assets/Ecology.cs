using UnityEngine;
using System.Collections;
using System.Linq;

public class Ecology : MonoBehaviour {


    public PathMaker Maker;
    public FishSpawner Spawner;    
    Regulus.Utility.Updater _Fishs;
    public int Amount;

    public GameObject PathRoot;
	// Use this for initialization
	void Start () 
    {
        _Fishs = new Regulus.Utility.Updater();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(_NeedSpawn())
        {
            _Spawn();
        }

        _Fishs.Working();
	}

    private void _Spawn()
    {
        var path = _GetPath();
        var fish = _GetFish();
        _Build(fish, path);
        _AddFish(fish);
    }

    private void _Build(FishCollider fish, Vector3[] path)
    {
        var root = _Create();

        root.transform.position = path[PathMaker.END_INDEX];
        fish.transform.position = path[PathMaker.BEGIN_INDEX];                

        var deadTrigger = root.GetComponent<FishDeadTrigger>();
        deadTrigger.Fish = fish;

        var steering = fish.GetComponent<UnitySteer.Behaviors.SteerForPathSimplified>();
        
        steering.Path = new UnitySteer.SplinePathway(path.ToList(), 1);
    }

    private GameObject _Create()
    {
        return GameObject.Instantiate(PathRoot);
    }

    
    private void _AddFish(FishCollider fish)
    {
        _Fishs.Add(new Assets.FishHunter.Script.Pure.FishDeadReleaser(fish));
    }

    private FishCollider _GetFish()
    {
        return Spawner.Spawn();
    }

    private Vector3[] _GetPath()
    {
        return Maker.Maker();
    }

    private bool _NeedSpawn()
    {

        return _Fishs.Count < Amount;
    }
}
