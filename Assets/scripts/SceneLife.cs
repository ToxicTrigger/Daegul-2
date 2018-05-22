using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLife : MonoBehaviour {
    public 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.game_over)
        {
            SceneManager.LoadScene("main_menu");
            //SceneManager.UnloadSceneAsync("building");
        }
	}
}
