using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFXHandler : MonoBehaviour
{

    [Header("Audio Sources")]

    public AudioSource engineAudio;
    public AudioSource carHitAudio;
    public AudioSource tireScreechAudio;
    public AudioSource raceBackgroudAudio;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;
    CarController carController;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>(); 
    }

   
    private void Update()
    {
        UpdateEngineSFX();
        UpdateTireNoiseSFX(); 

    }
    void UpdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();
        float desiredEngineVolume = velocityMagnitude * 0.05f;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudio.volume = Mathf.Lerp(engineAudio.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTireNoiseSFX()
    {
        if(carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tireScreechAudio.volume = Mathf.Lerp(tireScreechAudio.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tireScreechAudio.volume = Mathf.Abs(lateralVelocity) * 0.5f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;

            }
        }
    }
}
