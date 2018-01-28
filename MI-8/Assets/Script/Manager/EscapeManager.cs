using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeManager : MonoBehaviour {
    // public values
    public float EndCounterMax = 5.0f;
    public float RestartCounterMax = 1.0f;
    public float FadeOutStartCounterMax = 0.1f;

    public float PwCounterMax = 8.0f;

    public AudioClip JingleGameOver;

    // GameObjects
    GameObject PwObj;
    GameObject FadeObj;

    // private values
    float Counter = 0.0f;
    float RestartCounter = 0.0f;
    float FadeOutStartCounter = 0.0f;
    bool IsFadeOut = false;
    bool IsGameOver = false;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        // Find GameObjects
        PwObj = GameObject.Find("RidedPoweredSuite");
        FadeObj = GameObject.Find("FadeInOut");

        // counter
        Counter = 0.0f;
        RestartCounter = 0.0f;
        FadeOutStartCounter = 0.0f;
        IsFadeOut = false;
        IsGameOver = false;

        // Play Jingle
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        // Get Pw State
        bool isGoaled = PwObj.GetComponent<RidedPoweredSuite>().IsGoaled;
        bool isGameover = PwObj.GetComponent<RidedPoweredSuite>().IsGameOver;

        // Goal
        if (isGoaled == true)
        {
            Counter += Time.deltaTime;
            if (Counter > PwCounterMax)
            {
                SceneManager.LoadScene("GameTitle");
            }
        }
        // GameOver
        else if (isGameover == true)
        {
            if (IsGameOver == false)
            {
                IsGameOver = true;
                FadeObj.GetComponent<FadeInOut>().SetFadeIn();
                RestartCounter = 0.0f;

                // Play Jingle
                audioSource.clip = JingleGameOver;
                audioSource.Play();
            }
        }

        // Restart
        if (IsGameOver == true)
        {
            RestartCounter += Time.deltaTime;
            if (RestartCounter > RestartCounterMax)
            {
                SceneManager.LoadScene("Escape");
                //SceneManager.LoadScene("IshihataEscapeRobo");
            }
        }

        // FadeOut
        FadeOutStartCounter += Time.deltaTime;
        if (IsFadeOut == false)
        {
            // Set FadeOut
            FadeObj.GetComponent<FadeInOut>().SetFadeOut();
            if (FadeOutStartCounter > FadeOutStartCounterMax)
            {
                IsFadeOut = true;
            }
        }
    }
}
