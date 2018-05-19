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

	public static float CircleAngle (float angle)
	{
		while (angle < 0f)
		{
			angle += 360f;
		}
		while (360f < angle)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float CalculateZAngleDifference (Vector2 pos1, Vector2 pos2)
	{
		var xDiff = pos2.x - pos1.x;
		var yDiff = pos2.y - pos1.y;
		var angle = Mathf.Atan2 (xDiff, yDiff) * Mathf.Rad2Deg;
		angle = -CircleAngle (angle);
		return angle;
	}

	public static float CalculateAngleOffset (int dirIndex, float dankAngle, float angleOffset)
	{
		float angle = dirIndex % 2 == 0 ? dankAngle - (angleOffset * (float) dirIndex / 2f) : dankAngle + (angleOffset * Mathf.Ceil ((float) dirIndex / 2f));
		return angle;
	}
}