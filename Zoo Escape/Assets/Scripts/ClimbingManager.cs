using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Actions { LEFTGRAB, LEFTRELEASE, RIGHTGRAB, RIGHTRELEASE }
public class ClimbingManager : MonoBehaviour
{
    //public Rigidbody character;
    private Rigidbody character;
    public ClimbingLeft left;
    public ClimbingRight right;
    public static event Action Moving;
    public static event Action Climbing;
    public new Transform camera;
    private Actions _actions;
    private bool gripped;

    private bool leftGrab=false;
    private bool leftRelease=false;
    private bool rightGrab=false;
    private bool rightRelease=false;

    private void Start()
    {
        character = GetComponent<Rigidbody>();
        _actions = Actions.LEFTGRAB;
    }
    private void Update()
    {
        //gripped = left.grippable || right.grippable;
        //detects if the controller's triggers are held down
        //if we are colliding with a grippable object and if we are holding down the grip buttons
        if (gripped)
        {
            //Left
            if (left.grippable && (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
            {
                //_actions = Actions.LEFTGRAB;
                leftGrab = true;

            }
            else if (left.grippable && (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)))
            {
                //_actions = Actions.LEFTRELEASE;
                leftGrab = false;
                leftRelease = true;

            }

            //Right
            if (right.grippable && (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
            {
                //_actions = Actions.RIGHTGRAB;
                rightGrab = true;
            }
            else if (right.grippable && (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)))
            {
                //_actions = Actions.RIGHTRELEASE;
                rightGrab = false;
                rightRelease = true;
            }


        }
        //left.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
        //right.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);

    }
    private void FixedUpdate()
    {
        gripped = left.grippable || right.grippable;

        if (gripped)
        {
            //SwapStates();
            if (leftGrab && !leftRelease)
            {
                character.isKinematic = true;
                //character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                character.transform.Translate((left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)));// += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));

                if (Climbing != null)
                {
                    Climbing();
                }
            }
            else if (leftRelease)
            {
                character.isKinematic = false;
                character.velocity = (left.previousPosition - left.transform.localPosition) / Time.deltaTime;
                if (Moving != null)
                {
                    Moving();
                }
                leftRelease = false;
            }
            if (rightGrab && !rightRelease)
            {
                character.isKinematic = true;
                //character.transform.position += (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
                character.transform.Translate((right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch)));
                if (Climbing != null)
                {
                    Climbing();
                }
            }
            else if (rightRelease)
            {
                character.isKinematic = false;
                character.velocity = (right.previousPosition - right.transform.localPosition) / Time.deltaTime;

                if (Moving != null)
                {
                    Moving();
                }
                rightRelease = false;
            }
        }
        left.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
        right.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);
    }

   /* private void SwapStates()
    {
        switch(_actions)
        {
            case Actions.LEFTGRAB:
                character.isKinematic = true;
                character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                if (Climbing != null)
                {
                    Climbing();
                }
            break;

            case Actions.LEFTRELEASE:
                character.isKinematic = false;
                character.velocity = (left.previousPosition - left.transform.localPosition) / Time.deltaTime;
                if (Moving != null)
                {
                    Moving();
                }
            break;
            case Actions.RIGHTGRAB:
                character.isKinematic = true;
                character.transform.position += (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
                if (Climbing != null)
                {
                    Climbing();
                }
            break;
            case Actions.RIGHTRELEASE:
                character.isKinematic = false;
                character.velocity = (right.previousPosition - right.transform.localPosition) / Time.deltaTime;

                if (Moving != null)
                {
                    Moving();
                }
            break;
        }
    }*/

}