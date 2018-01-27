using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangarManager : MonoBehaviour {
    // public values
    public float EndCounterMax = 5;
    
    // GameObjects
    GameObject SpyObj;

    // private values
    float Counter;

	// Use this for initialization
	void Start () {
        SpyObj = GameObject.Find("Spy");
        Counter = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        bool isGoaled = SpyObj.GetComponent<Spy>().IsGoaled;
		if( isGoaled == true )
        {
            Counter += Time.deltaTime;
            if(Counter > EndCounterMax)
            {
                SceneManager.LoadScene("Escape");
            }
        }
	}
}
