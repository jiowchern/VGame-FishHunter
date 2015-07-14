using System.Linq;
using UnityEngine;
using System.Collections;
using Assets;

public class GameObjectPool : MonoBehaviour 
{
    public static GameObjectPool Instance { get { return GameObject.FindObjectOfType<GameObjectPool>(); } }
    class Catche
    {
        GameObject _Prefab;
        System.Collections.Generic.List<GameObject> _Used;
        System.Collections.Generic.Queue<GameObject> _NotUsed;
        public Catche(GameObject prefab)
        {
            _Prefab = prefab;
            _Used = new System.Collections.Generic.List<GameObject>();
            _NotUsed = new System.Collections.Generic.Queue<GameObject>();
        }
        internal GameObject Instantiate()
        {


            GameObject ins ;
            if (_NotUsed.Count > 0)
                ins = _NotUsed.Dequeue();
            else
            {
                ins = _Create();                
            }
            _Used.Add(ins);                
            _Enable(ins);
            return ins;
        }

        private GameObject _Create()
        {
            var instance = GameObject.Instantiate(_Prefab);            
            return instance;
        }

        private static void _Enable(GameObject instance)
        {
            var componment = _GetCatcheObject(instance);
            componment.Enabled();
        }

        private static CatcheObject _GetCatcheObject(GameObject instance)
        {
            var componment = instance.GetComponent<CatcheObject>();
            if (componment == null)
                throw new System.Exception("The instance without CatcheObject.");
            return componment;
        }

        internal bool Recycle(GameObject instance)
        {
            var catche = (from c in _Used where c == instance select c).SingleOrDefault() ;
            if(catche != null)
            {
                _Disable(instance);
                _Used.Remove(instance);
                _NotUsed.Enqueue(instance);
                return true;
            }
            return false;
        }

        private static void _Disable(GameObject instance)
        {
            var catacjObject = _GetCatcheObject(instance);
            catacjObject.Disable();
        }

        
    }
    System.Collections.Generic.Dictionary<GameObject, Catche > _Pool;

    public GameObjectPool()
    {
        _Pool = new System.Collections.Generic.Dictionary<GameObject, Catche >();
    }
	// Use this for initialization
	void Start () 
    {
	    
	}

    void OnDestroy()
    {
        
    }


    public void Destroy(GameObject instance)
    {
        foreach (var catache in _Pool)
        {
            if (catache.Value.Recycle(instance))
                return;
        }
        GameObject.Destroy(instance);
        
    }
    
    public GameObject Instantiate(GameObject prefab)
    {
        Catche queue = _Query(prefab);
        return queue.Instantiate();        
    }

    private Catche _Query(GameObject prefab)
    {
        Catche catche;
        if(_Pool.TryGetValue(prefab, out catche))
        {
            return catche;
        }
        catche = new Catche(prefab);
        _Pool.Add(prefab, catche);
        return catche;
    }
    
	// Update is called once per frame
	void Update () {
	
	}
}
