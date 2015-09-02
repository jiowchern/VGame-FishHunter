using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Regulus.CustomType;

namespace VGame.Extension
{
    public static class ExtensionPolygonCollider2D
    {
        public static Regulus.CustomType.Polygon ToRegulusPolygon(this UnityEngine.PolygonCollider2D collider)
        {
            
            var len = collider.points.Length;
            List<Vector2> points = new List<Vector2>();
            for (int i = 0 ; i < len  ; ++i)
            {
                var point = collider.points[i];
                var worldPoint = collider.transform.TransformPoint(point);
                var connr =CameraHelper.Front.GetScreenPoint(worldPoint);
                
                points.Add( new Regulus.CustomType.Vector2(connr.x , connr.y));            
            }
            Regulus.CustomType.Polygon polygon = new Regulus.CustomType.Polygon(points.ToArray());
            
            return polygon;
        }
    }
}
