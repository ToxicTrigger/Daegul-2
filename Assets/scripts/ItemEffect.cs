using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour {
    public Vector3 offset;
    public Quaternion angle ;
    public float t;
	
	void Update () {
        t += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = offset.y + Mathf.Sin(t);

        transform.rotation = angle;
        transform.position = pos;

	}
}
