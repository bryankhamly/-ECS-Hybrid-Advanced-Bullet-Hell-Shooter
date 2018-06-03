using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouRandomPattern : TouhouPattern
{
	[Header ("[Pattern]")]
	[Range (0f, 360f)]
	public float aimAngle = 180f;
	[Range (0f, 360f)]
	public float angleRange;

	[Header ("[Bullet]")]
	[Range (1, 25)]
	public float minSpeed;
	[Range (1, 25)]
	public float maxSpeed;
	[Range (0, 2)]
	public float minFireRate;
	[Range (0, 2)]
	public float maxFireRate;

	[Header ("[Spin]")]
	public bool spin;
	public float angleOffset;

	[Header ("[AutoAim]")]
	public bool autoAim;
	public Transform target;

	private void Start ()
	{
		target = transform.root.GetComponent<BossHealth> ().target;
	}

	public override void ShootBullet ()
	{
		if (target == null)
		{
			//target = transform.root.GetComponent<BossHealth>().target;
		}

		if (autoAim)
		{
			LookAtTarget ();
		}

		StartCoroutine (Shoot ());

		if (autoAim)
		{
			StartCoroutine (Aim ());
		}
	}

	IEnumerator Shoot ()
	{
		if (!available)
			yield break;

		available = false;

		if (spin)
		{
			for (int i = 0; i < bulletsToShoot; i++)
			{
				if (0 < i && 0f <= minFireRate && 0f < maxFireRate)
				{
					float randomFireRate = Random.Range (minFireRate, maxFireRate);
					yield return new WaitForSeconds (randomFireRate);
				}

				var bullet = CreateBullet (transform.position, transform.rotation);

				float bulletSpeedXD = Random.Range (minSpeed, maxSpeed);

				float midAngle = aimAngle + (angleOffset * i);
				float minAngle = midAngle - (angleRange / 2f);
				float maxAngle = midAngle + (angleRange / 2f);
				float angle = Random.Range (minAngle, maxAngle);

				InitBullet (this, bullet, angle, bulletSpeedXD, bulletAccel, bulletDamage, bulletStrafe);
			}
		}
		else
		{
			var bulletList = new List<int> ((int) bulletsToShoot);

			for (int i = 0; i < bulletsToShoot; i++)
			{
				bulletList.Add (i);
			}

			while (0 < bulletList.Count)
			{
				int index = Random.Range (0, bulletList.Count);
				var bullet = CreateBullet (transform.position, transform.rotation);

				float bulletSpeedXD = Random.Range (minSpeed, maxSpeed);

				float minAngle = aimAngle - (angleRange / 2f);
				float maxAngle = aimAngle + (angleRange / 2f);
				float angle = 0f;

				angle = Random.Range (minAngle, maxAngle);

				InitBullet (this, bullet, angle, bulletSpeedXD, bulletAccel, bulletDamage, bulletStrafe);

				bulletList.RemoveAt (index);

				if (0 < bulletList.Count && 0f <= minFireRate && 0f < maxFireRate)
				{
					float randomFireRate = Random.Range (minFireRate, maxFireRate);
					yield return new WaitForSeconds (randomFireRate);
				}
			}
		}

		available = true;
		yield break;

	}

	void LookAtTarget ()
	{
		if (target)
		{
			aimAngle = TrigStuff.CalculateZAngleDifference (transform.position, target.transform.position);
		}
	}

	IEnumerator Aim ()
	{
		while (autoAim)
		{
			if (available)
			{
				yield break;
			}

			LookAtTarget ();

			yield return 0;
		}
		yield break;
	}
}