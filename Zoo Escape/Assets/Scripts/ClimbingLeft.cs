using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClimbingLeft: MonoBehaviour {
    //public Rigidbody character;
    //public GameObject controller;
    public Vector3 previousPosition;
    public bool grippable;
    public bool holding = false;
    // Use this for initialization
    void Start () {

        previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    }
    private void OnEnable()
    {
        OVRGrabber.CanClimbL += NotHoldingObject;
        OVRGrabber.CantClimbL += HoldingObject;
    }
    private void OnDisable()
    {
        OVRGrabber.CanClimbL -= NotHoldingObject;
        OVRGrabber.CantClimbL -= HoldingObject;
    }
    void NotHoldingObject()
    {
        holding = false;
    }
    void HoldingObject()
    {
        holding = true;
        grippable = false;
    }

    private void Update()
    {
        if (holding == true)
        {
            grippable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Climbable" && !holding)
        {
            grippable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Climbable")
        {
            grippable = false;
        }
    }

}
