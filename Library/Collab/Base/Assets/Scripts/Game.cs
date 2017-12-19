using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public GameObject wavesObj;
	
	public Text scoreTxt;
	public Text waveTxt;
	public GameObject Player;
	public bool showMenu = true;
	public GameObject MenuTitle;

	public AudioSource FxBtnClick;
	public AudioSource FxNewWave;

	private int score;
	private int waveCnt;
	private int waveIdx;
	private int waveChild;
	private int startWaveX;
	private GameObject curWave;
	private bool isPlaying;
	

	void Start () {
		startWaveX = 17;
		reset(); // nécéssaire pour désactiver toutes les waves
		// play 
		if (showMenu)
			playMenu();
		else
			playGame();
	}

	void playGame() {
		MenuTitle.SetActive(false);
		refreshUI("UI_Game");
		//reset();
		restart();		
	}

	void playMenu() {
		MenuTitle.transform.position = new Vector3(0, 0, 0);
		MenuTitle.SetActive(true);
		refreshUI("UI_Menu");
	}

	void refreshUI(string tag) {
		GameObject canvas = GameObject.Find("Canvas");
		foreach (Transform child in canvas.transform) {
			child.gameObject.SetActive((child.tag == tag));
		}
         
	}

	public void reset() {
		isPlaying = false;
		score = 0;
		waveCnt = 0;
		waveIdx = 0;
		waveChild = wavesObj.transform.childCount;
		curWave = null;
		// disable all waves
		foreach(Transform child in wavesObj.transform)
		{
		    child.gameObject.SetActive(false);
		}
	}

	void Update () {
		if (isPlaying) {
			// new wave
			if (curWave && !isEnemyLeft()) {
				curWave = null;
				newWave();
			}	
		}
	}

	public void gameOver() {
		isPlaying = false;
	}

	public void restart() {
		// reset game
		reset();
		// reset player
		Player.GetComponent<Player>().reset();
    	// reset all enemies
		foreach(Transform wave in wavesObj.transform)
		{
		    foreach(Transform enemy in wave.transform)
			{
				enemy.gameObject.GetComponent<Enemy>().reset();
			}
		}
		// create new wave
		newWave();
		isPlaying = true;
	}

	/*
	public void pause() {
		print("PAUSE");
	}
	*/

	void newWave() {

		// start over
		if (waveIdx == waveChild) {
			waveIdx = 0;
		}

		FxNewWave.Play();

		waveCnt++;

		// display wave #
		waveTxt.gameObject.SetActive(true );
		waveTxt.text = "Wave " + waveCnt.ToString();
		Invoke("hideWaveTxt", 3);

		// activate wave
		Transform wave = wavesObj.gameObject.transform.GetChild(waveIdx);
		wave.transform.position = new Vector3(startWaveX, 0, 0);
    	wave.gameObject.SetActive(true);

    	// reset all enemies
    	foreach(Transform child in wave.transform)
		{
			print("ENEMY");
			child.gameObject.GetComponent<Enemy>().levelup();
		}

		curWave = wave.gameObject;
		waveIdx++;

	}

	// UI
	
	public void creditsBtn() {
		SceneManager.LoadScene("Credits", LoadSceneMode.Single);
	}

	public void menuBtn() {
		print("MENU");
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void playBtn() {
		FxBtnClick.Play();
		playGame();
	}

	public void restartBtn() {
		FxBtnClick.Play();
		Player.GetComponent<Player>().pause(); // marche pas pour fixer le missile qui part quand on fait Restart()
		isPlaying = false;
		restart();

	}

	public void updateScore(int value) {
		score += value;
		scoreTxt.text = score.ToString();
	}

	void hideWaveTxt() {
		waveTxt.gameObject.SetActive(false);
	}

	bool isEnemyLeft() {
		foreach(Transform child in curWave.transform)
		{
			if (child.gameObject.activeSelf)
		    	return true;
		}
		return false;
	}

}
