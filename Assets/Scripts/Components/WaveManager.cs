using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
	public TextMeshProUGUI waveText;
	public WaypointManager waypoints;
	public List<Wave> waves;
	public int waveIndex = -1;

	public List<Enemy> enemyList;
	private bool running;
	private bool spawning;
	
	void Start ()
	{
		waveIndex = -1;
	}
	
	void Update () 
	{
		if (running)
		{
			if (enemyList.Count == 0 && !spawning)
			{
				Debug.Log("Wave Finished " + waveIndex);
				running = false;

				if (WasLastWave())
				{
					Debug.Log("All Waves Finished");
				}
				else
				{
					Debug.Log("Next Wave...");
					waveIndex++;
					StartWave(waveIndex);
				}
			
			}
		}
	
	}

	public void StartWave(int index)
	{
		if (running)
			return;
		Debug.Log("Wave Start: " + index);
		waveIndex = index;
		enemyList.Clear();
		running = true;
		spawning = true;
		StartCoroutine(WaveCoroutine(index));
		waveText.text = (waveIndex + 1).ToString();
	}

	
	IEnumerator WaveCoroutine(int index)
	{
		for (int i = 0; i < waves[index].spawns.Length; i++)
		{
			for (int j = 0; j < waves[index].spawns[i].enemyCount; j++)
			{
				var spawn = waves[index].spawns[i].enemy.GetPooledInstance<PooledObject>();
				Enemy enemy = spawn.GetComponent<Enemy>();		
				enemyList.Add(enemy);
				enemy.myGroup = enemyList;
				enemy.myWaypoint = waypoints.wayPoints[waves[index].spawns[i].waypointIndex];
				enemy.gameObject.transform.position = enemy.myWaypoint.points[0].position;
				yield return new WaitForSeconds(waves[index].spawns[i].timePerSpawn);
			}
		}

		spawning = false;
		Debug.Log("Wave Finished Spawning: " + index);
		yield break;
	}

	bool WasLastWave()
	{
		bool wow;
		if (waveIndex == waves.Count - 1)
		{
			wow = true;
		}
		else
		{
			wow = false;
		}
		return wow;
	}
}
