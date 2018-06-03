using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
	public BossHealth bossHealth;

	[Header ("Music")]
	public AudioSource audioSource;
	public AudioClip phase1;
	public AudioClip phase2;
	public AudioClip phase3;

	public GameObject pauseScreen;
	bool paused;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		//InitializePlayer();
		Application.targetFrameRate = 60;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			paused = !paused;

			if (paused)
			{
				Time.timeScale = 0.0f;
				pauseScreen.SetActive (true);
			}
			else
			{
				Time.timeScale = 1.0f;
				pauseScreen.SetActive (false);
			}
		}
	}

	public void InitializePlayer ()
	{
		player = Instantiate (PlayerPrefab, SpawnPos, Quaternion.identity);
		player.GetComponent<PlayerPoints> ().pointsText = pointsUI;
		player.GetComponent<PlayerHealth> ().lifeIcon = lifeIcons;
		player.GetComponent<PlayerHealth> ().loseScreen = loseXD;
		player.GetComponent<Special> ().statusBar = status;
		player.GetComponent<PlayerWeapons> ().waveManager = waveManager;
		waveManager.player = player.transform;
		bossHealth.target = player.transform;
	}
}