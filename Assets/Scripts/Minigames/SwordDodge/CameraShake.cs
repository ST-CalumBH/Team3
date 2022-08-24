using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< .merge_file_a39732
namespace SwordDodge {
    public class CameraShake : MonoBehaviour
    {
        public Camera mainCam;
        public float shakeFrequency;

        void Awake()
        {
            if (mainCam == null) mainCam = Camera.main;
        }

        public void Shake()
        {
            shakeFrequency = 0.1f;
            InvokeRepeating("CameraBake", 0, 0.01f);
            Invoke("CameraStop", 0.2f);
        }

        private void CameraBake()
        {
            if (shakeFrequency > 0)
            {
                Vector3 camPos = mainCam.transform.position;

                float offsetX = Random.value * shakeFrequency * 2 - shakeFrequency;
                float offsetY = Random.value * shakeFrequency * 2 - shakeFrequency;
                camPos.x += offsetX;
                camPos.y += offsetY;

                mainCam.transform.position = camPos;
            }
        }

        private void CameraStop()
        {
            CancelInvoke("CameraBake");
            mainCam.transform.localPosition = new Vector3(0,0,-10);

        }
    }
}
=======
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
>>>>>>> .merge_file_a38720
