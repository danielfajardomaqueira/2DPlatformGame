using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineVirtualCamera mainCamera; //Reference to the virtual camera
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin; //Noise profile added to the camera.


    private float moveTime;
    private float totalMoveTime; //variable to gradually fade the camera movement.
    private float initialIntensity;

    private void Awake()
    {
        Instance = this; //This object.
        mainCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float frequency, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        initialIntensity = intensity;
        totalMoveTime = time;
        moveTime = time;
    }

    private void Update()
    {
        if(moveTime > 0)
        {
            moveTime -= Time.deltaTime;

            //Mathf.Lerp = Linear interpolation between the first parameter towards the second parameter by time (third parameter)
            //The intensity is gradually reduced to 0 depending on the time spent in the movement, the division causes it to be reduced less and less.
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (moveTime / totalMoveTime));
        }
    }
}
