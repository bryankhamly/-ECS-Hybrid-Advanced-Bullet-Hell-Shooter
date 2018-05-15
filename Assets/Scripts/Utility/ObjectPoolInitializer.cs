using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolInitializer : MonoBehaviour
{
	public ObjectSpawns[] objectSpawns;

	void Start ()
	{
		for (int i = 0; i < objectSpawns.Length; i++)
		{
			var objectSpawn = objectSpawns[i];

			for (int j = 0; j < objectSpawn.amount; j++)
			{
				var XD = objectSpawn.objectToSpawn.GetPooledInstance<PooledObject> ();
				StartCoroutine (ReturnPool (XD));
			}
		}
	}

	IEnumerator ReturnPool (PooledObject XD)
	{
		yield return null; //Waits a frame.
		XD.ReturnToPool ();
	}
}