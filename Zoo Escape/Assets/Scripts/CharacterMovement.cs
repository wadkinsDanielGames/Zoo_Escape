using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public Rigidbody player;
    public float controllerX;
    public float controllerY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate (){

        controllerX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        controllerY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        if (controllerY > .5f || controllerY < -.5f)
        {
            transform.position += this.transform.forward * 2 * Time.deltaTime;
        }
        /*if (Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f)
        {
            player.transform.position = new Vector2(player.velocity.x, Input.GetAxisRaw("Vertical") * speed);

        }*/



        /*if (Input.GetAxisRaw("Horizontal") > .5f || Input.GetAxisRaw("Horizontal") < -.5f)
        {
            player.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, player.velocity.y);

        }
        if (Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f)
        {
            player.velocity = new Vector2(player.velocity.x, Input.GetAxisRaw("Vertical") * speed);

        }

        if (Input.GetAxisRaw("Horizontal") < .5f && Input.GetAxisRaw("Horizontal") > -.5f)
        {
            player.velocity = new Vector2(0.0f, player.velocity.y);

        }

        if (Input.GetAxisRaw("Vertical") < .5f && Input.GetAxisRaw("Vertical") > -.5f)
        {
            player.velocity = new Vector3(player.velocity.x, 0.0f);

        }*/
    }
}
