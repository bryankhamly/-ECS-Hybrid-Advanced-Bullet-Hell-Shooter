using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Running out of time, gunna hard code some upgrades.

public class Upgrades : MonoBehaviour
{
	public int powerLevel = 0;

	PlayerWeapons weapon;

	// Use this for initialization
	void Start ()
	{
		weapon = GetComponent<PlayerWeapons> ();
	}

	public void Upgrade ()
	{
		powerLevel++;

		switch (powerLevel)
		{
			case 1:
				weapon.weaponList[0].bulletsToShoot = 2;
				weapon.weaponList[0].angleSpread = 0.1f;
				break;
			case 2:
				weapon.weaponList[0].fireRate = 0.10f;
				weapon.weaponList[0].bulletsToShoot = 3;
				weapon.weaponList[0].angleSpread = 0.2f;
				break;
			case 3:
				weapon.weaponList[0].fireRate = 0.09f;
				weapon.weaponList[0].bulletsToShoot = 4;
				weapon.weaponList[0].angleSpread = 0.25f;
				break;
			case 4:
				weapon.weaponList[0].fireRate = 0.08f;
				weapon.weaponList[0].bulletsToShoot = 5;
				weapon.weaponList[0].angleSpread = 0.3f;
				break;
			case 5:
				weapon.weaponList[1].active = true;
				break;
			case 6:
				weapon.weaponList[1].fireRate = 0.7f;
				weapon.weaponList[1].bulletsToShoot = 4;
				break;
			case 7:
				weapon.weaponList[1].fireRate = 0.65f;
				weapon.weaponList[1].bulletsToShoot = 5;
				break;
			case 8:
				weapon.weaponList[2].active = true;
				break;
			case 9:
				weapon.weaponList[2].bulletsToShoot = 10;
				weapon.weaponList[2].fireRate = 0.7f;
				break;
			case 10:
				weapon.weaponList[2].bulletsToShoot = 12;
				weapon.weaponList[2].fireRate = 0.65f;
				break;
			case 11:
				weapon.weaponList[2].bulletsToShoot = 16;
				weapon.weaponList[2].fireRate = 0.62f;
				break;
			case 12:
				weapon.weaponList[0].bulletsToShoot = 6;
				weapon.weaponList[0].fireRate = 0.075f;
				weapon.weaponList[0].angleSpread = 0.6f;
				break;
			case 13:
				weapon.weaponList[1].fireRate = 0.62f;
				weapon.weaponList[1].bulletsToShoot = 8;
				break;
			default:
				break;
		}
	}
}