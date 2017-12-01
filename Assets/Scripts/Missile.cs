using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MISSILE_TYPE
{
    Player = 0,
    Enemy = 1,
    EnemyRandom = 2,
}

public class Missile : MonoBehaviour {

	public float speed;
    public MISSILE_TYPE type;
    public float enemyRandomSpread = 1;

    private AudioSource audioSrc;
    private int direction;

    void Start () {
        if (type == MISSILE_TYPE.EnemyRandom) {
            // move up or down
            direction = Random.Range( 0, 3 );
        }
    }

	void Update () {
        // player
        if (type == MISSILE_TYPE.Player) {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        // enemy
        else if (type == MISSILE_TYPE.Enemy) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        // enemy random
        else if (type == MISSILE_TYPE.EnemyRandom) {
            if (direction == 1)  {
                transform.Translate(Vector3.up * Time.deltaTime * enemyRandomSpread);
            }
            else if (direction == 2) {
                transform.Translate(Vector3.down * Time.deltaTime * enemyRandomSpread);
            }
            // move left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
	}

    // collision

    void OnTriggerEnter2D(Collider2D other){
        // destroy enemy
    	if(other.tag == "Enemy" && type == MISSILE_TYPE.Player) {
            other.gameObject.GetComponent<Enemy>().shot();
        }
        // destroy player
        else if(other.tag == "Player" && (type == MISSILE_TYPE.Enemy || type == MISSILE_TYPE.EnemyRandom)) {
            other.gameObject.GetComponent<Player>().die();
        }
        // destroy self
        Destroy(gameObject);
    } 

}
