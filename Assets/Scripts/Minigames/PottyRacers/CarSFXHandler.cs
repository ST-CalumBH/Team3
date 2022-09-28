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

    private void Awake()
    {
      /*  carController = GetComponentInParent<CarController>(); */
    }


    private void Update()
    {
       /* UpdateEngineSFX();
        UpdateTireNosieSFX(); */

    }
    void UpdateEngineSFX()
    {
        /*float velocityMagnitude = carController.GetVelocityMagnitude();*/

    }
}
