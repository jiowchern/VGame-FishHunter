using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VGame.Extension
{
    public static class ExtensionBounds
    {
        public static UnityEngine.Vector3[] GetVectors(this UnityEngine.Bounds bounds)
        {
            var boundPoints = new Vector3[8];
            
            boundPoints[0] = bounds.min;
            boundPoints[1] = bounds.max;
            boundPoints[2] = new Vector3(boundPoints[0].x, boundPoints[0].y, boundPoints[1].z);
            boundPoints[3] = new Vector3(boundPoints[0].x, boundPoints[1].y, boundPoints[0].z);
            boundPoints[4] = new Vector3(boundPoints[1].x, boundPoints[0].y, boundPoints[0].z);
            boundPoints[5] = new Vector3(boundPoints[0].x, boundPoints[1].y, boundPoints[1].z);
            boundPoints[6] = new Vector3(boundPoints[1].x, boundPoints[0].y, boundPoints[1].z);
            boundPoints[7] = new Vector3(boundPoints[1].x, boundPoints[1].y, boundPoints[0].z);


            return boundPoints;
        }
    }
}
