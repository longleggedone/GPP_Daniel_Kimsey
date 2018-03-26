using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public float speed = 10;
    public float timer = 5;
	
	// Update is called once per frame
	void Update () 
    {
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
        timer -= Time.deltaTime;
        if (timer <= 0)
        { 
            Destroy(gameObject);
        }
	}

	public void SetSpeed(float newSpeed) 
    {
		speed = newSpeed;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
