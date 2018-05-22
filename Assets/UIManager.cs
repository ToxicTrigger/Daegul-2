using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image timer;
    public Button pause;

    public Move pig;
    public float pig_size;
    public Move.State pig_state;
    public bool stop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(GameManager.instance.is_load_done)
        {
            if(!stop)
            {
                float a = GameManager.instance.play_time / GameManager.instance.max_play_time;
                timer.fillAmount = 1 - a;
            }
    
        }
	}
}
