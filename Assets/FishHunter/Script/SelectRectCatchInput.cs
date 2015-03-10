using UnityEngine;
using System.Collections;

public class SelectRectCatchInput : MonoBehaviour {


    
    private Vector2? orgBoxPos ;
    private Vector2? endBoxPos ;
	
	void Start () {
	
	}
	
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && orgBoxPos.HasValue == false)
        {
            orgBoxPos = Input.mousePosition;            
        }        
        endBoxPos = Input.mousePosition;
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _Selection(orgBoxPos.Value, endBoxPos.Value);
            orgBoxPos = null;            
        }    
	}

    private void _Selection(Vector2 orgBoxPos, Vector2 endBoxPos)
    {

        var r =  new Regulus.CustomType.Rect(orgBoxPos.x, Screen.height - orgBoxPos.y, endBoxPos.x - orgBoxPos.x, -1 * ((Screen.height - orgBoxPos.y) - (Screen.height - endBoxPos.y)));
        var set = GameObject.FindObjectOfType<VGame.Project.FishHunter.FishSet>();
        var fishs = set.Query(r);

        if (fishs.Length > 0)
            Debug.Log("fishs.Length : " + fishs.Length);
    }
    public Texture selectTexture;
    void OnGUI()
    {
        if (orgBoxPos.HasValue )
        {
            GUI.DrawTexture(new Rect(orgBoxPos.Value.x, Screen.height - orgBoxPos.Value.y, endBoxPos.Value.x - orgBoxPos.Value.x, -1 * ((Screen.height - orgBoxPos.Value.y) - (Screen.height - endBoxPos.Value.y))), selectTexture); // -
        }
    }
}
