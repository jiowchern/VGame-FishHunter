using UnityEngine;
using System.Collections;

public class BulletCollider : MonoBehaviour 
{

    public Vector3 Direction;

    public PolygonCollider2D Collider;

    public Renderer ViewChecker;
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {

        transform.Translate(Direction * UnityEngine.Time.deltaTime );

        VGame.Project.FishHunter.FishBounds[] fishs = VGame.Project.FishHunter.FishSet.Find(CameraHelper.Front , Collider.bounds);

        if(fishs.Length > 0)
        {
            Debug.Log("hit fish count:" + fishs.Length);

            bool hit = false;
            foreach(var fish in fishs)
            {
                if(fish.IsHit(Collider))
                    hit = true;
            }

            if(hit)
                GameObject.Destroy(gameObject);
        }

        if(ViewChecker.isVisible == false)
            GameObject.Destroy(gameObject);




	}
}
