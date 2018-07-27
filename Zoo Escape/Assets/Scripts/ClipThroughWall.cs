using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipThroughWall : MonoBehaviour {
    public bool released = false;
    public bool backInBounds = true;
    public bool colliding = false;
    public bool startingTimer = false;
    public bool fromLeft;

    private void OnTriggerStay(Collider other)//If you drop an item into a wall for a second, reset the object's position.
    {

        if(other.tag == "ItemBounds")
        {
            colliding = true;
            if (released && startingTimer)
            {
                StartCoroutine(ReleaseWait());
            }

            if (released && colliding && !backInBounds)
            {
                this.GetComponent<ResetPosition>().outOfBounds = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)//An object no longer is out of bounds and its locationd does not need to be reset
    {
        if (other.tag == "ItemBounds")
        {
            colliding = false;
            backInBounds = true;
            released = false;
        }
    }

    IEnumerator ReleaseWait()
    {
        startingTimer = false;
        yield return new WaitForSeconds(1f);
        if (colliding && released)
        {
            backInBounds = false;
        }
    }

    private void OnEnable()
    {
        OVRGrabber.ItemReleasedOutOfBounds += Release;
        OVRGrabber.ItemRegrabbedBounds += Regrab;

    }

    private void OnDisable()
    {
        OVRGrabber.ItemReleasedOutOfBounds -= Release;
        OVRGrabber.ItemRegrabbedBounds -= Regrab;

    }

    void Regrab()//If you place an object in a wall and grab it before the 1 second timer is up, you no longer need to release its position.
    {
        if (colliding)
        {
            released = false;
        }
    }
    void Release()//Start a timer if you drop an object into a wall. 
    {
        if (colliding && this.transform.parent == null)
        {
            released = true;
            startingTimer = true;
        }

    }
}
