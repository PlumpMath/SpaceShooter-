using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour {


    private float vert;
    private float hori;
    public float speed;
    public GameObject missile;
    private Animator myAnim;

	// Use this for initialization
	void Start () {

        myAnim = GetComponent<Animator>();


		
	}
	
	// Update is called once per frame
	void Update () {

        vert = Input.GetAxisRaw("Vertical");
        hori = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(hori, vert) * Time.deltaTime * speed);

        myAnim.SetFloat("moveUp", vert);
        myAnim.SetFloat("moveDown", -vert);

        if(Input.GetKeyDown("space")) {
             Instantiate(missile);
        }
		
	}
}
