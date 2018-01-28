using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangarManager : MonoBehaviour
{
    // public values
    public float EndCounterMax = 5.0f;
    public float RestartCounterMax = 1.0f;
    public float FadeOutStartCounterMax = 0.1f;

    // Jingle
    public AudioClip JingleStart;
    public AudioClip JingleClear;
    public AudioClip JingleGameOver;
    bool IsPlayJingleClear = false;

    // GameObjects
    GameObject SpyObj;
    GameObject FadeObj;

    // private values
    float Counter = 0.0f;
    float RestartCounter = 0.0f;
    float FadeOutStartCounter = 0.0f;
    bool IsFadeOut = false;
    bool IsGameOver = false;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        // Find
        SpyObj = GameObject.Find("Spy");
        FadeObj = GameObject.Find("FadeInOut");

        // counter
        Counter = 0.0f;
        RestartCounter = 0.0f;
        FadeOutStartCounter = 0.0f;
        IsFadeOut = false;
        IsGameOver = false;

        // Play Jingle
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = JingleStart;
        audioSource.Play();
        IsPlayJingleClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Spy State
        bool isGoaled = SpyObj.GetComponent<Spy>().IsGoaled;
        bool isGameover = SpyObj.GetComponent<Spy>().IsGameOver;

        // Goal
        if (isGoaled == true)
        {
            // Play Jingle
            if (IsPlayJingleClear == false)
            {
                audioSource.clip = JingleClear;
                audioSource.Play();
                IsPlayJingleClear = true;
            }


            // Scene Change
            Counter += Time.deltaTime;
            if (Counter > EndCounterMax)
            {
                SceneManager.LoadScene("Escape");
            }

            // GameOver
        }else if(isGameover == true)
        {
            if(IsGameOver == false)
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
        if(IsGameOver == true)
        {
            RestartCounter += Time.deltaTime;
            if(RestartCounter > RestartCounterMax)
            {
                SceneManager.LoadScene("Hangar");
                //SceneManager.LoadScene("IshihataFade");
            }
        }

        // FadeOut
        FadeOutStartCounter += Time.deltaTime;
        if(IsFadeOut == false)
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
