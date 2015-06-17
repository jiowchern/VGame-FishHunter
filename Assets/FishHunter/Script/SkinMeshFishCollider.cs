using UnityEngine;
using System.Collections;

public class SkinMeshFishCollider : FishCollider
{

    public SkinnedMeshRenderer SkinnedMesh;
    public Material Original;
    public Material Hit;

        
    protected override Bounds _GetBounds()
    {
        
        return SkinnedMesh.bounds;
    }

    protected override void _ChangeMaterial()
    {
        if(Hit != null)
            this.SkinnedMesh.sharedMaterial = Hit;
    }
}
