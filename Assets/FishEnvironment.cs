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


        private VGame.Project.FishHunter.Common.Data.FISH_TYPE _FishType;

        public VGame.Project.FishHunter.Common.Data.FISH_TYPE LockFishType
        {
            get
            {
                return Lock
                       ? _FishType
                       : VGame.Project.FishHunter.Common.Data.FISH_TYPE.ANGEL_FISH;
            }
        }

        public int _Selected;
        public int SelectedId { get
        {
            return Lock
                       ? _Selected
                       : 0;
        }}


        public delegate void RotationCallback(float calibration);

        public event RotationCallback RotationEvent;
        public  delegate void FireCallback();

        public event FireCallback TouchFireEvent;
        public event FireCallback ClickFireEvent;


        void OnDestroy()
        {
            
        }
        void Start()
        {
            
            
        }

        

        // Update is called once per frame
        void Update()
        {


            

            if ( _InGame() )
            {

                _UpdateSelectFromTouch();

                _UpdateKeyOnFire();                        

            }
            
        }

        private void _UpdateKeyOnFire()
        {
            var horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0 && RotationEvent != null)
                RotationEvent(-horizontal);
                
            
            var space = Input.GetKeyUp(KeyCode.Space);
            if (space && ClickFireEvent != null)
                ClickFireEvent();

        }

        private bool _UpdateSelectFromTouch()
        {
            if (!Input.GetMouseButton((0)))
                return true;

            var touchPosition = Input.mousePosition;

            if (UICamera.Raycast(touchPosition))
                return true;


            var ray = CameraHelper.Middle.ScreenPointToRay(touchPosition);

            RaycastHit hitInfo;
            var fish = (from r in Physics.RaycastAll(ray)
                let collider = r.collider.GetComponent<FishCollider>()
                where collider != null && collider.Id != 0
                select new {id = collider.Id, FishType = collider.FishType}).FirstOrDefault();

            if (fish != null && fish.id != 0)
            {
                _Selected = fish.id;
                _FishType = fish.FishType;
            }
            if (TouchFireEvent != null)
                TouchFireEvent();

            return false;
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
