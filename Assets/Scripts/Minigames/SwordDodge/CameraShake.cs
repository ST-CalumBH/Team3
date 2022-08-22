using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform = default;
    private Vector3 _originalPosOfCam = default;
    public float shakeFrequency = default;

    // Start is called before the first frame update
    void Start()
    {
        _originalPosOfCam = cameraTransform.position;
        cameraTransform = Transform.FindObjectOfType<Camera>().transform;
    }

    private void CameraBake()
    {
        cameraTransform.position = _originalPosOfCam + Random.insideUnitSphere * shakeFrequency;
    }
        
  
}
