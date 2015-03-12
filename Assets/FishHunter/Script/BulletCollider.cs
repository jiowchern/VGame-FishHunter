using UnityEngine;
using System.Collections;
using VGame.Extension;
public class BulletCollider : MonoBehaviour 
{

    public Vector2 Direction;

    public PolygonCollider2D Collider;

    public Renderer ViewChecker;
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = ((Vector3)Direction * UnityEngine.Time.deltaTime  )+ transform.position;
        //transform.Translate(Direction * UnityEngine.Time.deltaTime);
        

        VGame.Project.FishHunter.FishBounds[] fishs = VGame.Project.FishHunter.FishSet.Find(CameraHelper.Front , Collider.bounds);

        if(fishs.Length > 0)
        {
            

            bool hit = false;
            var bulletCollider= Collider.ToRegulusPolygon();
            foreach(var fish in fishs)
            {
                if (fish.IsHit(bulletCollider))
                {
                    hit = true;                    
                }
                    
            }

            if(hit)
                GameObject.Destroy(gameObject);
        }

        if(ViewChecker.isVisible == false)
            GameObject.Destroy(gameObject);




	}
}
