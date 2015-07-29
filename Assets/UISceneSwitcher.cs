using UnityEngine;
using System.Collections;

public class UISceneSwitcher : MonoBehaviour 
{

    public string Name;
	

    public void Load()
    {
        Application.LoadLevel(Name);
    }
}
