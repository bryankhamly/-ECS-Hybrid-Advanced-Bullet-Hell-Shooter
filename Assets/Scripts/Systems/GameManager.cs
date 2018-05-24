using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject PlayerPrefab;
	public Vector3 SpawnPos;

	private GameObject player;
	
	void Start () 
	{
		InitializePlayer();
		Application.targetFrameRate = 60;
	}

	void InitializePlayer()
	{
		//player = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
	}
}
