using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public Transform cameraTransform;
	public float shakeTime;
	public float shakePower;
	Vector3 pos;
	public float shakeTimer;

	void Start ()
	{
		pos = transform.localPosition;
		shakeTimer = shakeTime;
	}

	void Update ()
	{
		shakeTimer += Time.deltaTime;

		if (shakeTimer < shakeTime)
		{
			cameraTransform.localPosition = pos + Random.insideUnitSphere * shakePower;
		}
		else
		{
			cameraTransform.localPosition = pos;
		}
	}
}