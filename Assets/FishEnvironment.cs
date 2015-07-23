using UnityEngine;
using System.Linq;
using System.Collections;
namespace VGame.Project.FishHunter
{
    public class FishEnvironment : MonoBehaviour
    {
        public FishSet Set;

        public int DebugId;
        FishBounds _Selected;
        public FishBounds Selected { get { return _Selected; } }

        void OnDestroy()
        {
            
            Set.LeaveEvent -= _OnLeave;
        }

        
        // Use this for initialization
        void Start()
        {
            
            Set.LeaveEvent += _OnLeave;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var touchPosition = Input.mousePosition;
                var fishs = Set.Query(new Regulus.CustomType.Rect(touchPosition.x, touchPosition.y, touchPosition.x + 1, touchPosition.y + 1));
                var fish = fishs.FirstOrDefault();

                if (fish != null)
                {
                    _Selected = fish;
                    DebugId = fish.Id;
                }
                                
                
            }
            
        }

        private void _OnLeave(FishBounds fish)
        {
            if(_Selected == fish)
            {
                _Selected = null;
                DebugId = 0;
            }
        }


    }

}
