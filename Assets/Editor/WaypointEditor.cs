using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (WaypointManager))]
public class WaypointEditor : Editor
{
	SerializedObject waypointManagerSO;
	WaypointManager myWaypointManager;
	string wpName = "";

	void OnEnable ()
	{
		waypointManagerSO = new SerializedObject (target);
		myWaypointManager = (WaypointManager) target;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		waypointManagerSO.Update ();

		GUILayout.BeginVertical ("box");
		wpName = GUILayout.TextField (wpName, 20, "textfield");

		if (GUILayout.Button ("Add Waypoint"))
		{
			myWaypointManager.wayPoints.Add (new Waypoint () { waypointName = wpName });
		}
		GUILayout.EndVertical ();

		GUILayout.BeginVertical ("box");
		if (GUILayout.Button ("Add Point"))
		{
			GameObject newPoint = new GameObject ();
			var pointTransform = newPoint.transform;
			newPoint.transform.name = "Point";
			newPoint.transform.parent = myWaypointManager.gameObject.transform;
			var lastPoint = myWaypointManager.wayPoints[myWaypointManager.wayPoints.Count - 1].points;
			lastPoint.Add (pointTransform);
			Selection.activeGameObject = newPoint;
		}

		if (GUILayout.Button ("Remove Point"))
		{
			var lastPoint = myWaypointManager.wayPoints[myWaypointManager.wayPoints.Count - 1].points;
			lastPoint.RemoveAt (lastPoint.Count - 1);
			DestroyImmediate (lastPoint[lastPoint.Count - 1]);
		}
		GUILayout.EndVertical ();

		if (GUI.changed)
		{
			EditorUtility.SetDirty (target);
			EditorUtility.SetDirty (myWaypointManager);
		}

		waypointManagerSO.ApplyModifiedProperties ();
	}
}