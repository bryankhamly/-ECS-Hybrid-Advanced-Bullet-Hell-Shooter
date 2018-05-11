using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	public Transform ShootPoint;
	public GameObject Bullet;
	public float attackSpeed;
	public float timer;

	public int weaponIndex;
	public Weapon currentWeapon;
}