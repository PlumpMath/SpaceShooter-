using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public GameObject wavesObj;
	public Text scoreTxt;

	private int score;
	private int waveIdx;
	private int waveCnt;
	private int startWaveX;
	private GameObject curWave;

	void Start () {

		score = 0;
		waveIdx = 0;
		startWaveX = 20;
		waveCnt = wavesObj.transform.childCount;

		// disable all waves

		foreach(Transform child in wavesObj.transform)
		{
		    child.gameObject.SetActive(false);
		}

		newWave();
	}

	void Update () {

		if (curWave && !isEnemyLeft()) {

			curWave = null;

			newWave();
		}
	}

	bool isEnemyLeft() {

		foreach(Transform child in curWave.transform)
		{
			if (child.gameObject.activeSelf)
		    	return true;
		}
		return false;
	}

	public void Restart() {

		print("RESTART");

	}

	public void updateScore(int value) {
		score += value;
		scoreTxt.text = score.ToString();
	}

	void newWave() {
		
		print("new wave " + waveIdx);

		if (waveIdx == waveCnt) {

			print("START OVER");

			waveIdx = 0;
		}

		Transform wave = wavesObj.gameObject.transform.GetChild(waveIdx);

		wave.transform.position = new Vector3(startWaveX, 0, 0);

    	wave.gameObject.SetActive(true);

    	// reset all enemies

    	foreach(Transform child in wave.transform)
		{
			child.gameObject.GetComponent<Enemy>().reset();
		}

		curWave = wave.gameObject;
	
		waveIdx++;

	}

}
