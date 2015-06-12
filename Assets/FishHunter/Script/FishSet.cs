using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGame.Project.FishHunter
{
    using VGame.Extension;
    public class FishSet : UnityEngine.MonoBehaviour
    {
        Regulus.Collection.QuadTree<FishBounds> _Set;

        public FishSet ()
        {
            _Set = new Regulus.Collection.QuadTree<FishBounds>( new Regulus.CustomType.Size(2,2) , 1000);
        }
        public void Add(FishBounds fishOutline)
        {
            _Set.Insert(fishOutline);            
        }

        public FishBounds[] Query(Regulus.CustomType.Rect rect)
        {
            return _Set.Query(rect).ToArray();
        }

        internal static FishBounds[] Find(UnityEngine.Camera camera , UnityEngine.Bounds bounds)
        {
            var set = UnityEngine.GameObject.FindObjectOfType<FishSet>();
            return set.Query(camera.ToRect(bounds));
        }

        internal void Remove(FishBounds _Bounds)
        {
            _Set.Remove(_Bounds);
        }
    }
}
