using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Could have made Rotator its own class so this wouldn't be seperate from BossRotator.

public class Rotator : MonoBehaviour
{
	public float speed;

	void Start ()
	{

	}

	void Update ()
	{
		transform.Rotate (0, 0, speed * Time.deltaTime);
	}
}