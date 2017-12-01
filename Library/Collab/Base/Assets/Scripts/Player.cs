using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject missile;
    public GameObject explosion;

    private float vert;
    private float hori;
    private Animator myAnim;
    private float currentShootingTime;

	void Start () {
        myAnim = GetComponent<Animator>();
	}
	
    void Update () {

        vert = Input.GetAxisRaw("Vertical");
        hori = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(hori, vert) * Time.deltaTime * speed);

        myAnim.SetFloat("moveUp", vert);
        myAnim.SetFloat("moveDown", -vert);

        // check fire
       	if(Input.GetAxisRaw("Fire1") == 1 && checkShoot()) {
        	currentShootingTime = Time.time + fireRate;
            Vector3 missilePos = new Vector3 (transform.position.x + 2, transform.position.y, 0);
            Instantiate(missile, missilePos, Quaternion.identity);
        }
		
	}

    void OnCollisionEnter2D(Collision2D other){

        // player die!

        if (other.gameObject.tag != "Wall") {

			gameObject.SetActive(false);

        	// explosion
/*
			Vector3 expPos = new Vector3 (transform.position.x, transform.position.y, 0);

			GameObject explosionObj = Instantiate(explosion, expPos, Quaternion.identity);

			Destroy(explosionObj, 1);

			explosionObj = null;
*/
        }
       
    } 

	bool checkShoot()  
	{
	    return (Time.time > currentShootingTime);
	}

}
