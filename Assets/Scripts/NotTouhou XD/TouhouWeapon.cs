using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspired by Touhou and Bullet Heaven 2
//Bryan Khamly

public class TouhouWeapon : MonoBehaviour
{
	public int patternIndex = 0;
	public List<TouhouPattern> weaponPatterns;

	IEnumerator ShootPattern ()
	{
		weaponPatterns[patternIndex].ShootBullet ();
		yield break;
	}
}