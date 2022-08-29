using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Combat {
    public class Minigame : MonoBehaviour
    {

        public bool result = false;
        public bool isInProgress = true;

        public IEnumerator EndMinigame()
        {
            yield return new WaitForSeconds(1f);
            isInProgress = false;
            CheckResult();
            Destroy(transform.parent.gameObject);
        }
        public IEnumerator EndMinigame(bool res)
        {
            yield return new WaitForSeconds(1f);
            isInProgress = false;
            result = res;
            Debug.Log(res.ToString());
            CheckResult();
            Destroy(transform.parent.gameObject);
        }

        public IEnumerator EndMinigame(float time)
        {
            yield return new WaitForSeconds(time);
            isInProgress = false;
            CheckResult();
            Destroy(transform.parent.gameObject);
        }
        public IEnumerator EndMinigame(float time, bool res)
        {
            yield return new WaitForSeconds(time);
            isInProgress = false;
            result = res;
            Debug.Log(res.ToString());
            CheckResult();
            Destroy(transform.parent.gameObject);
        }

        private void CheckResult()
        {
            if (result == false)
            {
                LifeSystem.Instance.playerLives--;
            }
        }
    }
}