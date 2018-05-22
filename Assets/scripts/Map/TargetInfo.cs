using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfo : MonoBehaviour {
    public enum Type
    {
        None,               //
        Volume_Up,          //
        Volume_Up_Slowly,   //
        Speed_Up,           //
        Time_Stop,          //
        Random_Box,         //
        Move_Stop,          
        Reverse,
    }

    public float size = 0.5f;
    public Vector3 scale;
    public Type type = Type.Volume_Up;
    bool is_effect_over;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 target = collision.gameObject.transform.localScale;
            if (target.x >= size)
            {
                match_item_effect(collision.gameObject);   
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 target = collision.gameObject.transform.localScale;
            if (target.x >= size)
            {
                match_item_effect(collision.gameObject);
            }
        }
    }

    void match_item_effect(GameObject obj)
    {
        switch(type)
        {
            case Type.Volume_Up: StartCoroutine(Volume_Up(0, obj, scale.x, 1)); break;
            case Type.Volume_Up_Slowly: StartCoroutine(Volume_Up(3, obj, scale.x, 3)); break;
            case Type.Speed_Up: StartCoroutine(set_speed(obj, 20, 5.0f)); break;
            case Type.Time_Stop: StartCoroutine(time_stop()); break;
            case Type.Random_Box: rand_box(obj); break;
            case Type.Move_Stop: StartCoroutine( set_state(Move.State.Move_stop, obj, 5)); break;
            case Type.Reverse: StartCoroutine(set_state(Move.State.Rev, obj, 5)); break;
            default: break;
        }

        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if(is_effect_over)
        {
            Destroy(gameObject);
        }
    }

    void rand_box(GameObject obj)
    {
        int rand = Random.Range(0, 100);
        if(rand >= 0 & rand <= 40)
        {
            StartCoroutine(time_stop());
        }
        else if(rand >= 40 & rand <= 70)
        {
            set_state(Move.State.Rev, obj, 8);
        }
        else
        {
            set_state(Move.State.Move_stop, obj, 5);
        }
    }

    IEnumerator set_state(Move.State state, GameObject obj, float time)
    {
        Move.State back = obj.GetComponent<Move>().state;
        obj.GetComponent<Move>().state = state;
        
        yield return new WaitForSeconds(time);
        obj.GetComponent<Move>().state = back;
    }

    IEnumerator time_stop()
    {
        GameManager.instance.ui.stop = true;
        yield return new WaitForSeconds(10);
        GameManager.instance.ui.stop = false;
    }

    IEnumerator set_speed(GameObject obj, float add_speed, float time)
    {
        obj.GetComponent<Move>().state = Move.State.Speed_up;
        float s = obj.GetComponent<Move>().max_speed;
        obj.GetComponent<Move>().max_speed = s + add_speed;
        obj.GetComponent<Move>().velocity = s;
        yield return new WaitForSeconds(time);
        obj.GetComponent<Move>().max_speed = s ;
        obj.GetComponent<Move>().velocity = s;
        obj.GetComponent<Move>().state = Move.State.None;
    }

    IEnumerator Volume_Up(float time, GameObject obj, float size, int ti)
    {
        float t =  size / ti;
        int tii = ti;

        while( tii > 0 )
        {
            obj.transform.localScale += Vector3.one * t;
            yield return new WaitForSeconds(time);
            tii--;
        }
        is_effect_over = true;
    }
}
