using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGame.Extension
{
    public static class ExtensionPolygonCollider2D
    {
        public static Regulus.CustomType.Polygon ToRegulusPolygon(this UnityEngine.PolygonCollider2D collider)
        {
            Regulus.CustomType.Polygon polygon = new Regulus.CustomType.Polygon();
            var len = collider.points.Length;
            for(int i = 0 ; i < len  ; ++i)
            {
                var point = collider.points[i];
                var worldPoint = collider.transform.TransformPoint(point);
                var connr =CameraHelper.Front.GetScreenPoint(worldPoint);
                polygon.Points.Add( new Regulus.CustomType.Vector2(connr.x , connr.y));            
            }

            polygon.BuildEdges();
            return polygon;
        }
    }
}
