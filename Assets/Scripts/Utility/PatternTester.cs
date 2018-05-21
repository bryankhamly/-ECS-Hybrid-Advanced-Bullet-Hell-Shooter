using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTester : MonoBehaviour
{
	public List<TouhouPattern> patterns;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			foreach (var item in patterns)
			{
				item.ShootBullet ();
			}
		}
	}
}