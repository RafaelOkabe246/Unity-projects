using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeviceMode
{
    Mobile,
    Desktop
}

public class DetectDevice : MonoBehaviour
{
    public static DeviceMode currentDevice;

    // Start is called before the first frame update
    void Awake()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            currentDevice = DeviceMode.Mobile;
            //Debug.Log("Is desktop");
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            currentDevice = DeviceMode.Mobile;
            Debug.Log("is mobile");
        }
    }

}
