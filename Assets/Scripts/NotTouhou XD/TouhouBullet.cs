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

	private TouhouPattern pattern;

	public void Initialize (TouhouPattern pattern, float angle, float speed, float accel) //Too bad can't use a constructor on MonoBehaviours
	{
		this.pattern = pattern;
		this.angle = angle;
		this.speed = speed;
		this.acceleration = accel;
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	private void Update ()
	{
		if (!IsVisibleFromCamera ())
		{
			pattern.bulletsShot.Remove (this);
			ReturnToPool ();
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
}