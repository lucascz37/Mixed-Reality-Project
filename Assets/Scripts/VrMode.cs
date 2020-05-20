using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrMode : MonoBehaviour
{
    [SerializeField]
    private Canvas uiCanvas;
    public void ModoVR()
    {
        uiCanvas.enabled = false;
        StartCoroutine(ActivateVR());
    }
    IEnumerator ActivateVR()
    {
        string device = "cardboard";
        if(string.Compare(UnityEngine.XR.XRSettings.loadedDeviceName, device, true) != 0)
        {
            UnityEngine.XR.XRSettings.LoadDeviceByName(device);
            yield return null;
        }

        UnityEngine.XR.XRSettings.enabled = true;
    }

    IEnumerator ExitVR()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("");
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitVR());
            uiCanvas.enabled = true;
        }
    }

}
