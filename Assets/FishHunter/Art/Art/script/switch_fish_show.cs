using UnityEngine;
using System.Collections;

public class switch_fish_show : MonoBehaviour {
	public GameObject fish1;
	public GameObject fish2;
	public GameObject fish3;
	public GameObject fish4;
	public GameObject fish5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space")) {
			if(fish1.activeSelf == true){
				fish1.SetActive(false);
				fish2.SetActive(true);
			}
			else if(fish2.activeSelf == true){
				fish2.SetActive (false);
				fish3.SetActive(true);
			}
			else if(fish3.activeSelf == true){
				fish3.SetActive (false);
				fish4.SetActive (true);
			}
			else if(fish4.activeSelf == true){
				fish4.SetActive (false);
				fish5.SetActive (true);
			}
			else if(fish5.activeSelf == true){
				fish5.SetActive (false);
				fish1.SetActive (true);
			}
		}
	}
}
