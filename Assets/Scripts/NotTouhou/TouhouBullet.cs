using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouBullet : PooledObject
{
	public float angle;
	public float speed;

	public void Initialize (float angle, float speed) //Too bad can't use a constructor on MonoBehaviours
	{
		this.angle = angle;
		this.speed = speed;
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	private void Update ()
	{
		transform.localPosition += transform.up.normalized * speed * Time.deltaTime;

		if (!IsVisibleFromCamera ())
		{
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