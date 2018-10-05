using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openSwitch()
    {
        SceneManager.LoadScene("Switch", LoadSceneMode.Single);
    }
    public void openLight()
    {
        SceneManager.LoadScene("Light", LoadSceneMode.Single);
    }


}
