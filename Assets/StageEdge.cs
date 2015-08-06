using UnityEngine;
using System.Collections;
using Regulus.Extension;


[RequireComponent(typeof(EdgeCollider2D))]
public class StageEdge : MonoBehaviour {
	private Camera _Camera;
	private EdgeCollider2D _Edge;

	// Use this for initialization
	void Start () {

		_Camera = CameraHelper.Front;

		if(_Camera == null)
			Debug.LogError("找不到2D攝影機.");

		if (_Camera.orthographic == false)
			Debug.LogError("攝影機必須為Orthographic.");


		_Edge = GetComponent<EdgeCollider2D>();

		_SetEdge();
	}

	private void _SetEdge()
	{
		if (_Camera.orthographic)
		{
			var sizeH = _Camera.orthographicSize;
			var sizeW = sizeH*_Camera.aspect;
			var center =(Vector2) (_Camera.transform.position - gameObject.transform.position  );

			

			var points = new Vector2[5];
			points[0] = new Vector2(center.x - sizeW, center.y - sizeH);
			points[1] = new Vector2(center.x + sizeW, center.y - sizeH);
			points[2] = new Vector2(center.x + sizeW, center.y + sizeH);
			points[3] = new Vector2(center.x - sizeW, center.y + sizeH);
			points[4] = new Vector2(center.x - sizeW, center.y - sizeH);

			_Edge.points = points;

		}
	}

	// Update is called once per frame
	void Update () 
	{
							
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.LogFormat("object name : {0}  , rig name {1} ", other.gameObject.name, other.attachedRigidbody.gameObject.name );
			
	}
}
