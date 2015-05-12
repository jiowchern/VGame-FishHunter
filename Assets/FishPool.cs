using UnityEngine;
using System.Collections;

public class FishPool : MonoBehaviour 
{
    public int Limit;
    int _CurrentCount;
    VGame.Project.FishHunter.IPlayer _Player;
    public GameObject Fish;
	// Use this for initialization
	void Start () 
    {
        if(Client.Instance != null)
        {
            Client.Instance.User.PlayerProvider.Supply += PlayerProvider_Supply;
        }
        
	}

    void OnDestroy()
    {
        if (Client.Instance != null)
            Client.Instance.User.PlayerProvider.Supply -= PlayerProvider_Supply;
    }

    void PlayerProvider_Supply(VGame.Project.FishHunter.IPlayer obj)
    {
        _Player = obj;
        _Check();   
    }

    private void _Check()
    {
        if(Limit > _CurrentCount )
        {
            var requestCount = Limit - _CurrentCount;
            for (int i = 0; i < requestCount; ++i)
            {
                _Player.RequestFish().OnValue += FishPool_OnValue;
            }

            _CurrentCount = Limit;
        }
        
    }

    void FishPool_OnValue(short id)
    {
        
        var fish = GameObjectPool.Instance.Instantiate(Fish);
        var collider = fish.GetComponent<FishCollider>();
        collider.Initial(id); 
        collider.DeadEvent += collider_DeadEvent;

        fish.transform.position = UnityEngine.Random.insideUnitSphere * 15;
    }

    void collider_DeadEvent()
    {
        _CurrentCount--;
        _Check();
    }
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
