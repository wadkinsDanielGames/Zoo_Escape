using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public GameObject leftMessage;
    public GameObject rightMessage;
	// Use this for initialization
	void Start () {
        //Material materialL = new Material(Shader.Find("ControllerA"));
        //controllerR.GetComponent<Renderer>().material = materialL;
        //StartCoroutine(test());
        //leftMessage.SetActive(true);
        //rightMessage.SetActive(true);
    }

    // Update is called once per frame
     void Update () {
        GameObject leftController;
        GameObject rightController;
        Material materialL = new Material(Shader.Find("graphs/ControllerXThumbstick"));
        Material materialR = new Material(Shader.Find("graphs/ControllerAB"));

        leftController = GameObject.Find("controller_left_renderPart_0");
        rightController = GameObject.Find("controller_right_renderPart_0");
        leftController.GetComponent<Renderer>().material = materialL;
        rightController.GetComponent<Renderer>().material = materialR;
    }
    IEnumerator test()
    {
        GameObject leftController;
        GameObject rightController;
        Material materialL = new Material(Shader.Find("graphs/ControllerXThumbstick"));
        Material materialR = new Material(Shader.Find("graphs/ControllerAB"));

        yield return new WaitForSeconds(5);
        leftController = GameObject.Find("controller_left_renderPart_0");
        rightController = GameObject.Find("controller_right_renderPart_0");
        leftController.GetComponent<Renderer>().material = materialL;
        rightController.GetComponent<Renderer>().material = materialR;
    }
}
