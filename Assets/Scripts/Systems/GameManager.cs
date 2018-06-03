using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject PlayerPrefab;
	public Vector3 SpawnPos;

	public GameObject player;

	public TextMeshProUGUI pointsUI;
	public Transform lifeIcons;
	public GameObject loseXD;
	public Image status;

	public WaveManager waveManager; //enemies from list on wavemanager

	[Header("Music")]
	public AudioSource audioSource;
	public AudioClip phase1;
	public AudioClip phase2;
	public AudioClip phase3;
	
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		//InitializePlayer();
		Application.targetFrameRate = 60;
	}

	public void InitializePlayer()
	{
		player = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
		player.GetComponent<PlayerPoints>().pointsText = pointsUI;
		player.GetComponent<PlayerHealth>().lifeIcon = lifeIcons;
		player.GetComponent<PlayerHealth>().loseScreen = loseXD;
		player.GetComponent<Special>().statusBar = status;
		player.GetComponent<PlayerWeapons>().waveManager = waveManager;
		waveManager.player = player.transform;
	}
}
