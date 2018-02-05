using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody))] //requires attached object to have a rigidbody
public class PlayerController : MonoBehaviour 
{


	Vector3 velocity; //player velocity
	Rigidbody rb; //player rigidbody

	// Use this for initialization
	void Start () 
    {
		rb = GetComponent<Rigidbody>(); //assign rigidbody
	}

	public void FixedUpdate() 
    { 
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //move rigidbody position using velocity and time
	}

	public void Move(Vector3 _velocity)
    { //used to set velocity, recieves input from player
		velocity = _velocity;
	}

	public void LookAt(Vector3 lookPoint)
    { //turns player to look at mouse position, recieves input from player
		Vector3 playerHeightPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z); //corrects so that point is considered to be at the same height as the player
		transform.LookAt(playerHeightPoint);
	}

}
