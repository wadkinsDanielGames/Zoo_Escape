using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClimbingManager : MonoBehaviour {
    //public Rigidbody character;
    public GameObject character;
    public ClimbingLeft left;
    public ClimbingRight right;
    public static event Action Moving;
    public static event Action Climbing;
    public Vector3 fixedInputLeft;
    public Vector3 fixedInputRight;
    public float rotationAmount = 0;
    public float radiusL;
    public float radiusR;
    public float angleL;
    public float angleR;
    public float quadrantL;
    public float quadrantR;
    public double adjustedLX;
    public double adjustedLZ;
    public double adjustedRX;
    public double adjustedRZ;
    void Update()
    {
        bool gripped = left.grippable || right.grippable;
        //detects if the controller's triggers are held down
        //if we are colliding with a grippable object and if we are holding down the grip buttons
        if (gripped)
        {
            //Left
            if (left.grippable && (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
            {

                character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                //character.transform.position += (left.previousPosition - fixedInputLeft);
                if (Climbing != null)
                {
                    Climbing();
                }
            }
            else //if (left.grippable && (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)))
            {
                if (Moving != null)
                {
                    Moving();
                }
            }

            //Right
            if (right.grippable && (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)||OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
            {

                character.transform.position += (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
                //character.transform.position += (right.previousPosition - fixedInputRight);
                if (Climbing != null)
                {
                    Climbing();
                }
            }
            else if (right.grippable && (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)))
            {
                if (Moving != null)
                {
                    Moving();
                }
            }
            if ((OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) && ((OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))))
            {
                character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                //character.transform.position += (left.previousPosition - fixedInputLeft);
                if (Climbing != null)
                    if (Climbing != null)
                {
                    Climbing();
                }
            }

        }
        //left.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        //right.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        left.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
        right.previousPosition = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);
        //fixedInputLeft = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y, Convert.ToSingle(adjustedLZ));
        //fixedInputRight = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).y, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);
        angleL = rotationAmount+(Mathf.Atan2(left.previousPosition.x, left.previousPosition.z) * (180 / Mathf.PI));
        angleR = rotationAmount+(Mathf.Atan2(right.previousPosition.x, right.previousPosition.z) * (180 / Mathf.PI));
        quadrantL = (angleL / 360) - (Mathf.Floor(angleL/360));
        quadrantR = (angleR / 360) - (Mathf.Floor(angleR/360));
        radiusL = left.previousPosition.z / (Mathf.Sin(angleL) * (180 / Mathf.PI));
        radiusR = right.previousPosition.z / (Mathf.Sin(angleR) * (180 / Mathf.PI));

        if (quadrantL > 0f && quadrantL < .25f)
        {
            //adjustedLX = (Mathf.Cos(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLZ = (Mathf.Sin(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLX = adjustedLZ/(Mathf.Tan(angleL) * (180 / Math.PI));

        }
        else if (quadrantL > .25 && quadrantL < .5)
        {
            //adjustedLX = (Mathf.Cos(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLZ = (Mathf.Sin(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLX = adjustedLZ / (Mathf.Tan(angleL) * (180 / Math.PI));

        }
        else if (quadrantL > .5 && quadrantL < .75)
        {
            //adjustedLX = (Mathf.Cos(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLZ = (Mathf.Sin(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLX = adjustedLZ / (Mathf.Tan(angleL) * (180 / Math.PI));

        }
        else if (quadrantL > .75 && quadrantL < 1)
        {
            //adjustedLX = (Mathf.Cos(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLZ = (Mathf.Sin(angleL) * (180 / Math.PI)) * radiusL;
            adjustedLX = adjustedLZ / (Mathf.Tan(angleL) * (180 / Math.PI));

        }
    }
    private void OnEnable()
    {
        OVRPlayerController.turnLeft += Left;
        OVRPlayerController.turnRight += Right;
    }
    private void OnDisable()
    {
        OVRPlayerController.turnLeft -= Left;
        OVRPlayerController.turnRight -= Right;
    }
    void Left(float amnt)
    {
        rotationAmount -= 45;
    }
    void Right(float amnt)
    {
        rotationAmount += 45;
    }

}





