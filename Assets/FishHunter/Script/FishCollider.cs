using UnityEngine;
using System.Collections;
using VGame.Extension;
public abstract class FishCollider : MonoBehaviour 
{        
    public Transform Root;
         
    VGame.Project.FishHunter.FishBounds _Bounds;
    public PolygonCollider2D Collider;
    protected abstract Bounds _GetBounds();

	public FishCollider()
    {

        
    }
	void Start () 
    {
        _Bounds = new VGame.Project.FishHunter.FishBounds(_UpdateBounds());
        var set = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishSet>();
        set.Add(_Bounds);

        _Bounds.RequestHitEvent += _Hit;
	}

    private bool _Hit(Collider2D collider)
    {
        if (collider.IsTouching(_GetCollider()))
        {
            transform.position = UnityEngine.Random.onUnitSphere * 10;
            return true;
        }
        return false;
    }

    private Collider2D _GetCollider()
    {
        
        var boxs = gameObject.GetComponentsInChildren<BoxCollider>();
        foreach(var box in boxs)
        {

        }

        return Collider;
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

    void OnDrawGizmos()
    {
        /*var bounds = _Bounds.Bounds;
        var p1 = CameraHelper.Middle.ScreenToWorldPoint(new Vector3(bounds.Left, Screen.height - bounds.Top , 1 ));
        var p2 = CameraHelper.Middle.ScreenToWorldPoint(new Vector3(bounds.Left, Screen.height - bounds.Bottom, 1));
        var p3 = CameraHelper.Middle.ScreenToWorldPoint(new Vector3(bounds.Right, Screen.height - bounds.Top, 1));
        var p4 = CameraHelper.Middle.ScreenToWorldPoint(new Vector3(bounds.Right, Screen.height - bounds.Bottom, 1));

        var prevColor = Gizmos.color;
        Gizmos.color = Color.black;
        
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p4);
        Gizmos.DrawLine(p4, p3);
        Gizmos.DrawLine(p3, p1);

        Gizmos.color = prevColor;*/
    }
}
