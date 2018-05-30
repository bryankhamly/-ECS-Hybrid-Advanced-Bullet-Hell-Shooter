using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	public Transform ShootPoint;
	public Transform Target;

	public Weapon defaultWeapon;

	[HideInInspector]
	public List<Weapon> weaponList;

	public List<Weapon> otherWeapons;

	void Awake ()
	{
		weaponList.Add(defaultWeapon);
		foreach (var weapon in otherWeapons)
		{
			weaponList.Add(weapon);
		}
	}
}