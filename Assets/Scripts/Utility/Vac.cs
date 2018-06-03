using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vac : MonoBehaviour
{
	public bool ready;
	public Transform target;
	GameManager gm;
	public float speed;

	void Start ()
	{
		gm = FindObjectOfType<GameManager> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (ready)
		{
			Transform player = gm.player.transform;
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.position, step);
		}
	}
}