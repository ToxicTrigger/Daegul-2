using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_UI : MonoBehaviour {
    public Text FPS_show;
    float deltaTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        FPS_show.text = string.Format("{0:0.0} ms ({1:0.} fps)", deltaTime * 1000.0f, 1.0f / deltaTime);
	}
}
