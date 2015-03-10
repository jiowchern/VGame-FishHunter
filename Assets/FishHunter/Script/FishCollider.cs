using UnityEngine;
using System.Collections;
using VGame.Extension;
public abstract class FishCollider : MonoBehaviour 
{        
    public Transform Root;
         
    VGame.Project.FishHunter.FishBounds _Bounds;

    protected abstract Bounds _GetBounds();

	public FishCollider()
    {

        
    }
	void Start () 
    {
        _Bounds = new VGame.Project.FishHunter.FishBounds(_UpdateBounds());
        var set = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishSet>();
        set.Add(_Bounds);
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateBounds();
	    
	}

    private void UpdateBounds()
    {        
        if (Root.hasChanged)
        {
            var rect = _UpdateBounds();
            _Bounds.SetBounds(rect);

            Root.hasChanged = false;
        }
    }

    private Regulus.CustomType.Rect _UpdateBounds()
    {
        var camera = CameraHelper.Middle;
        var bounds = _GetBounds();
        return camera.ToRect(bounds);        
    }

    private static Vector3 _GetScreenPoint(Camera camera, Vector3 boundPoint)
    {
        var vp = camera.WorldToViewportPoint(boundPoint);
        return new Vector3(Screen.width * vp.x, Screen.height * (1 - vp.y), 0);
    }

    void OnGUI()
    {
        

        var bounds = _Bounds.Bounds;
        GUIHelper.DrawLine(new Vector2(bounds.Left, bounds.Top), new Vector2(bounds.Right, bounds.Top), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Left, bounds.Top), new Vector2(bounds.Left, bounds.Bottom), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Right, bounds.Bottom), new Vector2(bounds.Left, bounds.Bottom), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Right, bounds.Bottom), new Vector2(bounds.Right, bounds.Top), Color.black);
        
    }
}
