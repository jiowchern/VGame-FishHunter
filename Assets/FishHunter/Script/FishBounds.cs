using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGame.Project.FishHunter
{
    public class FishBounds :  Regulus.Collection.IQuadObject
    {
        
        Regulus.CustomType.Rect _Bounds;
        public delegate bool AreaCallback(UnityEngine.Collider2D collider );
        public event AreaCallback RequestHitEvent;
        public FishBounds(Regulus.CustomType.Rect bounds)
        {
            SetBounds(bounds);
        }
        Regulus.CustomType.Rect Regulus.Collection.IQuadObject.Bounds
        {
            get { return _Bounds; }
        }

        public Regulus.CustomType.Rect Bounds
        {
            get { return _Bounds; }
        }

        event EventHandler _BoundsChanged;
        event EventHandler Regulus.Collection.IQuadObject.BoundsChanged
        {
            add { _BoundsChanged += value; }
            remove { _BoundsChanged -= value; }
        }

        public void SetBounds(Regulus.CustomType.Rect bounds)
        {

            if (Regulus.Utility.ValueHelper.DeepEqual(_Bounds, bounds) == false)
            {
                _Bounds = bounds;
                if (_BoundsChanged != null)
                    _BoundsChanged(this, new EventArgs());
            }
        }



        internal bool IsHit(UnityEngine.Collider2D collider)
        {
            return RequestHitEvent(collider);
        }


    }
}
