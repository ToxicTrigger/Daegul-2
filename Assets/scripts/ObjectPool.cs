using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public List<GameObject> pool;
    public int max_size = 10;

    private void Awake()
    {
        if (pool.Count == 0)
        {
            pool = new List<GameObject>(max_size);
        }
    }

    public void Use(int Key)
    {
        if (pool[Key] == null && pool[Key])
        {
            //TODO 
        }
        pool[Key].SetActive(true);
    }

    public void Clean()
    {
        pool = new List<GameObject>(max_size);

    }

    public void Disable(string name)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject tmp = pool[i];
            if (tmp.name.Equals(name))
            {
                Disable(i);
            }
        }
    }

    public void Add(GameObject data)
    {
        if(pool.Capacity > pool.Count)
        {   
            if(GetIdleObject_index() != -1)
            {
                GameObject tmp = Instantiate(data);
            }
        }
    }

    public void Disable(int i)
    {
        if (pool[i] == null )
        {
            Debug.LogError("Add tmp Object, Key is null");
            Add((GameObject)Instantiate(null, Vector3.zero, Quaternion.identity, null));
        }

        pool[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

        pool[i].transform.position = Vector3.zero;
        pool[i].transform.rotation = new Quaternion();
        pool[i].transform.localScale = Vector3.one;

        pool[i].SetActive(false);
    }

    public int GetIdleObject_index()
    {
        for(int i = 0 ; i < pool.Count; i++)
        {
            if(!pool[i].activeSelf)
            {
                return i;
            }
        }
        Debug.LogError("can't allocated in Pool, all pool Object Enable");
        return -1;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
