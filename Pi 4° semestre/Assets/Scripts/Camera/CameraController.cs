using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CinemachineVirtualCamera cmVCam;
    public static CameraController instance;
    public CinemachineVirtualCamera virtualCamera;

    [Header("VFX")]
    private bool changedOrthosizeTemporally;
    private float initialOrthosize;
    [SerializeField] private float speedToResetOrthosize;
    private bool isCameraShaking;

    private float curAmplitude, curFrequency;

    private void Awake()
    {
        instance = this;
        cmVCam = virtualCamera;
    }

    private void Start()
    {
        initialOrthosize = cmVCam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        ResetOrthosize();
    }

    public void CameraShake(float amplitude, float frequency, float time) 
    {
        //Using this verification, we can override the current camera shakes with stronger shakes
        if (isCameraShaking && (curAmplitude < amplitude && curFrequency < frequency))
            StopCameraShake();

        if (isCameraShaking)
            return;

        StartCoroutine(ShakeCamera(amplitude, frequency, time));
    }

    public IEnumerator ShakeCamera(float amplitude, float frequency, float time)
    {
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        isCameraShaking = true;

        yield return new WaitForSecondsRealtime(time);

        StopCameraShake();
    }

    public void StopCameraShake() 
    {
        StopCoroutine(ShakeCamera(0f,0f,0f));
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        isCameraShaking = false;
    }

    public void ChangeOrthosizeTemporally(float newSize) 
    {
        cmVCam.m_Lens.OrthographicSize = newSize;
        changedOrthosizeTemporally = true;
    }

    private void ResetOrthosize() 
    {
        if (changedOrthosizeTemporally)
        {
            int multiplier = cmVCam.m_Lens.OrthographicSize < initialOrthosize ? 1 : -1;

            if (Mathf.Abs(cmVCam.m_Lens.OrthographicSize) > (initialOrthosize + 0.1f) || Mathf.Abs(cmVCam.m_Lens.OrthographicSize) < (initialOrthosize - 0.1f))
                cmVCam.m_Lens.OrthographicSize += speedToResetOrthosize * Time.deltaTime * multiplier;
            else
            {
                cmVCam.m_Lens.OrthographicSize = initialOrthosize;
                changedOrthosizeTemporally = false;
            }
        }
    }
}
