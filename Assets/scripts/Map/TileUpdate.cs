using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUpdate : MonoBehaviour {
    /// <summary>
    /// 플레이어가 해당 타일에 있는가?
    /// </summary>
    public bool hasPlayerHere;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag.Equals("Player"))
        {
            hasPlayerHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            hasPlayerHere = false;
            
        }
    }
}
