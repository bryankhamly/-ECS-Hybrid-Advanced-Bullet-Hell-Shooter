using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouBullet : PooledObject
{
	public float angle;
	public float speed;

	private TouhouPattern pattern;

	public void Initialize (TouhouPattern pattern, float angle, float speed) //Too bad can't use a constructor on MonoBehaviours
	{
		this.pattern = pattern;
		this.angle = angle;
		this.speed = speed;
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