using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Mechanics { MOVEMENT, CLIMBING };

public class MechanicsHandler : MonoBehaviour {
    public Mechanics _current;
    //public CharacterController charController;
    //public OVRPlayerController OVRControl;
    public static event Action ClimbingInputLock;
    public static event Action ClimbingInputUnlock;

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
                //charController.stepOffset = 0.3f;
                //OVRControl.GravityModifier = .15f;
                if (ClimbingInputLock != null)
                {
                    ClimbingInputUnlock();
                }
                break;

            case Mechanics.CLIMBING:
                //charController.stepOffset = 0f;
                //OVRControl.GravityModifier = 0f;
                if(ClimbingInputLock != null)
                {
                    ClimbingInputLock();
                }
                break;

        }
    }
    private void OnEnable()
    {
        ClimbingManager.Moving += SwapMoving;
        ClimbingManager.Climbing += SwapClimbing;
    }
    private void OnDisable()
    {
        ClimbingManager.Moving -= SwapMoving;
        ClimbingManager.Climbing -= SwapClimbing;
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
