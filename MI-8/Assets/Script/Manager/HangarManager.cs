using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangarManager : MonoBehaviour
{
    // public values
    public float EndCounterMax = 5;

    // Jingle
    public AudioClip JingleStart;
    public AudioClip JingleClear;
    bool IsPlayJingleClear = false;

    // GameObjects
    GameObject SpyObj;

    // private values
    float Counter;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        SpyObj = GameObject.Find("Spy");
        Counter = 0.0f;

        // Play Jingle
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = JingleStart;
        audioSource.Play();
        IsPlayJingleClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGoaled = SpyObj.GetComponent<Spy>().IsGoaled;
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
        }
    }
}
