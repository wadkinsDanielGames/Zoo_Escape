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
        tempSpeed = speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                grounded = true; 
            }
        }
        //grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && grounded && _current == State.MOVING)
        {
            jump = true;
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            speed = tempSpeed + 2;
        }
        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            speed = tempSpeed;
        }
    }
    // Update is called once per frame
    private void FixedUpdate () {
        SwapState();
        //collisionFollower.center = new Vector3(camera.localPosition.x, collisionFollower.center.y, camera.localPosition.z);
        if (jump)
        {
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jump = false;
        }
        
    }

    private void SwapState()
    {
        switch (_current)
        {
            case State.MOVING:
                if (collisionFollower.center.y > 0)
                {
                    collisionFollower.center = new Vector3(camera.localPosition.x, collisionFollower.center.y - .01f, camera.localPosition.z);
                }
                else
                {
                    collisionFollower.center = new Vector3(camera.localPosition.x, collisionFollower.center.y, camera.localPosition.z);
                }
                float xAxis = Input.GetAxis("Horizontal");
                float zAxis = Input.GetAxis("Vertical");
                //moveDirection = new Vector3(xAxis, 0, zAxis) * speed * Time.deltaTime;
                moveDirection = (camera.transform.forward * zAxis * speed * Time.deltaTime) + (camera.transform.right * xAxis * speed * Time.deltaTime);
                rigid.MovePosition(new Vector3((transform.position.x + moveDirection.x), transform.position.y ,(transform.position.z + moveDirection.z)));
                break;

            case State.CLIMBING:
                collisionFollower.center = new Vector3(camera.localPosition.x, camera.localPosition.y + .5f, camera.localPosition.z);
                break;

        }
    }
    private void OnEnable()
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
