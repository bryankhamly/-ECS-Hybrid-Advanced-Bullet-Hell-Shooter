using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (TouhouPattern), true)]
public class TouhouEditor : Editor
{
	SerializedObject touhouObject;
	TouhouPattern touhouPattern;

	void OnEnable ()
	{
		touhouObject = new SerializedObject (target);
		touhouPattern = (TouhouPattern) target;
	}

	public override void OnInspectorGUI ()
	{
		touhouObject.Update ();
		base.OnInspectorGUI ();

		GUILayout.BeginVertical ("box");
		if (GUILayout.Button ("Test"))
		{
			if (Application.isPlaying && touhouPattern.gameObject.activeInHierarchy)
			{
				touhouPattern.ShootBullet ();
			}
		}
		GUILayout.EndVertical ();

		if (GUI.changed)
		{
			EditorUtility.SetDirty (target);
			EditorUtility.SetDirty (touhouPattern);
		}

		touhouObject.ApplyModifiedProperties ();
	}
}