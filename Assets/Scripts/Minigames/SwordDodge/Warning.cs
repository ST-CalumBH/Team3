using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordDodge {
    public class Warning : MonoBehaviour
    {
        [SerializeField] private float flashTime = 0.3f;
        [SerializeField] private float flashCount = 3f;
        AudioSource aSource;

        private void Start()
        {
            aSource = GetComponent<AudioSource>();
        }

        public float GetTotalFlashTime()
        {
            return flashTime * flashCount;
        }

        public void StartFlash()
        {
            StartCoroutine(Flash());
        }

        public IEnumerator Flash()
        {
            //Debug.Log("Enum Start");
            int i = 0;
            SpriteRenderer tWarning = GetComponent<SpriteRenderer>();
            while (i < flashCount)
            {
                tWarning.enabled = true;
                aSource.Play();
                //Debug.Log("FLASH ON");
                yield return new WaitForSeconds(flashTime);
                tWarning.enabled = false;
                //Debug.Log("FLASH OFF");
                yield return new WaitForSeconds(flashTime);
                i++;
            }
        }
    }
}
