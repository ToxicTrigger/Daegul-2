using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public Vector3 offset;
    public Move Pig;
    public float time_factor = 1.0f;
    public Rigidbody rig;

    public Ray Look;
    public float input_x;
    Vector3 moved_pos;
    Vector3 tri;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(input_x != 0)
        {
            Vector3 world_pos = Target.position;
            Vector3 tmp = Vector3.Lerp(transform.position, world_pos + moved_pos, Time.deltaTime * time_factor);
            transform.position = tmp;
        }
        transform.LookAt(Target.position);
    }

    void LateUpdate()
    {
        input_x += CrossPlatformInputManager.GetAxisRaw("Mouse Y") * Mathf.PI * 0.01f;
        if (input_x != 0)
        {
            float x = 0;
            float z = 0;
            x = Mathf.Cos(input_x);
            z = Mathf.Sin(input_x);
            tri = new Vector3(x, 0,z) * offset.z * Pig.transform.localScale.x * 0.5f;
            moved_pos = tri;
            moved_pos.y = offset.y * Pig.transform.localScale.x * 0.5f;
        }
    }
}
