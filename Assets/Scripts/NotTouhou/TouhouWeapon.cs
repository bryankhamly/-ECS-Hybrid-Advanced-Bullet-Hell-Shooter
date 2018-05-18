using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouWeapon : MonoBehaviour
{
	public int patternIndex = 0;
	public List<TouhouPattern> weaponPatterns;

	IEnumerator ShootPattern ()
	{
		weaponPatterns[patternIndex].ShootBullet ();
		yield break;
	}

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			StartCoroutine (ShootPattern ());
		}
	}
}