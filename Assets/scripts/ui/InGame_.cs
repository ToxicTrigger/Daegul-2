using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_ : MonoBehaviour {
    
    public void Pause()
    {
        GameManager.instance.game_over = true;
    }
}
