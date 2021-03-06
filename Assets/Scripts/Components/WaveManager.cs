﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public enum Difficulty
{
	Normal,
	Hard
}

public class WaveManager : MonoBehaviour
{
	public Difficulty difficulty;
	public GameManager gameManager;
	public TextMeshProUGUI waveText;
	public WaypointManager waypoints;
	public List<Wave> waves;
	public int waveIndex = -1;

	public List<Enemy> enemyList;
	public List<Transform> bossList;
	private bool running;
	private bool spawning;

	public EnemyBehavior boss;
	public bool done;

	public GameObject startButton;
	public GameObject startButton2;

	public Transform player;

	[Header ("Boss Stuff")]
	public int easyHealth1;
	public int easyHealth2;
	public int easyHealth3;
	[Space (10)]
	public int hardHealth1;
	public int hardHealth2;
	public int hardHealth3;

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
				Debug.Log ("Wave Finished " + waveIndex);
				running = false;

				if (WasLastWave () && !done)
				{
					done = true;
					Debug.Log ("All Waves Finished");
					boss.gameObject.SetActive (true);
					boss.gameObject.GetComponent<BossHealth> ().StartBoss ();

					bossList.Add (boss.transform);
				}
				else
				{
					Debug.Log ("Next Wave...");
					waveIndex++;
					StartWave (waveIndex);
				}

			}
		}

	}

	public void StartButtonNormal ()
	{
		difficulty = Difficulty.Normal;
		StartWave (0);
		startButton.SetActive (false);
		startButton2.SetActive (false);
		gameManager.InitializePlayer ();
	}

	public void StartButtonHard ()
	{
		difficulty = Difficulty.Hard;
		StartWave (0);
		startButton.SetActive (false);
		startButton2.SetActive (false);
		gameManager.InitializePlayer ();
	}

	public void StartWave (int index)
	{
		if (running)
			return;
		Debug.Log ("Wave Start: " + index);
		waveIndex = index;
		enemyList.Clear ();
		running = true;
		spawning = true;
		StartCoroutine (WaveCoroutine (index));
		waveText.text = (waveIndex + 1).ToString ();
	}

	IEnumerator WaveCoroutine (int index)
	{
		for (int i = 0; i < waves[index].spawns.Length; i++)
		{
			for (int j = 0; j < waves[index].spawns[i].enemyCount; j++)
			{
				var spawn = waves[index].spawns[i].enemy.GetPooledInstance<PooledObject> ();
				Enemy enemy = spawn.GetComponent<Enemy> ();
				enemyList.Add (enemy);
				enemy.myGroup = enemyList;
				enemy.myWaypoint = waypoints.wayPoints[waves[index].spawns[i].waypointIndex];
				enemy.gameObject.transform.position = enemy.myWaypoint.points[0].position;

				if (waves[index].spawns[i].aim)
				{
					enemy.gameObject.GetComponent<PlebEnemy> ().aim = true;
				}
				yield return new WaitForSeconds (waves[index].spawns[i].timePerSpawn);
			}
		}

		spawning = false;
		Debug.Log ("Wave Finished Spawning: " + index);
		yield break;
	}

	bool WasLastWave ()
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