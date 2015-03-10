using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class CameraHelper
    {
        public static UnityEngine.Camera Front 
        {
            get 
            {
                return UnityEngine.GameObject.FindGameObjectWithTag("CameraFront").GetComponent<UnityEngine.Camera>();
            }
        }

        public static UnityEngine.Camera Middle
        {
            get
            {
                return UnityEngine.GameObject.FindGameObjectWithTag("CameraMiddle").GetComponent<UnityEngine.Camera>();
            }
        }
    }

