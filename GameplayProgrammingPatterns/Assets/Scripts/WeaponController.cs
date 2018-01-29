using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public Transform weaponPosition; //the position of the player's weapon
	Weapon equippedWeapon; //the weapon currently held
	public Weapon startWeapon;


	void Start(){
		if(startWeapon != null){ //if there is a starting weapon
			EquipWeapon(startWeapon); //equip that weapon
		}
	}

	public void EquipWeapon(Weapon weaponToEquip){ //used to equip new weapons, pass in the new weapon to equip
		if (equippedWeapon != null){ //if there is already a weapon equipped
			Destroy(equippedWeapon.gameObject); //destroy that weapon
		}
		equippedWeapon = Instantiate (weaponToEquip, weaponPosition.position, weaponPosition.rotation) as Weapon; //instantiate the new weapon at the correct position and rotation
		equippedWeapon.transform.parent = weaponPosition; //set weapons transform parent
	}

	public void Shoot(){
		if (equippedWeapon != null){
			equippedWeapon.Shoot();
		}
	}
}
