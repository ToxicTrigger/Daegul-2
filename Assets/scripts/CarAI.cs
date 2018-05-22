using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour {
    public Transform start, end, endturn, startturn;
    public bool is_get_down;
    public NavMeshAgent na;
    public GameObject cur;

	// Use this for initialization
	void Start () {
        //na = GetComponent<NavMeshAgent>();
        na.SetDestination(start.position);
	}
    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log(collision.name);
        if (collision.gameObject.transform.Equals(start))
        {
            cur = endturn.gameObject;
        }
        else if (collision.gameObject.transform.Equals(endturn))
        {
            cur = end.gameObject;
        }
        else if (collision.gameObject.transform.Equals(end))
        {
            cur = startturn.gameObject;
        }
        else if (collision.gameObject.transform.Equals(startturn))
        {
            cur = start.gameObject;
        }
    }

    // Update is called once per frame
    void Update () {
        na.SetDestination(cur.transform.position);
	}
}
