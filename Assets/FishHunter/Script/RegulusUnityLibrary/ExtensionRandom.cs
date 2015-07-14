using UnityEngine;
using System.Collections;

public static class RandomExtension
{
    public static Vector3 AreaPosition(Regulus.Utility.IRandom rnd , Transform area)
    {
        Vector3 rndPosWithin = new Vector3(rnd.NextFloat(-1f, 1f), rnd.NextFloat(-1f, 1f), rnd.NextFloat(-1f, 1f));
        rndPosWithin = area.TransformPoint(rndPosWithin * .5f);
        return rndPosWithin;
    }
}
