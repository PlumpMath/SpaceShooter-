using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D  collision) {
		if (collision.gameObject.tag == "Enemy") {
	    	collision.gameObject.SetActive(false);
		}
		else {
			print("SHREDDER LET GO THIS ONE" + collision.gameObject.tag);
		}
	}


}
