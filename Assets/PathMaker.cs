using UnityEngine;
using System.Collections;

public class PathMaker : MonoBehaviour {

    public const int BEGIN_INDEX = 0;
    public const int MIDDLE_INDEX = 1;
    public const int END_INDEX = 2;
    public Transform[] EndPoints;
    public Transform MiddlePoint;

    
	// Use this for initialization
	public Vector3[] Maker()
    {
        Vector3 begin = _GetBegin();
        Vector3 middle = _GetMiddle();
        Vector3 end = _GetEnd();

        return new Vector3[] { begin, middle, end };
    }

    private Vector3 _GetEnd()
    {
        return _Random(_Random(EndPoints));
    }

    private Vector3 _GetMiddle()
    {
        return _Random(MiddlePoint);
    }

    private Vector3 _GetBegin()
    {
        return _Random(_Random(EndPoints));
    }

    private Vector3 _Random(Transform tran)
    {
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = tran.TransformPoint(rndPosWithin * .5f);
        return rndPosWithin;
    }

    private Transform _Random(Transform[] set)
    {
        var idx = Regulus.Utility.Random.Instance.NextInt(0, set.Length);
        return set[idx];
    }
}
