using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJump : MonoBehaviour
{
    public GameObject deleteLeft;
    public GameObject deleteRight;
    public GameObject leftMessage;
    public GameObject rightMessage;
    public bool opened = false;
    public Renderer left;
    public Renderer right;
    public GameObject disable1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character" && opened == false)
        {
            disable1.SetActive(false);
            leftMessage.SetActive(true);
            rightMessage.SetActive(true);
            //deleteLeft.SetActive(false);
            //deleteRight.SetActive(false);

            GameObject leftController;
            GameObject rightController;
            Material materialL = new Material(Shader.Find("graphs/ControllerXThumbstick"));
            Material materialR = new Material(Shader.Find("graphs/ControllerAB"));
            leftController = GameObject.Find("controller_left_renderPart_0");
            rightController = GameObject.Find("controller_right_renderPart_0");
            leftController.GetComponent<Renderer>().material = materialL;
            rightController.GetComponent<Renderer>().material = materialR;
            opened = true;
        }
    }
}
