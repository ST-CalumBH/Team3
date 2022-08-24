using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
    public class Minigame : MonoBehaviour
    {

        public bool result = false;
        public bool isInProgress = true;

        public IEnumerator EndMinigame()
        {
            yield return new WaitForSeconds(1f);
            isInProgress = false;
            Destroy(transform.parent.gameObject);
        }
        public IEnumerator EndMinigame(bool res)
        {
            yield return new WaitForSeconds(1f);
            isInProgress = false;
            result = res;
            Debug.Log(res.ToString());
            Destroy(transform.parent.gameObject);
        }

        public IEnumerator EndMinigame(float time)
        {
            yield return new WaitForSeconds(time);
            isInProgress = false;
            Destroy(transform.parent.gameObject);
        }
        public IEnumerator EndMinigame(float time, bool res)
        {
            yield return new WaitForSeconds(time);
            isInProgress = false;
            result = res;
            Debug.Log(res.ToString());
            Destroy(transform.parent.gameObject);
        }
    }