using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrigStuff
{
	public static float Radian2Degrees (float radian)
	{
		float degree;
		degree = radian * Mathf.Rad2Deg;
		return degree;
	}

	public static float CalculateComplementary (ref float angle)
	{
		angle += Mathf.PI * 2; //pi = 180 degrees half a circle
		return angle;
	}

	public static float CalculateCosinePos (float angle, float speed)
	{
		float x;
		x = Mathf.Cos (angle) * speed * Time.deltaTime;
		return x;
	}

	public static float CalculateSinePos (float angle, float speed)
	{
		float y;
		y = Mathf.Sin (angle) * speed * Time.deltaTime;
		return y;
	}
}