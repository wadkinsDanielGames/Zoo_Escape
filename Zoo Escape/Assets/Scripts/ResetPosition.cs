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
        reset = transform.position;
        resetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (outOfBounds == true)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().isKinematic = true;

            //stopMovement.useGravity = false;
            //stopMovement.isKinematic = true;
            transform.position = reset;
            transform.rotation = resetRotation;

            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
            outOfBounds = false;
            //stopMovement.useGravity = true;
            //stopMovement.isKinematic = false;
        }
	}
}
