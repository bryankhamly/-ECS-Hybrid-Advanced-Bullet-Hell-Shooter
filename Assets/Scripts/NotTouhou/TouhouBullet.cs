using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouBullet : MonoBehaviour
{
	public float angle;
	public float speed;

	public void Initialize(float angle, float speed) //Too bad can't use a constructor on MonoBehaviours
	{
		this.angle = angle;
		this.speed = speed;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}
}