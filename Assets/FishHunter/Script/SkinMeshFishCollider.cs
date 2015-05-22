using UnityEngine;
using System.Collections;

public class SkinMeshFishCollider : FishCollider
{

    public SkinnedMeshRenderer SkinnedMesh;
    protected override Bounds _GetBounds()
    {
        
        return SkinnedMesh.bounds;
    }
}
