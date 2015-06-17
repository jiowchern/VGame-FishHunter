using UnityEngine;
using System.Collections;

public class BoxFishCollider : FishCollider
{
    public Collider BoundsSource;
    

    protected override Bounds _GetBounds()
    {
        return BoundsSource.bounds;
    }

    protected override void _ChangeMaterial()
    {
        return;
    }
}
