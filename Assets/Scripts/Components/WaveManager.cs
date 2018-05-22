using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public WaypointManager waypoints;
	public List<Wave> waves;
	public int waveIndex = -1;
	
	void Start ()
	{
		waveIndex = -1;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown((KeyCode.J)))
		{
			StartWave(0);
		}
	}

	public void StartWave(int index)
	{
		waveIndex = index;
		StartCoroutine(WaveCoroutine(index));
	}

	IEnumerator WaveCoroutine(int index)
	{
		for (int i = 0; i < waves[index].spawns.Length; i++)
		{
			for (int j = 0; j < waves[index].spawns[i].enemyCount; j++)
			{
				var spawn = waves[index].spawns[i].enemy.GetPooledInstance<PooledObject>();
				Enemy enemy = spawn.GetComponent<Enemy>();
				yield return new WaitForSeconds(waves[index].spawns[i].timePerSpawn);
			}
		}
		
		yield break;
	}
}
