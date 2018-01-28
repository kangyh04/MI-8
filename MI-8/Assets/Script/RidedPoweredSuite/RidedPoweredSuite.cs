using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RidedPoweredSuite : MonoBehaviour
{
    // move speed
    public float PwSpeed = 5.0f;
    public float PwAcceleSpeed = 0.1f;
    public float PwVspeed = 10.0f;
    public float PwCounterMax = 8.0f;
    public bool IsGoaled = false;
    public bool IsGameOver = false;
    public float WaitCounterMax = 1.0f;

    // private values
    float EndCounter = 0.0f;
    float WaitCounter = 0.0f;
    bool IsWait = false;

    // GameObjects
    GameObject EfxBlast;

    // Use this for initialization
    void Start()
    {
        IsGoaled = false;
        IsGameOver = false;
        EndCounter = 0.0f;

        IsWait = false;
        WaitCounter = 0.0f;

        EfxBlast = GameObject.Find("EfxBlast");
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

                // Accel
                PwSpeed += PwAcceleSpeed;
            }
        }
        else
        {
            if( IsWait == true)
            {
                // don't move
                WaitCounter += Time.deltaTime;
                if(WaitCounter > WaitCounterMax)
                {
                    IsWait = false;
                }
            }
            else
            {
                // move vertical
                Vector3 pos = transform.position;
                pos.y += PwVspeed * Time.deltaTime;
                transform.position = pos;
            }

            // counter
            //EndCounter += Time.deltaTime;
            //if(EndCounter > PwCounterMax )
            //{
            //SceneManager.LoadScene("GameTitle");
            //}
        }
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanger stage clear
        if (collision.tag == "Ladders")
        {
            IsGoaled = true;
            IsWait = true;
            EfxBlast.GetComponent<ParticleSystem>().Play();
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