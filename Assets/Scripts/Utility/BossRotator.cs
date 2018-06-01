using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotator : MonoBehaviour
{
	public float speed;

	public bool move;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0, 0, speed * Time.deltaTime);

		if (move)
		{
			if (transform.localPosition.y <= 0)
			{
				transform.localPosition += new Vector3(0, 3 * Time.deltaTime, 0);
			}
			else
			{
				move = false;
				transform.localPosition = new Vector3(0, 0, 0);
				StartBoss ();
			}
		}
	}

	//Oh god why is this method here LOL
	public void StartBoss ()
	{
		GetComponentInParent<BossHealth> ().ActualStart ();
	}
}