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
    private void OnTriggerEnter(Collider other)//If you touch an out-of-bounds collider, you are prompted to return back to the play zone or else the scene will restart. 
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
    private void OnTriggerExit(Collider other)//This returns you back to the normal camera (back in bounds)
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
