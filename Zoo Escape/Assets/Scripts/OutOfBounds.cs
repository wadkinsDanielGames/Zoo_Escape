using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutOfBounds : MonoBehaviour {

    public bool outOfBounds;
    public GameObject mainCamera;
    public GameObject outCamera;
    public GameObject camScripts;
    public bool exited;
    public string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bounds")
        {
            outOfBounds = true;
            outCamera.SetActive(true);
            mainCamera.GetComponent<Camera>().enabled = false;
            camScripts.GetComponent<OVRCameraRig>().enabled = false;
            exited = false;
            StartCoroutine(CountDown());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Bounds")
        {
            outOfBounds = false;
            camScripts.GetComponent<OVRCameraRig>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = true;
            outCamera.SetActive(false);
            exited = true;
        }
    }
    public IEnumerator CountDown()
    {
        if (exited == true)
        {
            yield break;
        }
        yield return new WaitForSeconds(5f);
        if(exited == false)
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
