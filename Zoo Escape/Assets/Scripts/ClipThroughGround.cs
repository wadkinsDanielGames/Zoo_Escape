using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipThroughGround : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.GetComponent<ResetPosition>().outOfBounds = true;
        }
    }

}
