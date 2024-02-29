using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private float shakerTimer;
    public static CameraShake Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        _cam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakerTimer = time;
    }
    private void Update()
    {
        if(shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            if (shakerTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
