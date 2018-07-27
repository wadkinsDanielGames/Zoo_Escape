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
        _actions = Actions.LEFTGRAB;//Defaults to left, this will do nothing however at start. 
    }
    private void Update()
    {
        if (gripped)//If gripped, accept input from the controllers. 
        {
            //If the lefthand triggers are held down while you're interacting with a climbable surface
            if (left.grippable && (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
            {
                //This triggers a grab in fixedupdate (updated since we began working with rigidbodies)
                leftGrab = true;

            }
            //Upon release of the triggers with the lefthand
            else if (left.grippable && (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)))
            {
                //This triggers a release in fixedupdate(updated since we began working with rigidbodies)
                leftGrab = false;
                leftRelease = true;

            }

            //If the righthand triggers are held down while you're interacting with a climbable surface
            if (right.grippable && (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
            {

                rightGrab = true;

            }
            //Upon release of triggers with the righthand
            else if (right.grippable && (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)))
            {

                rightGrab = false;
                rightRelease = true;

            }

        }

    }
    private void FixedUpdate()
    {

        //detects if the controller's triggers are held down
        //if we are colliding with a grippable object and if we are holding down the grip buttons
        gripped = left.grippable || right.grippable;

        if (gripped)
        {
            //This will move the character in relative to the controller's local position.
            if (leftGrab && !leftRelease)
            {
                character.isKinematic = true;
                //character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));******Old method in which you can only turn with the headset (spinning in place), otherwise the climbing will not act properly unless character rotation is at (0,0,0)
                character.transform.Translate((left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)));//Updated version that allows the character to turn directly without affecting the climb. Allows offhand joystick rotation to not affect climbing.
                
                if (Climbing != null)//When climbing, you cannot walk. This sends a notification off to the mechanics handler to let you know you're climbing.
                {
                    Climbing();
                }
            }

            //This will release the climbing, and will add momentum and trajectory if released in a bursting motion in a certain direction (you can launch body upwards, to the side, etc). 
            else if (leftRelease)
            {
                character.isKinematic = false;
                character.velocity = (left.previousPosition - left.transform.localPosition) / Time.deltaTime;
                
                if (Moving != null)//This will return the mechanics handler back to the walking state.
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

        //This constantly tracks the local controller positions of the left and right hands. 
        left.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
        right.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);

    }

}