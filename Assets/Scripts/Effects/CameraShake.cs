using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void TriggerShake(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }
    private IEnumerator Shake(float intensity, float duration)
    {
        var perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null)
        {
            perlin = _virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        float originalAmplitude = perlin.m_AmplitudeGain;
        float originalFrequency = perlin.m_FrequencyGain;

        perlin.m_AmplitudeGain = intensity;
        perlin.m_FrequencyGain = intensity;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        perlin.m_AmplitudeGain = originalAmplitude;
        perlin.m_FrequencyGain = originalFrequency;
    }
}
