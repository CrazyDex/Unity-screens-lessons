using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    //public bool isBlock;
	// Use this for initialization
	void Start () {
        //isBlock = false;
	}
	
	// Update is called once per frame
	void Update () {
        //isBlock = Input.GetButton("Block");
        GetComponent<BoxCollider2D>().enabled = Input.GetButton("Block");
        GetComponent<SpriteRenderer>().enabled = Input.GetButton("Block");
    }
}
