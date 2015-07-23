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

        public delegate void FishCallback(FishBounds fish);

        public event FishCallback JoinEvent;
        public event FishCallback LeaveEvent;

        public FishSet ()
        {
            _Set = new Regulus.Collection.QuadTree<FishBounds>( new Regulus.CustomType.Size(2,2) , 1000);
        }
        public void Add(FishBounds fish)
        {
            
            _Set.Insert(fish);
            if (JoinEvent != null)
                JoinEvent(fish);
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

        internal void Remove(FishBounds fish)
        {
            _Set.Remove(fish);
            if (LeaveEvent != null)
                LeaveEvent(fish);   
        }

    }
}
