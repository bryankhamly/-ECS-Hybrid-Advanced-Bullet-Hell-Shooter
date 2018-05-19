using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspired by Touhou and Bullet Heaven 2
//Bryan Khamly

public class TouhouBullet : PooledObject
{
	public float angle;
	public float speed;
	public float acceleration;
	public int damage;
	public float strafe;

	public bool explode;
	public float explodeTime;
	public float explodeTimer;

	private TouhouPattern pattern;
	private TouhouPattern explodePattern;

	PooledObject explosion;

	public void Initialize (TouhouPattern pattern, float angle, float speed, float accel, int damage, float strafe) //Too bad can't use a constructor on MonoBehaviours
	{
		this.pattern = pattern;
		this.angle = angle;
		this.speed = speed;
		this.acceleration = accel;
		this.damage = damage;
		this.strafe = strafe;

		if (typeof (TouhouKaboomer).IsAssignableFrom (pattern.GetType ()))
		{
			var dankPattern = (TouhouKaboomer) pattern;
			this.explodePattern = dankPattern.patternToExplode;
			this.explode = true;
			this.explodeTime = dankPattern.explosionDelay;
			this.explodeTimer = 0;
			if (dankPattern.explosionPrefab)
				this.explosion = dankPattern.explosionPrefab;
		}

		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	private void Update ()
	{
		if (explode)
		{
			explodeTimer += Time.deltaTime;
			if (explodeTime < explodeTimer)
			{
				//Explosion!
				GameObject explosionObject = new GameObject ();
				explosionObject.AddComponent (explodePattern.GetType ());
				TouhouPattern tp = explosionObject.GetComponent<TouhouPattern> ();
				tp = explodePattern;
				tp.gameObject.transform.position = transform.position;
				if (explosion)
				{
					var explosionXD = explosion.GetPooledInstance<PooledObject> ();
					explosionXD.transform.position = transform.position;
				}
				tp.ShootBullet ();
				Destroy(explosionObject, 1);
				ResetExplosion ();
				PoolCleanup ();
			}
		}

		if (!IsVisibleFromCamera ())
		{
			PoolCleanup ();
		}
	}

	bool IsVisibleFromCamera ()
	{
		bool visible;
		var rend = GetComponent<Renderer> ();
		if (rend.IsVisibleFrom (Camera.main))
		{
			visible = true;
		}
		else
		{
			visible = false;
		}
		return visible;
	}

	void ResetExplosion ()
	{
		explode = false;
		explodeTimer = 0;
	}

	public void PoolCleanup ()
	{
		pattern.bulletsShot.Remove (this);
		ReturnToPool ();
	}
}