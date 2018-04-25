using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public GameObject CameraObject;

	[SerializeField]
	float moveSpeed = 4.0f;
	[SerializeField]
	float jumpForce = 100f;
	bool isJump;
	Vector3 forward, right;
	Transform _transfrom;
	Rigidbody rb;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_transfrom = transform;
		rb = GetComponent<Rigidbody>();
	}
	
	// Use this for initialization
	void Start () {

		Transform Camera = CameraObject.transform.Find("Camera");
		forward = Camera.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Jump") && !isJump)
		{
			StartCoroutine(Jump());
		}
		else if(Input.anyKey)
		{
			Move();
		}
	}

	void Move()
	{
		Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0 ,Input.GetAxis("VerticalKey"));
		Vector3 rightMovement = right*moveSpeed*Time.deltaTime*Input.GetAxis("HorizontalKey");
		Vector3 upMovement = forward*moveSpeed*Time.deltaTime*Input.GetAxis("VerticalKey");

		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		_transfrom.forward = heading;
		_transfrom.position += rightMovement;
		_transfrom.position += upMovement;
	}

	IEnumerator Jump()
	{
		Debug.Log("Ciao!");
		float originalYCharacter = _transfrom.position.y;
		isJump = true;

		rb.AddForce(Vector3.up *jumpForce, ForceMode.Impulse);

		while(_transfrom.position.y > originalYCharacter)
		{
			yield return null;
		}

		isJump = false;

		yield return null;
	}
}
