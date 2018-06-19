using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 4.0F;
    private float startTime;
    private float journeyLength;
    private int controllers = 0;
    private bool left;
    private bool right;
    public int doorNumber;
    private bool secondDoorLift = false;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (controllers == 2 && doorNumber == 1) {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
        if (OVRInput.GetDown(OVRInput.Button.One) && !left)
        {
            left = true;
            controllers++;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three) && !right)
        {
            right = true;
            controllers++;
        }
        if (doorNumber == 2 && secondDoorLift)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            secondDoorLift = true;
        }
    }

}
