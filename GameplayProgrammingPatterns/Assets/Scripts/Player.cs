using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (PlayerController))] //forces object to have an attached player controller if one is not already present
[RequireComponent (typeof (WeaponController))]
public class Player : MonoBehaviour {

	public float moveSpeed = 3; //speed of player
	PlayerController controller; //the player controller
	WeaponController weaponController;

    public AudioClip deathAudio;

	Camera mainCam; //the primary view camera

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController>(); //assign controller
		weaponController = GetComponent<WeaponController>();
		mainCam = Camera.main; //assign camera
	}
	
	// Update is called once per frame
	void Update () {
		//MOVEMENT
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")); //our H and V input
		Vector3 moveVelocity = moveInput.normalized * moveSpeed; // velocity is our normalized input times our move speed
		controller.Move(moveVelocity); //send velocity to player controller


		//FACING
		Ray ray = mainCam.ScreenPointToRay (Input.mousePosition); //returns ray from camera through mouse position
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //create plane for intersection of ray
		float rayDist;

		if(groundPlane.Raycast(ray, out rayDist)){ //if the ray intersects the ground plane, return distance
			Vector3 point = ray.GetPoint(rayDist); //point of intersection 
			Debug.DrawLine(ray.origin, point, Color.red); //makes ray visible in scene view
			controller.LookAt(point);
		}

		//WEAPONS
		if (Input.GetMouseButton(0)){ //if we are pressing the left mouse button
			weaponController.Shoot();
		}

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //SoundManager.Instance.GenerateSourceAndPlay(deathAudio, 1f, 1f, transform.position);
            Destroy(this.gameObject);
        }
    }
}
