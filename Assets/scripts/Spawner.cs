using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public bool is_overlaped;
    public float radius;
    public bool is_animal_type;

    public GameObject item;

    public int spawner_level;
    public bool is_gened_item;
    public bool live = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Spawner"))
        {
            is_overlaped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Spawner"))
        {
            is_overlaped = false;
        }
    }

    public void Awake()
    {
        radius = GetComponent<SphereCollider>().radius;
        StartCoroutine(gen_items());
        //gen_items();
    }

    // Do Make Itmes..
    IEnumerator gen_items()
    {
        yield return new WaitForEndOfFrame();
        while(item == null)
        {
            if(!is_animal_type)
            {
                IEnumerator iter = GameManager.instance.lev_preset.GetEnumerator();
                while (iter.MoveNext())
                {
                    Preset p = iter.Current as Preset;
                    float per = UnityEngine.Random.Range(0.0f, 100.0f);
                    //Debug.Log("preset.name = " + p.name);
                    if (p.per >= per)
                    {
                        try
                        {
                            item = Instantiate(Resources.Load("objects/" + p.name), gameObject.transform.position, Quaternion.identity, transform) as GameObject;
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError("Cant! || " + e.ToString());
                            break;
                        }
                        //Debug.Log("item.name = " + item.name);
                        is_gened_item = true;
                        spawner_level = p.lev;
                        break;
                    }
                }
            }
            else
            {
                float per = UnityEngine.Random.Range(0.0f, 100.0f);
                if(per <= 50.0f)
                {
                    GameObject cat = Instantiate(Resources.Load("objects/cat"), gameObject.transform.position, Quaternion.identity, transform) as GameObject;
                }else
                {
                    GameObject cat = Instantiate(Resources.Load("objects/dog"), gameObject.transform.position, Quaternion.identity, transform) as GameObject;
                }
            }
        }
        live = true;
    }

    void Update () {
       if(is_overlaped)
        {
            if(is_gened_item)
            {
                if (item == null)
                {
                    //Destroy(gameObject);
                    live = false;
                }
            }
            else
            {
                if(GameManager.instance.is_load_done)
                {
                    //gen_items();
                }
            }
        }
	}
}
