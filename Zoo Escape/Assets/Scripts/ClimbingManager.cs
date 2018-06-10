using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClimbingManager : MonoBehaviour {
    //public Rigidbody character;
    public GameObject character;
    public ClimbingLeft left;
    public ClimbingRight right;
    public static event Action moving;
    public static event Action climbing;

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
                if (climbing != null)
                {
                    climbing();
                }
            }
            else //if (left.grippable && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
            {
                if (moving != null)
                {
                    moving();
                }
            }

            //Right
            if (right.grippable && (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)||OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
            {

                character.transform.position += (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
                if (climbing != null)
                {
                    climbing();
                }
            }
            else if (right.grippable && (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)))
            {
                if (moving != null)
                {
                    moving();
                }
            }
            if ((OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) && ((OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))))
            {
                character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                if (climbing != null)
                {
                    climbing();
                }
            }

        }

        left.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        right.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

}





/*if (gripped)
       {
       //Left
           if (left.grippable && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
           {
               character.useGravity = false;
               character.isKinematic = true;
               character.transform.position += (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
               if (moving != null)
               {
                   climbing();
               }
           }
           else if (left.grippable && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
           {
               character.useGravity = true;
               character.isKinematic = false;
               character.velocity = (left.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)) / Time.deltaTime;

           }
        //Right
           if (right.grippable && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
           {
               character.useGravity = false;
               character.isKinematic = true;
               character.transform.position += (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

           }
           else if (right.grippable && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
           {
               character.useGravity = true;
               character.isKinematic = false;
               character.velocity = (right.previousPosition - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch)) / Time.deltaTime;
           }
       }

       else
       {
           character.useGravity = true;
           character.isKinematic = false;
           if(moving!= null)
           {
               moving();
           }
       }

       left.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
       right.previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
       */
