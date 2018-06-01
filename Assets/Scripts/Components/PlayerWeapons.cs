using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	public Transform ShootPoint;
	public Transform Target;

	public Weapon defaultWeapon;

	public List<Weapon> weaponList;

	void Awake ()
	{
		weaponList.Add(defaultWeapon);
	}
}