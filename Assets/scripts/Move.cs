using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Movable Components
/// </summary>
public class Move : MonoBehaviour {
    public enum State
    {
        None,
        Time_stop,
        Speed_up,
        Rev,
        Move_stop,
    }
    public State state;

    public float force;
    public float max_speed = 24;
    public float speed = 2f;
    public float velocity;
    Rigidbody rig;
    public Vector3 movement;
    float h, v;

    public float Size;
    public bool isGameOver;

    //non rotated Vector
    public GameObject worldNormal;
    //Y fixing Direction
    public Transform pig_look;

    bool isJumping;
    public float jumpPower = 1;
    public Camera cam;
    public float default_fov;
    public float fov;
    public float max_fov = 100;
    public Color ice;

    public int Level = 1;
    
	void Start () {
        rig = GetComponent<Rigidbody>();
        default_fov = cam.fieldOfView;
        fov = default_fov;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Mob"))
        {
            AI ai = collision.gameObject.GetComponent<AI>();
            if (Size <= 0)
            {
                isGameOver = true;
            }
            else
            {
                transform.localScale -= Vector3.one * ai.attack_power;
            }
        }

    }

    private void FixedUpdate()
    {
        if (state == State.Move_stop)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }
        else
        {
            Run();
            Jump();
            force = rig.angularVelocity.magnitude;
            max_speed = transform.localScale.x * 24f;
            speed = transform.localScale.x * 2f;
            rig.maxAngularVelocity = max_speed;
            Size = transform.localScale.x;
            if (max_fov >= cam.fieldOfView)
            {
                cam.fieldOfView = fov + force;
            }
            else
            {
                cam.fieldOfView = max_fov;
            }
        }
    }
    
    private void Update()
    {
        h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        movement.x = h;
        movement.z = v;

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    void Jump()
    {
        if (!isJumping) return;

        rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isJumping = false;

    }
    public Transform tmp_;
    void Run()
    {
        //independent Vector is always this gameobjects position
        tmp_.position = transform.position;
        
        //worldNormal.transform.position = tmp_;

        if (h == 0 && v == 0)
        {
            velocity = 0 ;
            return;
        }
        else
        {
            Vector3 moveLocat = new Vector3(h, 0, v);
            Vector3 tmp_look = transform.position - (moveLocat);
            Vector3 result = (tmp_look) + transform.position.normalized;
            pig_look.position = result;
        }

        if (velocity <= max_speed)
        {
            velocity += Time.deltaTime * (speed);
        }
        else
        {
            velocity = max_speed;
        }
        
        if(state != State.Rev)
        {
            rig.AddTorque(worldNormal.transform.right * v * velocity, ForceMode.Force);
            rig.AddTorque(worldNormal.transform.forward * -h * velocity, ForceMode.Force);
        }
        else
        {
            rig.AddTorque((worldNormal.transform.right*-1) * v * velocity, ForceMode.Force);
            rig.AddTorque((worldNormal.transform.forward * -1) * -h * velocity, ForceMode.Force);
        }

        
    }
}
