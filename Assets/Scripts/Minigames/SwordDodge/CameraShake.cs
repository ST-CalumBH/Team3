using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
