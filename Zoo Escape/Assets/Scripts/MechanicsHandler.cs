using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Mechanics { MOVEMENT, CLIMBING };//This will represent different mechanics

public class MechanicsHandler : MonoBehaviour {
    public Mechanics _current;
    //public CharacterController charController;
    //public OVRPlayerController OVRControl;
    public static event Action ClimbingInputLock;
    public static event Action ClimbingInputUnlock;

    // Use this for initialization
    void Start () {
        _current = Mechanics.MOVEMENT;//Default mechanic is movement
	}
	
	void Update () {
        SwapState();
	}

    void SwapState() {

        switch (_current)
        {
            case Mechanics.MOVEMENT://This will allow for movement. This will unlock movement in the movement script.

                if (ClimbingInputLock != null)
                {
                    ClimbingInputUnlock();
                }
                break;

            case Mechanics.CLIMBING://This will ban movement in the movement script.

                if(ClimbingInputLock != null)
                {
                    ClimbingInputLock();
                }
                break;

        }
    }
    
    //This receives input from the climbing manager script, and changes states as deemed necessary
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
