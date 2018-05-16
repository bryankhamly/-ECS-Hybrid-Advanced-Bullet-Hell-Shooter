using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Waypoint myWaypoint;
	public float speed;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		/* 
		var startPos = transform.position;
		var endPos = wayPoint.position;
		var wtf = startPos - endPos;
		var hmm = Mathf.Atan2 (wtf.y, wtf.x) * Mathf.Rad2Deg;

		if (Vector2.Distance (transform.position, endPos) > 0.25f)
			transform.position += new Vector3 (Mathf.Cos (hmm) * Time.deltaTime, Mathf.Sin (hmm) * Time.deltaTime, 0);
		*/
	}

}