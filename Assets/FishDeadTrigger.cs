using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class FishDeadTrigger : MonoBehaviour 
{

    public FishCollider Fish;
	// Use this for initialization
	void Start () {
        Fish.DeadEvent += Fish_DeadEvent;
	}

    void OnDestroy()
    {
        Fish.DeadEvent -= Fish_DeadEvent;
    }

    void Fish_DeadEvent()
    {
        Destroy(gameObject);        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        if (Fish == other.gameObject.GetComponent<FishCollider>())
        {
            Fish.Dead();
        }
        
    }
}
