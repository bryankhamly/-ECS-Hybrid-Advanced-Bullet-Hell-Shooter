using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : MonoBehaviour
{
	public TouhouPattern special;

	public float cooldown;
	public float timer;

	public Color readyColor;
	public Color cdColor;

	public Image statusBar;

	// Use this for initialization
	void Start ()
	{
		timer = cooldown;
		statusBar.color = readyColor;
	}

	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Space) && timer >= cooldown)
		{
			special.ShootBullet ();
			timer = 0;
			statusBar.color = cdColor;
			StartCoroutine(LerpColor());
		}
	}

	IEnumerator LerpColor ()
	{
		float progress = 0;
		float increment = 0.02f / cooldown;
		while (progress < 1)
		{
			Debug.Log(progress);
			statusBar.color = Color.Lerp (cdColor, readyColor, progress);
			progress += increment;
			yield return new WaitForSeconds (increment);
		}
	}
}