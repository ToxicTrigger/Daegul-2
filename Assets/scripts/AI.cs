using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    public Transform Target;
    public NavMeshAgent nv;
    public ParticleSystem ps, boom;
    public float attack_power = 0.2f;
    public AudioSource AS, Siren;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            GameObject tmp = GameObject.Instantiate(ps.gameObject, collision.transform.position, Quaternion.identity, null);
            GameObject bom = GameObject.Instantiate(boom.gameObject, transform.position, Quaternion.identity, null);
            AS.Play();
            Destroy(tmp, 0.5f);
            Destroy(bom, 0.5f);
            Destroy(gameObject, 1.0f);
        }
    }

    // Use this for initialization
    void Start () {
        nv = gameObject.GetComponent<NavMeshAgent>();
        AS = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Target.gameObject.GetComponent<Move>().Size >= 4f)
        {
            Siren.Play();
            nv.SetDestination(Target.position);
        }
        else
        {
            Siren.Pause();
        }

    }
}
