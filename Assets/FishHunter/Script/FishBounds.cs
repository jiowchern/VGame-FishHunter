using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VGame.Project.FishHunter.Common.Data;

namespace VGame.Project.FishHunter
{
    public class FishBounds :  Regulus.Collection.IQuadObject
    {
        int _Id;
        Regulus.CustomType.Rect _Bounds;
        public delegate bool AreaCallback(Regulus.CustomType.Polygon collider );
        public event AreaCallback RequestHitEvent;

        public bool Visible;
        public FishBounds(int id,FISH_TYPE type,Regulus.CustomType.Rect bounds )
        {
            Type = type;
            Visible = true;
            _Id = id;
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



        internal bool IsHit(Regulus.CustomType.Polygon collider)
        {

            if (Visible)
                return RequestHitEvent(collider);
            return false;
        }



        public int Id { get { return _Id; } }
        public FISH_TYPE Type { get; private set; }
    }
}
