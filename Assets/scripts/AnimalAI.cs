using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour {
    public Transform point;
    public bool has_touch_down;
    public Transform start;
    public float speed= 0.5f;
    public float jump = 2;


    void set_destination_pos()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        Vector3 pos = new Vector3(x, 0, z);
        point.position = pos + transform.position;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Point_E"))
        {
            has_touch_down = true;
        }else if(col.tag.Equals("Point_S"))
        {
            has_touch_down = false;
        }
    }
    float t;
    IEnumerator Move()
    {
        yield return new WaitForSeconds(0.1f);
        t += 1f;
        if (has_touch_down)
        {
            Vector3 pos = start.position - transform.position;
            transform.Translate(pos.normalized * speed);

        }else
        {
            Vector3 pos = point.position - transform.position;   
            transform.Translate(pos.normalized * speed);
        }
    }

    void Start () {
        set_destination_pos();
        
	}
	
	
	void Update () {
        StartCoroutine(Move());
	}
}
