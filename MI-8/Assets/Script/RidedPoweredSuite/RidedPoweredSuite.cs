using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidedPoweredSuite : MonoBehaviour
{
    // move speed
    public float PwSpeed = 5.0f;

    // private values
    bool isGoaled = false;


    // Use this for initialization
    void Start()
    {
        isGoaled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoaled == false)
        {
            // move
            Vector3 pos = transform.position;
            pos.x += PwSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanger stage clear
        if (collision.tag == "Ladders")
        {
            isGoaled = true;
            Debug.Log("GAME CLEAR!!");
        }
    }
}