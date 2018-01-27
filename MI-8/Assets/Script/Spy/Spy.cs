using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour {
    // public values
    public float SpySpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // move spy
        Vector3 spypos = transform.position;
        spypos.x += SpySpeed * Time.deltaTime;

        transform.position = spypos;
	}

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PoweredSuite" )
        {
            Debug.Log("Goal!");
        }
    }
}
