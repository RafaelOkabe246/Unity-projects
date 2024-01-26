using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CinemachineVirtualCamera cmVCam;
    public static CameraController instance;
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        instance = this;
        cmVCam = virtualCamera;
    }

    public static IEnumerator CameraShake(float amplitude, float frequency, float time)
    {
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        yield return new WaitForSeconds(time);

        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }
}
