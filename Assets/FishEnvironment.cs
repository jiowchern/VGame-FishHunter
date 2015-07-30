using UnityEngine;
using System.Linq;

namespace VGame.Project.FishHunter
{
    using System;
    using System.Collections.Generic;

    using Regulus.Extension;

    using Object = UnityEngine.Object;

    public class FishEnvironment : MonoBehaviour
    {
        public static FishEnvironment Instance
        {
            get
            {
                return Object.FindObjectOfType<FishEnvironment>();
            }
        }

        public enum EXCLUSIVE_FEATURE
        {
            GAME,
            SETTING,
            CONTACT,
            ATLAS,
            SHOP
        };


        private EXCLUSIVE_FEATURE _Current;

        
        public bool Lock;


        
        public int _Selected;
        public int Selected { get
        {
            return Lock
                       ? _Selected
                       : 0;
        }}

        public  delegate void TouchCallback();

        public event TouchCallback TouchEvent;


        void OnDestroy()
        {
            
        }
        void Start()
        {
            
            
        }

        

        // Update is called once per frame
        void Update()
        {
            if (! Input.GetMouseButton((0)))
                return;

            var touchPosition = Input.mousePosition;

            if (UICamera.Raycast(touchPosition))
                return;

            if ( _InGame() )
            {
                
                var ray = CameraHelper.Middle.ScreenPointToRay(touchPosition);

                RaycastHit hitInfo;
                var fish = (from r in Physics.RaycastAll(ray)
                           let collider = r.collider.GetComponent<FishCollider>()
                           where collider != null && collider.Id != 0
                           select collider.Id).FirstOrDefault();

                if (fish != 0)
                    _Selected = fish;

                if (TouchEvent != null)
                    TouchEvent();

            }
            
        }

        private bool _InGame()
        {
            return _Current == EXCLUSIVE_FEATURE.GAME;
        }

        public void Toggle(EXCLUSIVE_FEATURE feature)
        {
            var newFeature = EXCLUSIVE_FEATURE.GAME;
            if ( !(feature == _Current && !_InGame()) )
            {
                newFeature = feature;
            }            


            if (newFeature != _Current && FeatureToggleEvent != null)
                FeatureToggleEvent(newFeature, _Current);

            _Current = newFeature ;
        }

        public delegate void FeatureToggleCallabck(EXCLUSIVE_FEATURE current , EXCLUSIVE_FEATURE previous);

        public event FeatureToggleCallabck FeatureToggleEvent;
    }

}
