using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrMode : MonoBehaviour
{
    public void ModoVR()
    {
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
    // Update is called once per frame

}
