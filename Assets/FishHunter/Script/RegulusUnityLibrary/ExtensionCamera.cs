using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace VGame.Extension
{
    public static class ExtensionCamera
    {
        public static Regulus.CustomType.Rect ToRect(this Camera camera , Bounds bounds)
        {
            var points = bounds.GetVectors();

            var first = _GetScreenPoint(camera, points[0]);
            var rect = new Regulus.CustomType.Rect(first.x, first.y, 0, 0);
            for (int i = 1; i < points.Length; ++i)
            {
                var pt = points[i];
                var screen = _GetScreenPoint(camera, pt);

                if (screen.x < rect.Left)
                    rect.Left = screen.x;

                if (screen.y < rect.Top)
                    rect.Top = screen.y;

                if (screen.x > rect.Right)
                    rect.Right = screen.x;

                if (screen.y > rect.Bottom)
                    rect.Bottom = screen.y;
            }

            return rect;
        }

        private static Vector3 _GetScreenPoint(Camera camera, Vector3 boundPoint)
        {
            var vp = camera.WorldToViewportPoint(boundPoint);
            return new Vector3(Screen.width * vp.x, Screen.height * (1 - vp.y), 0);
        }
    }
}
