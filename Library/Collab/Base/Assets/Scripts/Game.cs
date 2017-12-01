using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {


	public GameObject wavesObj;

	private int waveIdx;
	private int EnemyCount;

	private int startWaveX;

	void Start () {

		waveIdx = 0;
		startWaveX = 17;

		// disable all waves

		foreach(Transform child in wavesObj.transform)
		{
		    child.gameObject.SetActive(false);
		}

		newWave();

	}

	public void Restart() {

		print("RESTART");

	}

	void newWave() {
		
		//print("NEW WAVE");
		
		Transform wave = wavesObj.gameObject.transform.GetChild(waveIdx);

        wave.gameObject.SetActive(true);

	}

}
