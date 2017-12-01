using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public void creditBackBtn() {
		print("MEUH");
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

}
