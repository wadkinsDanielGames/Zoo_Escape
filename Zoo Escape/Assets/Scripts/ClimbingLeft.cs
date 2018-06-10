using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClimbingLeft: MonoBehaviour {
    //public Rigidbody character;
    //public GameObject controller;
    public Vector3 previousPosition;
    public bool grippable;

    // Use this for initialization
    void Start () {

        previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Climbable")
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
