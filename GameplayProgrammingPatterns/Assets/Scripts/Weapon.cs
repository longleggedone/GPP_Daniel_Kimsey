using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Transform firePoint; //where projectiles should fire from
	public Projectile projectile; //the projectile to be fired
	public float fireInterval = 100; //interval in ms between shots
	public float projVelocity = 30; //speed at which projectile will leave weapin

	float shotTimer;

	public void Shoot(){
		if(Time.time > shotTimer){
			shotTimer = Time.time + fireInterval / 1000;
			Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as Projectile; //creates projectile at correct position and rotation
			newProjectile.SetSpeed(projVelocity); //sets the projectile's speed
		}
	}

}
