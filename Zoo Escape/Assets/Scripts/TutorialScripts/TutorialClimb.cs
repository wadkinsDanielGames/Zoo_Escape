using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClimb : MonoBehaviour
{
    public GameObject deleteLeft;
    public GameObject deleteRight;
    public GameObject leftMessage;
    public GameObject rightMessage;
    public bool opened = false;

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
            leftMessage.SetActive(true);
            rightMessage.SetActive(true);
            deleteLeft.SetActive(false);
            deleteRight.SetActive(false);

            GameObject leftController;
            GameObject rightController;
            Material materialL = new Material(Shader.Find("graphs/ControllerGrips"));
            Material materialR = new Material(Shader.Find("graphs/ControllerGrips"));
            leftController = GameObject.Find("controller_left_renderPart_0");
            rightController = GameObject.Find("controller_right_renderPart_0");
            leftController.GetComponent<Renderer>().material = materialL;
            rightController.GetComponent<Renderer>().material = materialR;
            opened = true;
        }
    }
}
