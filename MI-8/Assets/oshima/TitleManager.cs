using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PushStartButton(){
		SceneManager.LoadScene ("intoro");
	}
	public void PushStaffButton(){
		SceneManager.LoadScene ("Staff");
	}
}
