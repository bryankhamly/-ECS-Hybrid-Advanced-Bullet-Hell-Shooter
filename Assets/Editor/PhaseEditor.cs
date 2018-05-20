using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (EnemyBehavior), true)]
public class PhaseEditor : Editor
{
	SerializedObject touhouObject;
	EnemyBehavior enemyB;
	string phaseIndex = "0";
	bool canStart = true;

	void OnEnable ()
	{
		touhouObject = new SerializedObject (target);
		enemyB = (EnemyBehavior) target;
	}

	public override void OnInspectorGUI ()
	{
		touhouObject.Update ();
		base.OnInspectorGUI ();

		GUILayout.BeginVertical ("box");
		phaseIndex = GUILayout.TextField (phaseIndex, 2, "textfield");
		GUI.enabled = canStart;
		if (GUILayout.Button ("Start Phase"))
		{
			if (Application.isPlaying && enemyB.gameObject.activeInHierarchy)
			{
				canStart = false;
				enemyB.StartPhase (int.Parse (phaseIndex));
			}
		}
		GUI.enabled = true;
		if (GUILayout.Button ("Stop Phase"))
		{
			if (Application.isPlaying && enemyB.gameObject.activeInHierarchy)
			{
				canStart = true;
				enemyB.StopPhase ();
			}
		}
		GUILayout.EndVertical ();

		if (GUI.changed)
		{
			EditorUtility.SetDirty (target);
			EditorUtility.SetDirty (enemyB);
		}

		touhouObject.ApplyModifiedProperties ();
	}
}