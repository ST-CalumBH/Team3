using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd
{
    public class Shake : MonoBehaviour
    {
        public float shakeFrequency;

        public IEnumerator CameraBake()
        {
            if (shakeFrequency > 0)
            {
                Vector3 originalPos = transform.position;
                Vector3 newPos = transform.position;

                float offsetX = Random.value * shakeFrequency * 2 - shakeFrequency;
                float offsetY = Random.value * shakeFrequency * 2 - shakeFrequency;
                newPos.x += offsetX;
                newPos.y += offsetY;

                transform.position = newPos;

                yield return new WaitForSeconds(0.1f);

                transform.position = originalPos;
            }
        }
    }
}
