using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {
    private Vector3 reset;
    private Quaternion resetRotation;
    public bool outOfBounds = false;
    public Rigidbody stopMovement;
    // Use this for initialization

    void Start () {
        reset = transform.position;//This stores the reset location on the object this script is applied to.
        resetRotation = transform.rotation;//This resets the rotation on the object this script is applied to.
	}
	

	void Update () {//Instead of destroying and creating a new object, this stops movement first, then resets position, then enables physics attributes again. 
        if (outOfBounds == true)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().isKinematic = true;

            transform.position = reset;
            transform.rotation = resetRotation;

            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
            outOfBounds = false;
        }
	}
}
