using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrack : MonoBehaviour {
    public GameObject character;

	// Use this for initialization
	void Start () {
        this.transform.position += new Vector3(0f, .4f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.Euler( 0f, character.transform.rotation.y * -1, 0f);
	}
}
