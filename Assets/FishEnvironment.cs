using UnityEngine;
using System.Linq;

namespace VGame.Project.FishHunter
{
    public class FishEnvironment : MonoBehaviour
    {
        public bool Lock;

        public static FishEnvironment Instance { get
        {
            return Object.FindObjectOfType<FishEnvironment>();
        } }
        public int _Selected;
        public int Selected { get
        {
            return Lock
                       ? _Selected
                       : 0;
        }}

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var touchPosition = Input.mousePosition;
                var ray = CameraHelper.Middle.ScreenPointToRay(touchPosition);

                RaycastHit hitInfo;
                var fish = (from r in Physics.RaycastAll(ray)
                           let collider = r.collider.GetComponent<FishCollider>()
                           where collider != null && collider.Id != 0
                           select collider.Id).FirstOrDefault();

                if (fish != 0)
                    _Selected = fish;
            }
            
        }




    }

}
