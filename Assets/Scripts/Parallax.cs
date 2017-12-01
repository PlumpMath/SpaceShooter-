using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float scrollSpeed;

    void Update ()
    {

        if (transform.position.x < -25) {

            // remettre bg a la position de depart

        	transform.position = new Vector3 (25.5f, transform.position.y , 0);

        }

        else {

            // move bg

        	transform.position = new Vector3 (transform.position.x + 1 * scrollSpeed, transform.position.y ,0);

        }
    }

}