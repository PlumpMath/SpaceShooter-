using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject missile;
    public GameObject explosion;
    public AudioSource missileFx;

    private float vert;
    private float hori;
    private Animator myAnim;
    private float currentShootingTime;
    private Vector3 startPos;
    private bool isPlaying;

	void Start () {
        myAnim = GetComponent<Animator>();
        startPos = transform.localPosition;
        isPlaying = false;
	}
	
    void Update () {
        if (isPlaying) {
            vert = Input.GetAxisRaw("Vertical");
            hori = Input.GetAxisRaw("Horizontal");

            transform.Translate(new Vector2(hori, vert) * Time.deltaTime * speed);

            myAnim.SetFloat("moveUp", vert);
            myAnim.SetFloat("moveDown", -vert);

            // check fire
           	if(Input.GetAxisRaw("Fire1") == 1 && checkShoot()) {
                missileFx.Play();
            	currentShootingTime = Time.time + fireRate;
                Vector3 missilePos = new Vector3 (transform.position.x + 2, transform.position.y, 0);
                Instantiate(missile, missilePos, Quaternion.identity);
            }
        }	
	}

    public void reset() {
        transform.localPosition = startPos;
        gameObject.SetActive(true);
        isPlaying = true;
    }

    /*
    public void play() {
        isPlaying = true;
    }
    */

    public void pause() {
        isPlaying = false;
    }

    void OnCollisionEnter2D(Collision2D other){
        // player die!
        if (other.gameObject.tag != "Wall") {
        	die();
        }      
    } 

    public void die() {
        // explosion
        Vector3 expPos = new Vector3 (transform.position.x, transform.position.y, 0);
        GameObject explosionObj = Instantiate(explosion, expPos, Quaternion.identity);
        Destroy(explosionObj, 1);
        explosionObj = null;
        // disable
        gameObject.SetActive(false);
        // end game
        
        //GameObject.Find("Game").GetComponent<Game>.gameOver();
        //GameObject.Find("Game").gameObject.GetComponent<Game>.gameOver();
        GameObject.Find("Game").GetComponent<Game>().gameOver();
    }

	bool checkShoot()  
	{
	    return (Time.time > currentShootingTime);
	}

}
//Assets/Scripts/Player.cs(78,33): error CS0119: Expression denotes a `method group', where a `variable', `value' or `type' was expected
