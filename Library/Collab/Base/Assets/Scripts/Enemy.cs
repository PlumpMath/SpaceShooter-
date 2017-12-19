using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_TYPE
{
	Default = 0,
    EnemyThatMove = 1,
    EnemyThatShoot = 2,
    EnemyKamikaze = 3,
    EnemyThatShootRandom = 4,
}

public class Enemy : MonoBehaviour {

	public int life = 1;
	public float speed;
	public ENEMY_TYPE type;
	public GameObject explosion;
	public float fireRate;
    public GameObject missile;
    public int points = 1;

	private float moveTimer;
	private string moveDirection;
	private float currentShootingTime;
	private int startLife;
	private float startSpeed;
	private Vector3 startPos;
	private Game gameCtrl;
	private GameObject player;
	private int level;

	void Start () {	

		startLife = life;
		startSpeed = speed;
		startPos = transform.localPosition;
		player = GameObject.Find("Player");

		if (type == ENEMY_TYPE.EnemyThatMove) {

			moveTimer = 0;
			moveDirection = (Random.Range( 0, 2 ) == 1) ? "UP": "DOWN";

		}

		gameCtrl = GameObject.Find("Game").GetComponent<Game>();
	}

	void Update () {

		if (type == ENEMY_TYPE.EnemyThatMove) {

			// move up or down
			moveTimer += Time.deltaTime;
			if (moveTimer > 1) {
				moveDirection = (moveDirection == "UP") ? "DOWN" : "UP";
				moveTimer = 0;
			}

			if (moveDirection == "UP") {
				transform.Translate(Vector3.up * Time.deltaTime * speed);
			}
			else {
				transform.Translate(Vector3.down * Time.deltaTime * speed);	
			}
			
		}

		else if (type == ENEMY_TYPE.EnemyThatShoot) {

	        // check fire
	       	if(Time.time > currentShootingTime) {
	        	currentShootingTime = Time.time + fireRate;
	            Vector3 missilePos = new Vector3 (transform.position.x - 2, transform.position.y, 0);
	            Instantiate(missile, missilePos, Quaternion.identity);
	        }

	    }

		else if (type == ENEMY_TYPE.EnemyKamikaze) {

			// go crash on player
			float speedY = 0.6f;
			if (transform.position.y < player.transform.position.y) {
				transform.Translate(Vector3.up * Time.deltaTime * speedY);
			}
			else {
				transform.Translate(Vector3.down * Time.deltaTime * speedY);	
			}
	    }

		else if (type == ENEMY_TYPE.EnemyThatShootRandom) {
	        // check fire
	       	if(Time.time > currentShootingTime) {
	        	currentShootingTime = Time.time + fireRate;
	            Vector3 missilePos = new Vector3 (transform.position.x - 2, transform.position.y, 0);
	            Instantiate(missile, missilePos, Quaternion.identity);
	        }

	    }

		transform.Translate(Vector3.left * Time.deltaTime * speed);

	}

	public void reset() {
		// pos
		transform.localPosition = startPos;
		// life
		life = startLife;
		// speed
		speed = startSpeed;
		// level
		level = 0;
		// activate
		gameObject.SetActive(true);

	}

	public void levelup() {
		// level
		level++;
		// pos
		transform.localPosition = startPos;
		// life
		life = startLife + (level - 1);
		// speed
		speed = startSpeed * level;
		// activate
		gameObject.SetActive(true);
	}

	public void shot () {

		life--;

		if (life <= 0) {
			
			die();

		}
		else {

			// change color
			// shake effet
			// particul effect qui increase

		}

	}

	void die () {
		// add points
		gameCtrl.updateScore(points);
		// explosion
        Vector3 expPos = new Vector3 (transform.position.x, transform.position.y, 0);
		GameObject explosionObj = Instantiate(explosion, expPos, Quaternion.identity);
		Destroy(explosionObj, 1);
		explosionObj = null;
		// disable	
		gameObject.SetActive(false);
	}

}
