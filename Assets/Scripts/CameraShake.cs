using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineVirtualCamera mainCamera; //Referencia a la camara virtual.
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin; // Perfil de ruido agregado a la camara.

    private float moveTime;
    private float totalMoveTime; //variable para desvanecer poco a poco el movimiento de camara.
    private float initialIntensity;

    private void Awake()
    {
        Instance = this; //Este objeto.
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

            //Mathf.Lerp = Interpolacion lineal entre el primer parametro hacia segundo parametro por el tiempo (tercer parametro),
            //la intensidad se reduce gradualmente a 0 segun el tiempo que se coloque en el movimiento, la division hace que se reduzca cada vez menos.
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (moveTime / totalMoveTime)); //
        }
    }
}
