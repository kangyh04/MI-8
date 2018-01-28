using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spy : MonoBehaviour {
    // public values
    public float SpySpeed = 0.1f;
    public bool IsGoaled = false;
    public bool IsGameOver = false;

    // private values

    // Use this for initialization
    void Start () {
        IsGoaled = false;
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update () {
        // move
        if(IsGoaled == false)
        {
            // move spy
            Vector3 spypos = transform.position;
            spypos.x += SpySpeed * Time.deltaTime;

            transform.position = spypos;

            // GameOver
        }else if( IsGameOver == false)
        {
               // don't move
        }
    }

    // collision
    private void OnTriggerEnter(Collider collision)
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
            // GameOver
            IsGameOver = true;
            Debug.Log("Game Over by Obstacle");
        }

        // Hit any
        if (collision.GetComponent<Disturbance>() != null)
        {
            // GameOver
            IsGameOver = true;
            Debug.Log("Game Over");
        }
    }
}
