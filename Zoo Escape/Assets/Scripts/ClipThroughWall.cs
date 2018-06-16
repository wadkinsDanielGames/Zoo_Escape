using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipThroughWall : MonoBehaviour {
    public bool released = false;
    public bool backInBounds = true;
    public bool colliding = false;
    public bool startingTimer = false;
    public bool fromLeft;

    private void OnTriggerStay(Collider other)
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

    private void OnTriggerExit(Collider other)
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
    void Regrab()
    {
        if (colliding)
        {
            released = false;
        }
    }
    void Release()
    {
        if (colliding && this.transform.parent == null)
        {
            released = true;
            startingTimer = true;
        }

    }
}
