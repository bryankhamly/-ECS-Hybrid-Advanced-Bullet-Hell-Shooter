using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
	public float Points { get { return points; } set { points = value; pointsText.text = points.ToString (); } }
	public float points;
	public float newPoints;

	public TextMeshProUGUI pointsText;

	public void AddPoints (float value)
	{
		newPoints += value;
	}

	private void Update ()
	{
		if (Points <= newPoints)
		{
			Points = (int) (Points + (newPoints - Points) * (Time.deltaTime));
		}
	}
}