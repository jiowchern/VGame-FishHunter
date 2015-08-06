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


    public static T QueryComponment<T>(this UnityEngine.GameObject instnace) where T : Component
    {
        var componment = instnace.GetComponent<T>();
        return componment ?? instnace.AddComponent<T>();
    }
}
