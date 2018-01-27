using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spy : MonoBehaviour {
    // public values
    public float SpySpeed = 0.1f;
    public bool IsGoaled = false;

    // private values

    // Use this for initialization
    void Start () {
        IsGoaled = false;		
	}
	
	// Update is called once per frame
	void Update () {
        if(IsGoaled == false)
        {
            // move spy
            Vector3 spypos = transform.position;
            spypos.x += SpySpeed * Time.deltaTime;

            transform.position = spypos;
        }
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanger stage clear
        if( collision.tag == "PoweredSuite" )
        {
            IsGoaled = true;
            
            // next stage
            //SceneManager.LoadScene("Escape");
            Debug.Log("Goal!");
        }

        // Hit Obstacle
        if (collision.tag == "Obstacle")
        {
            Debug.Log("Game Over");
        }

        // Hit any
        if (collision.GetComponent<Disturbance>() != null)
        {
            Debug.Log("Game Over");
        }
    }
}
