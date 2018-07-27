using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClimbingLeft: MonoBehaviour {

    public Vector3 previousPosition;
    public bool grippable;
    public bool holding = false;
    // Use this for initialization


    void Start () {
        //This starts tracking the previous position of the controller on start.
        previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    }

    //If you're holding onto an object, you can't climb with that hand (you can with the offhand). 
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

    //If the hand is touching a climbing surface and you aren't holding an object, you are able to grip this object.
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
