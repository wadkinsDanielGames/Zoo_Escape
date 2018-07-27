using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { MOVING, CLIMBING }

public class Movement : MonoBehaviour {
    private Rigidbody rigid;
    private CapsuleCollider collisionFollower;
    public new Transform camera;
    private Vector3 moveDirection;
    [SerializeField]
    private State _current;
    [Header("Custom Physics Variables")]
    public float speed;
    [Range(0 , 5)]
    public float jumpForce;
    private float tempSpeed;
    public bool grounded;
    private bool jump = false;
    // Use this for initialization

    void Start () {
        rigid = GetComponent<Rigidbody>();
        collisionFollower = GetComponent<CapsuleCollider>();
        _current = State.MOVING;
        tempSpeed = speed; //This stores the initial speed as the tempSpeed, so we can switch between running and the default walking speed.
    }

    private void OnCollisionStay(Collision collision)//checks directly below the character to see if there is a collision, to see if we are able to jump. This prevents jumping on air. 
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                grounded = true; 
            }
        }
    }

    private void OnCollisionExit(Collision collision)//If there is no collision below the player, we cannot jump. 
    {
        grounded = false;
    }

    private void Update()//This handles direct button presses for running and jumping
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && grounded && _current == State.MOVING)//this will allow you to perform a jump (handled in FixedUpdate)
        {
            jump = true;
        }
        if (OVRInput.Get(OVRInput.Button.Two))//This increases speed while a button is held down
        {
            speed = tempSpeed + 2;
        }
        if (OVRInput.GetUp(OVRInput.Button.Two))//Upon release of 'B' button, the speed returns to it's default speed.
        {
            speed = tempSpeed;
        }
    }

    // Update is called once per frame
    private void FixedUpdate () {
        SwapState();
        if (jump)//This will handle the mechanics of the jump.
        {
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jump = false;
        }
        
    }

    //This will handle the mechanics of the walking. 
    private void SwapState()
    {
        switch (_current)
        {
            case State.MOVING:
                if (collisionFollower.center.y > 0)
                {
                    collisionFollower.center = new Vector3(camera.localPosition.x, collisionFollower.center.y - .01f, camera.localPosition.z);//**This will emulate climbing over a ledge and standing up slowly. 
                }
                else
                {
                    collisionFollower.center = new Vector3(camera.localPosition.x, collisionFollower.center.y, camera.localPosition.z);//*********This will move the collider in comparison to the camera's location instead of being stuck in the middle. 
                    //This is good for room scale, where you aren't locked in the middle of the playspace.
                }
                float xAxis = Input.GetAxis("Horizontal");
                float zAxis = Input.GetAxis("Vertical");
                //moveDirection = new Vector3(xAxis, 0, zAxis) * speed * Time.deltaTime;
                moveDirection = (camera.transform.forward * zAxis * speed * Time.deltaTime) + (camera.transform.right * xAxis * speed * Time.deltaTime);
                rigid.MovePosition(new Vector3((transform.position.x + moveDirection.x), transform.position.y ,(transform.position.z + moveDirection.z)));
                break;

            case State.CLIMBING:
                collisionFollower.center = new Vector3(camera.localPosition.x, camera.localPosition.y + .5f, camera.localPosition.z);//This raises the object's collision capsule when climbing, to allow you to easily stand up when you pull yourself over a ledge.
                break;

        }
    }

    private void OnEnable()//Changes between climbing and moving, comes from mechanics handler.
    {
        MechanicsHandler.ClimbingInputLock += Lock;
        MechanicsHandler.ClimbingInputUnlock += Unlock;
    }

    private void OnDisable()
    {
        MechanicsHandler.ClimbingInputLock -= Lock;
        MechanicsHandler.ClimbingInputUnlock -= Unlock;
    }

    private void Lock()
    {
        _current = State.CLIMBING;
    }

    private void Unlock()
    {
        _current = State.MOVING;
    }

}
