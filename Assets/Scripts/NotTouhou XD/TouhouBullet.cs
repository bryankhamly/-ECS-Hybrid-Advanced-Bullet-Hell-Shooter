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

	public bool follow;

	private TouhouPattern pattern;
	private TouhouPattern explodePattern;
	private TouhouPattern followPattern;

	GameObject childPattern;

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
			this.follow = dankPattern.follow;
			if (dankPattern.explosionPrefab)
				this.explosion = dankPattern.explosionPrefab;
		}

		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	private void Update ()
	{
		if (followPattern)
		{
			if (followPattern.available == true)
			{
				Destroy (childPattern);
				PoolCleanup ();
			}
		}

		if (explode)
		{
			explodeTimer += Time.deltaTime;
			if (explodeTime < explodeTimer)
			{
				//Explosion!
				GameObject explosionObject = Instantiate (explodePattern.gameObject, transform.position, Quaternion.identity);
				childPattern = explosionObject;
				//explosionObject.AddComponent (explodePattern.GetType ());
				TouhouPattern tp = explosionObject.GetComponent<TouhouPattern> ();

				if (follow)
				{
					tp.gameObject.transform.SetParent (transform);
					tp.gameObject.transform.localPosition = Vector2.zero;
				}
				else
				{
					tp.gameObject.transform.position = transform.position;
				}

				if (explosion)
				{
					var explosionXD = explosion.GetPooledInstance<PooledObject> ();
					explosionXD.transform.position = transform.position;
				}

				tp.ShootBullet ();

				if(!follow)
				{
					tp.destroyOnDone = true;
				}
				//Destroy (explosionObject, 1);

				ResetExplosion ();

				if (follow)
				{
					followPattern = tp;
				}
				else
				{
					PoolCleanup ();
				}
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
		if (followPattern)
			Destroy (childPattern);
		pattern.bulletsShot.Remove (this);
		ReturnToPool ();
	}
}