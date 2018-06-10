using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mechanics { MOVEMENT, CLIMBING };

public class MechanicsHandler : MonoBehaviour {
    public Mechanics _current;
    public CharacterController charController;
    public OVRPlayerController OVRControl;
    // Use this for initialization
    void Start () {
        _current = Mechanics.MOVEMENT;
	}
	
	// Update is called once per frame
	void Update () {
        SwapState();
	}

    void SwapState() {

        switch (_current)
        {
            case Mechanics.MOVEMENT:
                charController.stepOffset = 0.3f;
                OVRControl.GravityModifier = 1;
                break;

            case Mechanics.CLIMBING:
                charController.stepOffset = 0f;
                OVRControl.GravityModifier = 0;
                break;

        }
    }
    private void OnEnable()
    {
        ClimbingManager.moving += SwapMoving;
        ClimbingManager.climbing += SwapClimbing;
    }
    private void OnDisable()
    {
        ClimbingManager.moving -= SwapMoving;
        ClimbingManager.climbing -= SwapClimbing;
    }
    void SwapMoving()
    {
        _current = Mechanics.MOVEMENT;
    }
    void SwapClimbing()
    {
        _current = Mechanics.CLIMBING;
    }
}
