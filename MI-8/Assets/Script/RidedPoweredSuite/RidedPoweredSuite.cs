﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RidedPoweredSuite : MonoBehaviour
{
    // move speed
    public float PwSpeed = 5.0f;
    public float PwVspeed = 10.0f;
    public float PwCounterMax = 8.0f;
    public bool IsGoaled = false;
    public bool IsGameOver = false;

    // private values
    float EndCounter = 0.0f;

    // Use this for initialization
    void Start()
    {
        IsGoaled = false;
        IsGameOver = false;
        EndCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGoaled == false)
        {
            // GameOver
            if (IsGameOver == true)
            {
                // don't move
            }else
            {
                // move horizontal
                Vector3 pos = transform.position;
                pos.x += PwSpeed * Time.deltaTime;
                transform.position = pos;
            }
        }
        else
        {
            // move vertical
            Vector3 pos = transform.position;
            pos.y += PwVspeed * Time.deltaTime;
            transform.position = pos;

            // counter
            EndCounter += Time.deltaTime;
            if(EndCounter > PwCounterMax )
            {
                //SceneManager.LoadScene("GameTitle");
            }
        }
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanger stage clear
        if (collision.tag == "Ladders")
        {
            IsGoaled = true;
            Debug.Log("GAME CLEAR!!");
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