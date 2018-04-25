using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {

	// Use this for initialization
	public Transform target;


	Vector3 offset;
	[SerializeField]
	float smooth = 4f;
	private Transform _trasform;

	void Awake()
	{
		_trasform = transform;
	}


	void Start () {
		offset = _trasform.position - target.position ;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camPosition = target.position +offset;
		_trasform.position = Vector3.Lerp(_trasform.position, camPosition, smooth);
	}
}
