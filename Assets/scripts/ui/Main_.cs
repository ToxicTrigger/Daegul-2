using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_ : MonoBehaviour {
    public Animator ani;
    public GameObject img;
    public void game_start()
    {
        GameManager.instance.game_over = false;
        GameManager.instance.is_done = true;
        ani.SetBool("isPlaying", true);
        img.SetActive(false);
    }
}
