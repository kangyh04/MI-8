using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeManager : MonoBehaviour {
    // public values
    public float PwCounterMax = 8.0f;

    // GameObjects
    GameObject PwObj;

    // private values
    float Counter;

    // Use this for initialization
    void Start () {
        PwObj = GameObject.Find("RidedPoweredSuite");
        Counter = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        bool isGoaled = PwObj.GetComponent<RidedPoweredSuite>().IsGoaled;
        if (isGoaled == true)
        {
            Counter += Time.deltaTime;
            if (Counter > PwCounterMax)
            {
                SceneManager.LoadScene("GameTitle");
            }
        }
    }
}
