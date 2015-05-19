﻿using System.Linq;
using UnityEngine;
using System.Collections;
using VGame.Extension;
public abstract class FishCollider : MonoBehaviour 
{        
    public Transform Root;
         
    VGame.Project.FishHunter.FishBounds _Bounds;

    
    Regulus.CustomType.Polygon _Polygon;
    
    protected abstract Bounds _GetBounds();
    
    int _Id;
    private VGame.Project.FishHunter.IPlayer _Player;
    public delegate void DeadCallback();
    public event DeadCallback DeadEvent;
    private Client _Client;


	public FishCollider()
    {        
        _Polygon = new Regulus.CustomType.Polygon();
        _Polygon.BuildEdges();
     
    }

	void Start () 
    {
        
	}

    public void Initial(int id)
    {
        _Id = id;
        DeadEvent = null;

        _Bounds = new VGame.Project.FishHunter.FishBounds(_Id, _UpdateBounds());
        var set = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishSet>();
        set.Add(_Bounds);
        _Bounds.RequestHitEvent += _Hit;
        Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
        _Client = Client.Instance;
    }
    void Release()
    {
        _Bounds.RequestHitEvent -= _Hit;
        var set = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishSet>();
        if (set != null)
            set.Remove(_Bounds);        
            _Client.User.PlayerProvider.Supply -= PlayerProvider_Supply;
        _Player.DeathFishEvent -= _Player_DeathFishEvent;

        _Id = 0;
    }
    void OnDestroy()
    {
        if (_Id != 0)
            Release();
    }
    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
        _Player.DeathFishEvent += _Player_DeathFishEvent;
    }

    void _Player_DeathFishEvent(int obj)
    {
        if (obj == _Id)
            _Dead();
    }

    private void _Dead()
    {
        gameObject.GetComponent<FishCollider>().Release();
        GameObjectPool.Instance.Destroy(gameObject);        
        
        if (DeadEvent != null)
            DeadEvent();
    }

    private bool _Hit(Regulus.CustomType.Polygon collider)
    {
        var fishPolygon  = _GetCollider();
        var result = Regulus.CustomType.Polygon.Collision(collider, fishPolygon , new Regulus.CustomType.Vector2(0,0));
        if (result.Intersect || result.WillIntersect)
        {            
            return true;
        }
        return false;
    }

    private Regulus.CustomType.Polygon _GetCollider()
    {
        
        var boxs = gameObject.GetComponentsInChildren<BoxCollider>();
        Vector2[] points = _GetVectors(boxs);
        var paths = AForge.Math.Geometry.GrahamConvexHull.FindHull(points.ToList()).ToArray();

        _SetPolygon(_Polygon, paths);
        return _Polygon;
    }

    private void _SetPolygon(Regulus.CustomType.Polygon polygon, Vector2[] paths)
    {
        polygon.Points.Clear();
        foreach(var p in paths)
        {
            polygon.Points.Add( new Regulus.CustomType.Vector2(p.x , p.y));
        }

        polygon.BuildEdges();
    }

    

    private Vector2[] _GetVectors(BoxCollider[] boxs)
    {
        System.Collections.Generic.List<Vector2> points = new System.Collections.Generic.List<Vector2>();
        foreach(var box in boxs)
        {
            var vectors = _GetVector(box);
            points.AddRange(vectors);
        }
        return points.ToArray();
    }
    private Vector2[] _GetVector(BoxCollider box)
    {

        return (from vec in box.bounds.GetVectors() select (Vector2)CameraHelper.Middle.GetScreenPoint( vec) ).ToArray();
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
        

        /*var bounds = _Bounds.Bounds;
        GUIHelper.DrawLine(new Vector2(bounds.Left, bounds.Top), new Vector2(bounds.Right, bounds.Top), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Left, bounds.Top), new Vector2(bounds.Left, bounds.Bottom), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Right, bounds.Bottom), new Vector2(bounds.Left, bounds.Bottom), Color.black);
        GUIHelper.DrawLine(new Vector2(bounds.Right, bounds.Bottom), new Vector2(bounds.Right, bounds.Top), Color.black);

        var length = _Polygon.Points.Count;
        for (int i = 0; i < length - 1; ++i)
        {
            var path1 = _Polygon.Points[i];
            var path2 = _Polygon.Points[i + 1];

            GUIHelper.DrawLine(new Vector2(path1.X, path1.Y), new Vector2(path2.X, path2.Y), Color.yellow);
        }

        if (length > 1)
        {
            var path1 = _Polygon.Points[length - 1];
            var path2 = _Polygon.Points[0];
            GUIHelper.DrawLine(new Vector2(path1.X, path1.Y), new Vector2(path2.X, path2.Y), Color.yellow);
        }*/
            
        
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
