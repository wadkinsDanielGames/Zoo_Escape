using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipThroughGround : MonoBehaviour {

    //If an item falls through the level by change, the item's location is reset.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.GetComponent<ResetPosition>().outOfBounds = true;
        }
    }

}
